using System.Collections.ObjectModel ;
using Sonny.Application.Core.Bases ;
using Sonny.Application.Core.Interfaces ;
using Sonny.Application.Features.Settings.Models ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Features.Settings.ViewModels ;

/// <summary>
///     ViewModel for Settings dialog
/// </summary>
public partial class SettingsViewModel : BaseViewModel
{
    #region Constructor

    /// <summary>
    ///     Initializes a new instance of SettingsViewModel
    /// </summary>
    /// <param name="commonServices">Common services container</param>
    public SettingsViewModel(ICommonServices commonServices) : base(commonServices)
    {
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
        try
        {
            if (SelectedUnitOption != null)
            {
                SettingsService.SetDisplayUnit(SelectedUnitOption.UnitTypeId) ;
            }

            if (SelectedLanguageOption != null)
            {
                SettingsService.SetLanguage(SelectedLanguageOption.LanguageCode) ;
                // Change language in ResourceDictionaryManager
                ResourceDictionaryManager.Instance.ChangeLanguage(SelectedLanguageOption.LanguageCode) ;
            }

            ShowInfo("Settings saved successfully") ;
            CloseWindow() ;
        }
        catch (Exception ex)
        {
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
    private void InitializeUnitOptions()
    {
        UnitOptions = new ObservableCollection<UnitOption>
        {
            new("Millimeters (mm)",
                UnitTypeId.Millimeters),
            new("Centimeters (cm)",
                UnitTypeId.Centimeters),
            new("Meters (m)",
                UnitTypeId.Meters),
            new("Feet (ft)",
                UnitTypeId.Feet),
            new("Inches (in)",
                UnitTypeId.Inches)
        } ;
    }

    /// <summary>
    ///     Initialize available language options
    /// </summary>
    private void InitializeLanguageOptions()
    {
        LanguageOptions =
        [
            new LanguageOption("English",
                LanguageCode.En),
            new LanguageOption("Vietnamese",
                LanguageCode.Vi)
        ] ;
    }

    /// <summary>
    ///     Load current settings
    /// </summary>
    private void LoadCurrentSettings()
    {
        var currentUnit = SettingsService.GetDisplayUnit(RevitDocument.Document) ;
        SelectedUnitOption = UnitOptions.FirstOrDefault(u => u.UnitTypeId.TypeId == currentUnit.TypeId) ;

        var currentLanguage = SettingsService.GetLanguage() ;
        SelectedLanguageOption = LanguageOptions.FirstOrDefault(l => l.LanguageCode == currentLanguage) ;
    }

    #endregion
}
