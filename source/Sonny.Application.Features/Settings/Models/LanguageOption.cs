using Sonny.ResourceManager ;

namespace Sonny.Application.Features.Settings.Models ;

/// <summary>
///     Represents a language option for display in settings
/// </summary>
public class LanguageOption
{
    /// <summary>
    ///     Initializes a new instance of LanguageOption
    /// </summary>
    /// <param name="displayName">Display name (e.g., "English", "Vietnamese")</param>
    /// <param name="languageCode">Language code enum</param>
    public LanguageOption(string displayName,
        LanguageCode languageCode)
    {
        DisplayName = displayName ;
        LanguageCode = languageCode ;
    }

    /// <summary>
    ///     Display name for the language
    /// </summary>
    public string DisplayName { get ; }

    /// <summary>
    ///     Language code enum
    /// </summary>
    public LanguageCode LanguageCode { get ; }

    /// <summary>
    ///     String representation for display
    /// </summary>
    public override string ToString() => DisplayName ;
}
