using System.Collections.ObjectModel ;
using System.Windows.Threading ;
using Revit.Async ;
using Sonny.Application.Core.Bases ;
using Sonny.Application.Core.Interfaces ;
using Sonny.Application.Features.ColumnFromCad.Contexts ;
using Sonny.Application.Features.ColumnFromCad.Interfaces ;
using Sonny.Application.Features.ColumnFromCad.Models ;
using Sonny.Application.UI.Views ;
using Sonny.RevitExtensions.Extensions ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Features.ColumnFromCad.ViewModels ;

public partial class ColumnFromCadViewModel : BaseViewModelWithSettings<ColumnFromCadSettings>
{
    #region Services

    private IColumnFromCadOrchestrator ColumnFromCadOrchestrator { get ; }

    private IColumnFromCadContext Context { get ; }

    #endregion

    #region Constructor

    public ColumnFromCadViewModel(ICommonServices commonServices,
        IColumnFromCadOrchestrator columnFromCadOrchestrator,
        IColumnFromCadContext context,
        IViewModelSettingsService<ColumnFromCadSettings> settingsService) : base(commonServices,
        settingsService)
    {
        ColumnFromCadOrchestrator = columnFromCadOrchestrator ;
        Context = context ;

        InitializeWithSettings() ;
    }

    #endregion

    #region Properties for UI Binding

    /// <summary>
    ///     All available layers from CAD link
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<string> _allLayerNames = [] ;

    /// <summary>
    ///     Selected layer name
    /// </summary>
    [ObservableProperty]
    private string? _selectedLayer ;

    /// <summary>
    ///     Whether to model by hatch (true) or boundary (false)
    /// </summary>
    [ObservableProperty]
    private bool _isModelByHatch = true ;

    /// <summary>
    ///     Whether to model by boundary
    /// </summary>
    [ObservableProperty]
    private bool _isModelByBoundary ;

    /// <summary>
    ///     All available column families
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Family> _allColumnFamilies = [] ;

    /// <summary>
    ///     Selected rectangular column family
    /// </summary>
    [ObservableProperty]
    private Family? _selectedRectangularColumnFamily ;

    /// <summary>
    ///     Selected circular column family
    /// </summary>
    [ObservableProperty]
    private Family? _selectedCircularColumnFamily ;

    /// <summary>
    ///     All available type parameters for rectangular columns (Width, Height)
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<string> _allRectangularColumnTypeParameters = [] ;

    /// <summary>
    ///     All available type parameters for circular columns (Diameter)
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<string> _allCircularColumnTypeParameters = [] ;

    /// <summary>
    ///     Width parameter name for rectangular columns
    /// </summary>
    [ObservableProperty]
    private string? _widthParameter ;

    /// <summary>
    ///     Height parameter name for rectangular columns
    /// </summary>
    [ObservableProperty]
    private string? _heightParameter ;

    /// <summary>
    ///     Diameter parameter name for circular columns
    /// </summary>
    [ObservableProperty]
    private string? _diameterParameter ;

    /// <summary>
    ///     All available levels
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Level> _allLevels = [] ;

    /// <summary>
    ///     Base level for columns
    /// </summary>
    [ObservableProperty]
    private Level? _baseLevel ;

    /// <summary>
    ///     Top level for columns
    /// </summary>
    [ObservableProperty]
    private Level? _topLevel ;

    /// <summary>
    ///     Base offset in display unit
    /// </summary>
    [ObservableProperty]
    private double _baseOffsetDisplay ;

    /// <summary>
    ///     Top offset in display unit
    /// </summary>
    [ObservableProperty]
    private double _topOffsetDisplay ;

    #endregion

    #region Commands

