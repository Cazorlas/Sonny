using System.Collections.ObjectModel ;
using Revit.Async ;
using Sonny.Application.Bases ;
using Sonny.Application.Features.AutoColumnDimension.Interfaces ;
using Sonny.Application.Interfaces ;

namespace Sonny.Application.Features.AutoColumnDimension.ViewModels ;

public partial class AutoColumnDimensionViewModel : BaseViewModel
{
    #region Constructor

    public AutoColumnDimensionViewModel(ICommonServices commonServices,
        IAutoColumnDimensionHandler handler) : base(commonServices)
    {
        Handler = handler ;

        // Initialize data synchronously (using RevitDocument service directly)
        InitializeData() ;
    }

    #endregion

    #region Feature-Specific Services

    /// <summary>
    ///     Handler for auto column dimension feature
    /// </summary>
    private IAutoColumnDimensionHandler Handler { get ; }

    #endregion

    #region Commands

    [RelayCommand]
    private async Task Run()
    {
        try
        {
            // Convert display unit to internal unit (feet) before passing to handler
            await RevitTask.RunAsync(() => Handler.Execute(RevitDocument,
                SnapDistanceInternal * RevitDocument.ActiveView.Scale,
                SelectedDimensionType)) ;

            // Close window after successful execution
            CloseWindow() ;
        }
        catch (Exception ex)
        {
            LogError("Error occurred during dimension creation",
                ex) ;
            ShowError($"An error occurred: {ex.Message}") ;
        }
    }

    #endregion

    #region Event Handlers

    partial void OnSelectedDimensionTypeChanged(DimensionType? value) => UpdateSnapDistanceFromDimensionType() ;

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
            LogError("Failed to initialize dimension types",
                ex) ;
            ShowError($"Failed to initialize dimension types: {ex.Message}") ;
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
            var parameter = dimensionType.FindParameter(BuiltInParameter.DIM_STYLE_DIM_LINE_SNAP_DIST) ;
            if (parameter is not { StorageType: StorageType.Double })
            {
                return ;
            }

            // Get value in feet from Revit parameter
            var snapDistanceFeet = parameter.AsDouble() ;

            // Convert from internal unit (feet) to display unit (mm, cm, etc.)
            SnapDistanceDisplay = UnitConverter.FromInternalUnit(snapDistanceFeet,
                DisplayUnit) ;
        }
        catch (Exception ex)
        {
            LogWarning($"Failed to update snap distance: {ex.Message}") ;
        }
    }

    #endregion

    #region MVVM Bindings (Properties for UI Binding)

    [ObservableProperty]
    private DimensionType? selectedDimensionType ;

    public ObservableCollection<DimensionType> DimensionTypes { get ; set ; } = [] ;

    /// <summary>
    ///     Snap distance in display unit (mm, cm, m, etc.) for UI binding
    /// </summary>
    [ObservableProperty]
    private double snapDistanceDisplay ;

    /// <summary>
    ///     Snap distance in internal unit (feet) for calculation
    /// </summary>
    private double SnapDistanceInternal =>
        UnitConverter.ToInternalUnit(snapDistanceDisplay,
            DisplayUnit) ;

    #endregion
}
