using Sonny.Application.Domain.Entities.Settings ;
using Sonny.Application.Domain.Services ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Infrastructure.Revit.Implements ;

/// <summary>
///     Handles language change events from ISettingsService and updates ResourceDictionaryManager
/// </summary>
public class LanguageChangeHandler
{
    /// <summary>
    ///     Initializes a new instance of LanguageChangeHandler
    /// </summary>
    /// <param name="settingsService">Settings service to subscribe to language change events</param>
    public LanguageChangeHandler(ISettingsService settingsService) =>
        // Subscribe to language change event
        settingsService.LanguageChanged += OnLanguageChanged ;

    private void OnLanguageChanged(object? sender,
        AppLanguageCode languageCode)
    {
        // Convert Domain enum to ResourceManager enum
        var resourceManagerLanguageCode = LanguageCodeConverter.ToResourceManagerLanguageCode(languageCode) ;

        // Update ResourceDictionaryManager
        ResourceDictionaryManager.Instance.ChangeLanguage(resourceManagerLanguageCode) ;
    }
}
