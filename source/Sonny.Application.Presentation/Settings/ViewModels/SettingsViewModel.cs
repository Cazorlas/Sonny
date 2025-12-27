using System.Collections.ObjectModel ;
using Sonny.Application.Domain.Entities.Settings ;
using Sonny.Application.Domain.Entities.Settings.Models ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.Presentation.Bases ;
using Sonny.Application.Presentation.Services ;
using Sonny.Application.UseCases.Settings.Models ;

namespace Sonny.Application.Presentation.Settings.ViewModels ;

/// <summary>
///     ViewModel for Settings dialog
/// </summary>
public partial class SettingsViewModel : BaseViewModel
{
    #region Constructor

    private readonly IDisplayUnitProvider _displayUnitProvider ;

    /// <summary>
    ///     Initializes a new instance of SettingsViewModel
    /// </summary>
    /// <param name="commonServices">Common services container</param>
    /// <param name="displayUnitProvider">Display unit provider</param>
    public SettingsViewModel(ICommonServices commonServices,
        IDisplayUnitProvider displayUnitProvider) : base(commonServices,
        displayUnitProvider)
    {
        _displayUnitProvider = displayUnitProvider ;
        InitializeUnitOptions() ;
        InitializeLanguageOptions() ;
        LoadCurrentSettings() ;
    }

    #endregion

    #region Properties for UI Binding

    /// <summary>
    ///     Available unit options
    /// </summary>
    public ObservableCollection<UnitOption> UnitOptions { get ; private set ; } = [] ;

    /// <summary>
    ///     Selected unit option
    /// </summary>
    [ObservableProperty]
    private UnitOption? selectedUnitOption ;

    /// <summary>
    ///     Available language options
    /// </summary>
    public ObservableCollection<LanguageOption> LanguageOptions { get ; private set ; } = [] ;

    /// <summary>
    ///     Selected language option
    /// </summary>
    [ObservableProperty]
    private LanguageOption? selectedLanguageOption ;

    #endregion

    #region Commands

    /// <summary>
    ///     Save settings command
    /// </summary>
    [RelayCommand]
    private void Save()
    {
        try {
            if (SelectedUnitOption != null) {
                SettingsService.SetDisplayUnit(SelectedUnitOption.DisplayUnit) ;
            }

            if (SelectedLanguageOption != null) {
                SettingsService.SetLanguage(SelectedLanguageOption.LanguageCode) ;
            }

            ShowInfo("Settings saved successfully") ;
            CloseWindow() ;
        }
        catch (Exception ex) {
            LogError("Failed to save settings",
                ex) ;
            ShowError($"Failed to save settings: {ex.Message}") ;
        }
    }

    /// <summary>
    ///     Cancel command
    /// </summary>
    [RelayCommand]
    private void Cancel() => CloseWindow() ;

    #endregion

    #region Private Methods

    /// <summary>
    ///     Initialize available unit options
    /// </summary>
    private void InitializeUnitOptions() =>
        UnitOptions = new ObservableCollection<UnitOption>
        {
            new("Millimeters (mm)",
                AppDisplayUnit.Millimeters),
            new("Centimeters (cm)",
                AppDisplayUnit.Centimeters),
            new("Meters (m)",
                AppDisplayUnit.Meters),
            new("Feet (ft)",
                AppDisplayUnit.Feet),
            new("Inches (in)",
                AppDisplayUnit.Inches)
        } ;

    /// <summary>
    ///     Initialize available language options
    /// </summary>
    private void InitializeLanguageOptions() =>
        LanguageOptions =
        [
            new LanguageOption("English",
                AppLanguageCode.En),
            new LanguageOption("Vietnamese",
                AppLanguageCode.Vi)
        ] ;

    /// <summary>
    ///     Load current settings
    /// </summary>
    private void LoadCurrentSettings()
    {
        var currentUnit = SettingsService.GetDisplayUnitOrDefault(() => _displayUnitProvider.GetDefaultDisplayUnit()) ;
        SelectedUnitOption = UnitOptions.FirstOrDefault(u => u.DisplayUnit == currentUnit) ;

        var currentLanguage = SettingsService.GetLanguage() ;
        SelectedLanguageOption = LanguageOptions.FirstOrDefault(l => l.LanguageCode == currentLanguage) ;
    }

    #endregion
}
