using Sonny.ResourceManager ;

namespace Sonny.Application.Core.Interfaces ;

/// <summary>
///     Interface for application settings management
/// </summary>
public interface ISettingsService
{
    /// <summary>
    ///     Get the user-selected display unit preference
    /// </summary>
    /// <param name="document">Revit document (for fallback to default)</param>
    /// <returns>Display unit type</returns>
    ForgeTypeId GetDisplayUnit(Document document) ;

    /// <summary>
    ///     Set the user-selected display unit preference
    /// </summary>
    /// <param name="displayUnit">Display unit type to save</param>
    void SetDisplayUnit(ForgeTypeId displayUnit) ;

    /// <summary>
    ///     Event raised when display unit setting changes
    /// </summary>
    event EventHandler<ForgeTypeId>? DisplayUnitChanged ;

    /// <summary>
    ///     Get the user-selected language preference
    /// </summary>
    /// <returns>Language code enum</returns>
    LanguageCode GetLanguage() ;

    /// <summary>
    ///     Set the user-selected language preference
    /// </summary>
    /// <param name="languageCode">Language code enum to save</param>
    void SetLanguage(LanguageCode languageCode) ;

    /// <summary>
    ///     Event raised when language setting changes
    /// </summary>
    event EventHandler<LanguageCode>? LanguageChanged ;
}