    /// <summary>
    ///     Command to execute column creation process
    /// </summary>
    [RelayCommand]
    public async Task Execute()
    {
        if (! ValidateInput()) {
            return ;
        }

        // Save settings before executing
        SaveSettings() ;

        // Extract column data
        var columnsData = await RevitTask.RunAsync(() => ColumnFromCadOrchestrator.ExtractColumnData(
            Context.SelectedCadLink,
            SelectedLayer!,
            IsModelByHatch)) ;

        if (columnsData.Count == 0) {
            ShowInfo(ResourceHelper.GetString("MessageNoColumnsFound")) ;
            return ;
        }

        // Close settings window first
        CloseWindow() ;

        // Show progress window on UI thread
        var processWindow = new ProgressView(ResourceHelper.GetString("MessageCreatingColumns")) ;
        processWindow.Show() ;

        // Force UI update
        await Task.Delay(100) ;

        // Create columns
        var createdIds = await RevitTask.RunAsync(() =>
        {
            var context = new ColumnCreationContext
            {
                Document = RevitDocument.Document,
                SelectedRectangularColumnFamily = SelectedRectangularColumnFamily!,
                SelectedCircularColumnFamily = SelectedCircularColumnFamily!,
                WidthParameter = WidthParameter!,
                HeightParameter = HeightParameter!,
                DiameterParameter = DiameterParameter!,
                BaseLevel = BaseLevel!,
                TopLevel = TopLevel!,
                BaseOffset = BaseOffsetInternal,
                TopOffset = TopOffsetInternal,
                ProgressCallback = (current,
                    total) =>
                {
                    // Update progress on UI thread
                    processWindow.Dispatcher.Invoke(() =>
                        {
                            processWindow.UpdateProgress(current,
                                total) ;
                        },
                        DispatcherPriority.Background) ;
                }
            } ;

            return ColumnFromCadOrchestrator.CreateColumns(context) ;
        }) ;

        // Close progress window
        processWindow.Dispatcher.Invoke(() => { processWindow.Close() ; }) ;

        // Show result and select columns
        if (createdIds.Count > 0) {
            // Select created columns to highlight them in Revit UI
            await RevitTask.RunAsync(() => { RevitDocument.UIDocument.Selection.SetElementIds(createdIds) ; }) ;

            ShowInfo(ResourceHelper.GetString("MessageSuccessfullyCreated",
                createdIds.Count)) ;
        }
        else {
            ShowWarning(ResourceHelper.GetString("MessageNoColumnsCreated")) ;
        }
    }

    /// <summary>
    ///     Cancel command
    /// </summary>
    [RelayCommand]
    private void Cancel() => CloseWindow() ;

    #endregion

    #region Event Handlers

    partial void OnSelectedRectangularColumnFamilyChanged(Family? value)
    {
        if (value == null) {
            return ;
        }

        LoadRectangularColumnParameters(value) ;
    }

    partial void OnSelectedCircularColumnFamilyChanged(Family? value)
    {
        if (value == null) {
            return ;
        }

        LoadCircularColumnParameters(value) ;
    }

    #endregion

    #region Unit Conversion

    /// <summary>
    ///     Gets base offset in internal unit (feet)
    /// </summary>
    public double BaseOffsetInternal =>
        UnitConverter.ToInternalUnit(BaseOffsetDisplay,
            DisplayUnit) ;

    /// <summary>
    ///     Gets top offset in internal unit (feet)
    /// </summary>
    public double TopOffsetInternal =>
        UnitConverter.ToInternalUnit(TopOffsetDisplay,
            DisplayUnit) ;

    /// <summary>
    ///     Called when display unit changes, converts offset values to new unit
    /// </summary>
    protected override void OnDisplayUnitChanged(ForgeTypeId oldUnit,
        ForgeTypeId newUnit)
    {
        // Convert BaseOffsetDisplay from old unit to new unit
        var baseOffsetInternal = UnitConverter.ToInternalUnit(BaseOffsetDisplay,
            oldUnit) ;
        BaseOffsetDisplay = UnitConverter.FromInternalUnit(baseOffsetInternal,
            newUnit) ;

        // Convert TopOffsetDisplay from old unit to new unit
        var topOffsetInternal = UnitConverter.ToInternalUnit(TopOffsetDisplay,
            oldUnit) ;
        TopOffsetDisplay = UnitConverter.FromInternalUnit(topOffsetInternal,
            newUnit) ;
    }

