using System.Collections.ObjectModel ;
using Revit.Async ;
using Serilog ;
using SonnyApplication.Features.AutoColumnDimension.Interfaces ;
using SonnyApplication.Interfaces ;

namespace SonnyApplication.Features.AutoColumnDimension.ViewModels ;

public partial class AutoColumnDimensionViewModel : ObservableObject
{
    #region Constructor

    public AutoColumnDimensionViewModel(IRevitDocument revitDocument,
        IMessageService messageService,
        ILogger logger,
        IAutoColumnDimensionHandler handler)
    {
        RevitDocument = revitDocument ;
        MessageService = messageService ;
        Logger = logger ;
        Handler = handler ;

        // Initialize data synchronously (using RevitDocument service directly)
        InitializeData() ;
    }

    #endregion

    #region Services (Dependency Injection)

    private IRevitDocument RevitDocument { get ; }

    private IMessageService MessageService { get ; }

    private ILogger Logger { get ; }

    private IAutoColumnDimensionHandler Handler { get ; }

    #endregion

    #region MVVM Bindings (Properties for UI Binding)

    [ObservableProperty]
    private DimensionType? selectedDimensionType ;

    public ObservableCollection<DimensionType> DimensionTypes { get ; set ; } = [] ;

    [ObservableProperty]
    private double snapDistance ;

    #endregion

    #region Private Fields (Class Internal State)

    public Action? CloseWindowAction { get ; set ; }

    #endregion

    #region Commands

    [RelayCommand]
    private async Task Run()
    {
        try
        {
            await RevitTask.RunAsync(() => Handler.Execute(
                RevitDocument,
                SnapDistance,
                SelectedDimensionType)) ;

            // Close window after successful execution
            CloseWindow() ;
        }
        catch (Exception ex)
        {
            Logger.Error(ex,
                "Error occurred during dimension creation") ;
            MessageService.ShowError($"An error occurred: {ex.Message}") ;
        }
    }

    #endregion

    #region Event Handlers

    partial void OnSelectedDimensionTypeChanged(DimensionType? value)
    {
        UpdateSnapDistanceFromDimensionType() ;
    }

    #endregion

    #region Private Methods - Initialization

    private void InitializeData()
    {
        try
        {
            // Get dimension types using RevitDocument service (already in Revit API context)
            var dimensionTypes = RevitDocument.GetDimensionTypes() ;

            DimensionTypes = new ObservableCollection<DimensionType>(dimensionTypes) ;
            SelectedDimensionType = DimensionTypes.LastOrDefault() ;

            // Update snap distance
            UpdateSnapDistanceFromDimensionType() ;
        }
        catch (Exception ex)
        {
            Logger.Error(ex,
                "Failed to initialize dimension types") ;
            MessageService.ShowError($"Failed to initialize dimension types: {ex.Message}") ;
        }
    }

    #endregion

    #region Private Methods - UI Updates

    private void UpdateSnapDistanceFromDimensionType()
    {
        if (SelectedDimensionType is not { } dimensionType)
        {
            return ;
        }

        try
        {
            if (RevitDocument.ActiveView == null)
            {
                return ;
            }

            var parameter = dimensionType.FindParameter(BuiltInParameter.DIM_STYLE_DIM_LINE_SNAP_DIST) ;
            if (parameter is not { StorageType: StorageType.Double })
            {
                return ;
            }

            var snapDistanceValue = parameter.AsDouble() ;
            SnapDistance = snapDistanceValue * RevitDocument.ActiveView.Scale ;
        }
        catch (Exception ex)
        {
            Logger.Warning(ex,
                "Failed to update snap distance") ;
        }
    }

    private void CloseWindow()
    {
        CloseWindowAction?.Invoke() ;
    }

    #endregion
}
