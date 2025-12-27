using Sonny.Application.Domain.Entities.Settings ;

namespace Sonny.Application.Domain.Services ;

/// <summary>
///     Interface for application settings management
/// </summary>
public interface ISettingsService
{
    /// <summary>
    ///     Get the user-selected display unit preference
    ///     Returns saved preference or null if not set
    /// </summary>
    /// <returns>Display unit type, or null if not set</returns>
    AppDisplayUnit? GetDisplayUnit() ;

    /// <summary>
    ///     Get the user-selected display unit preference with fallback to default
    /// </summary>
    /// <param name="defaultUnitProvider">Provider to get default unit if not set</param>
    /// <returns>Display unit type</returns>
    AppDisplayUnit GetDisplayUnitOrDefault(Func<AppDisplayUnit> defaultUnitProvider) ;

    /// <summary>
    ///     Set the user-selected display unit preference
    /// </summary>
    /// <param name="displayUnit">Display unit type to save</param>
    void SetDisplayUnit(AppDisplayUnit displayUnit) ;

    /// <summary>
    ///     Event raised when display unit setting changes
    /// </summary>
    event EventHandler<AppDisplayUnit>? DisplayUnitChanged ;

    /// <summary>
    ///     Get the user-selected language preference
    /// </summary>
    /// <returns>Language code enum</returns>
    AppLanguageCode GetLanguage() ;

    /// <summary>
    ///     Set the user-selected language preference
    /// </summary>
    /// <param name="languageCode">Language code enum to save</param>
    void SetLanguage(AppLanguageCode languageCode) ;

    /// <summary>
    ///     Event raised when language setting changes
    /// </summary>
    event EventHandler<AppLanguageCode>? LanguageChanged ;
}