    #endregion

    #region Settings Management

    /// <summary>
    ///     Applies loaded settings to the view model
    /// </summary>
    protected override void ApplySettings(ColumnFromCadSettings settings)
    {
        // Load layer selection
        if (! string.IsNullOrEmpty(settings.SelectedLayer)
            && AllLayerNames.Contains(settings.SelectedLayer)) {
            SelectedLayer = settings.SelectedLayer ;
        }

        // Load modeling method
        IsModelByHatch = settings.IsModelByHatch ;
        IsModelByBoundary = ! settings.IsModelByHatch ;

        // Load column families (must be after LoadColumnFamilies is called)
        if (! string.IsNullOrEmpty(settings.RectangularColumnFamilyId)) {
            var rectangularFamily =
                AllColumnFamilies.FirstOrDefault(f => f.UniqueId == settings.RectangularColumnFamilyId) ;
            if (rectangularFamily != null) {
                SelectedRectangularColumnFamily = rectangularFamily ;
                LoadRectangularColumnParameters(rectangularFamily) ;
            }
        }

        if (! string.IsNullOrEmpty(settings.CircularColumnFamilyId)) {
            var circularFamily = AllColumnFamilies.FirstOrDefault(f => f.UniqueId == settings.CircularColumnFamilyId) ;
            if (circularFamily != null) {
                SelectedCircularColumnFamily = circularFamily ;
                LoadCircularColumnParameters(circularFamily) ;
            }
        }

        // Load parameters
        if (! string.IsNullOrEmpty(settings.WidthParameter)) {
            WidthParameter = settings.WidthParameter ;
        }

        if (! string.IsNullOrEmpty(settings.HeightParameter)) {
            HeightParameter = settings.HeightParameter ;
        }

        if (! string.IsNullOrEmpty(settings.DiameterParameter)) {
            DiameterParameter = settings.DiameterParameter ;
        }

        // Load levels (must be after LoadLevels is called)
        if (! string.IsNullOrEmpty(settings.BaseLevelId)) {
            var baseLevel = AllLevels.FirstOrDefault(l => l.UniqueId == settings.BaseLevelId) ;
            if (baseLevel != null) {
                BaseLevel = baseLevel ;
            }
        }

        if (! string.IsNullOrEmpty(settings.TopLevelId)) {
            var topLevel = AllLevels.FirstOrDefault(l => l.UniqueId == settings.TopLevelId) ;
            if (topLevel != null) {
                TopLevel = topLevel ;
            }
        }

        // Load offsets
        BaseOffsetDisplay = settings.BaseOffsetDisplay ;
        TopOffsetDisplay = settings.TopOffsetDisplay ;
    }

    /// <summary>
    ///     Creates settings object from current ViewModel state
    /// </summary>
    protected override ColumnFromCadSettings CreateSettings() =>
        new()
        {
            SelectedLayer = SelectedLayer,
            IsModelByHatch = IsModelByHatch,
            RectangularColumnFamilyId = SelectedRectangularColumnFamily?.UniqueId,
            CircularColumnFamilyId = SelectedCircularColumnFamily?.UniqueId,
            WidthParameter = WidthParameter,
            HeightParameter = HeightParameter,
            DiameterParameter = DiameterParameter,
            BaseLevelId = BaseLevel?.UniqueId,
            TopLevelId = TopLevel?.UniqueId,
            BaseOffsetDisplay = BaseOffsetDisplay,
            TopOffsetDisplay = TopOffsetDisplay
        } ;

    #endregion

    #region Private Methods - Initialization

    /// <summary>
    ///     Initializes data for the view model
    /// </summary>
    protected override void OnDataInitialized()
    {
        AllLayerNames = new ObservableCollection<string>(Context.LayerNames) ;
        SelectedLayer = AllLayerNames[0] ;

        LoadColumnFamilies() ;

        LoadLevels() ;
    }

    /// <summary>
    ///     Loads column families from context (business data already extracted)
    /// </summary>
    private void LoadColumnFamilies()
    {
        AllColumnFamilies = new ObservableCollection<Family>(Context.ColumnFamilies) ;
        SelectedRectangularColumnFamily = AllColumnFamilies.First() ;
        SelectedCircularColumnFamily = AllColumnFamilies.First() ;

        LoadRectangularColumnParameters(SelectedRectangularColumnFamily) ;
        LoadCircularColumnParameters(SelectedCircularColumnFamily) ;
    }

    /// <summary>
    ///     Loads rectangular column type parameters from context (business data already extracted)
    /// </summary>
    private void LoadRectangularColumnParameters(Family family)
    {
        // Get parameters from context (business data already extracted)
        var allParameters = Context.FamilyNumericParameters[family.Id] ;
        AllRectangularColumnTypeParameters = new ObservableCollection<string>(allParameters) ;

        WidthParameter = AllRectangularColumnTypeParameters[0] ;
        HeightParameter = AllRectangularColumnTypeParameters.Count > 1
            ? AllRectangularColumnTypeParameters[1]
            : AllRectangularColumnTypeParameters[0] ;
    }

    /// <summary>
    ///     Loads circular column type parameters from context (business data already extracted)
    /// </summary>
    private void LoadCircularColumnParameters(Family family)
    {
        // Get parameters from context (business data already extracted)
        var allParameters = Context.FamilyNumericParameters[family.Id] ;
        AllCircularColumnTypeParameters = new ObservableCollection<string>(allParameters) ;

        DiameterParameter = AllCircularColumnTypeParameters[0] ;
    }

    /// <summary>
    ///     Loads levels from document
    /// </summary>
    private void LoadLevels()
    {
        var levels = RevitDocument.Document
            .GetAllElements<Level>()
            .OrderBy(level => level.Elevation)
            .ToList() ;

        AllLevels = new ObservableCollection<Level>(levels) ;

        if (AllLevels.Count <= 0) {
            return ;
        }

        BaseLevel = AllLevels[0] ;
        TopLevel = AllLevels.Count > 1 ? AllLevels[1] : AllLevels[0] ;
    }

    #endregion

    #region Validation

    /// <summary>
    ///     Validates input before execution
    /// </summary>
    private bool ValidateInput()
    {
        if (string.IsNullOrEmpty(SelectedLayer)) {
            ShowError(ResourceHelper.GetString("ValidationPleaseSelectLayer")) ;
            return false ;
        }

        if (SelectedRectangularColumnFamily == null) {
            ShowError(ResourceHelper.GetString("ValidationPleaseSelectRectangularColumnFamily")) ;
            return false ;
        }

        if (SelectedCircularColumnFamily == null) {
            ShowError(ResourceHelper.GetString("ValidationPleaseSelectCircularColumnFamily")) ;
            return false ;
        }

        if (string.IsNullOrEmpty(WidthParameter)) {
            ShowError(ResourceHelper.GetString("ValidationPleaseSelectWidthParameter")) ;
            return false ;
        }

        if (string.IsNullOrEmpty(HeightParameter)) {
            ShowError(ResourceHelper.GetString("ValidationPleaseSelectHeightParameter")) ;
            return false ;
        }

        if (string.IsNullOrEmpty(DiameterParameter)) {
            ShowError(ResourceHelper.GetString("ValidationPleaseSelectDiameterParameter")) ;
            return false ;
        }

        if (BaseLevel == null) {
            ShowError(ResourceHelper.GetString("ValidationPleaseSelectBaseLevel")) ;
            return false ;
        }

        if (TopLevel == null) {
            ShowError(ResourceHelper.GetString("ValidationPleaseSelectTopLevel")) ;
            return false ;
        }

        return true ;
    }

    #endregion
}
