using System.Globalization ;

namespace Sonny.ResourceManager ;

/// <summary>
///     Extension methods for LanguageCode enum
/// </summary>
public static class LanguageCodeExtensions
{
    /// <summary>
    ///     Converts LanguageCode enum to ISO 639-1 two-letter language code string
    /// </summary>
    /// <param name="languageCode">Language code enum value</param>
    /// <returns>Two-letter language code (e.g., "en", "vi", "ja")</returns>
    public static string ToCodeString(this LanguageCode languageCode) =>
        languageCode switch
        {
            LanguageCode.En => "en",
            LanguageCode.Vi => "vi",
            LanguageCode.Ja => "ja",
            LanguageCode.Es => "es",
            LanguageCode.Id => "id",
            LanguageCode.Th => "th",
            LanguageCode.Km => "km",
            LanguageCode.Zh => "zh",
            LanguageCode.Ko => "ko",
            _ => "en"
        } ;

    /// <summary>
    ///     Converts ISO 639-1 two-letter language code string to LanguageCode enum
    /// </summary>
    /// <param name="codeString">Two-letter language code (e.g., "en", "vi", "ja")</param>
    /// <returns>LanguageCode enum value, defaults to En if not recognized</returns>
    public static LanguageCode ToLanguageCode(this string codeString)
    {
        if (string.IsNullOrWhiteSpace(codeString)) {
            return LanguageCode.En ;
        }

        var normalized = codeString.ToLowerInvariant() ;
        return normalized switch
        {
            "en" => LanguageCode.En,
            "vi" => LanguageCode.Vi,
            "ja" => LanguageCode.Ja,
            "es" => LanguageCode.Es,
            "id" => LanguageCode.Id,
            "th" => LanguageCode.Th,
            "km" => LanguageCode.Km,
            "zh" => LanguageCode.Zh,
            "ko" => LanguageCode.Ko,
            _ => LanguageCode.En
        } ;
    }

    /// <summary>
    ///     Converts LanguageCode enum to CultureInfo
    /// </summary>
    /// <param name="languageCode">Language code enum value</param>
    /// <returns>CultureInfo for the language code</returns>
    public static CultureInfo ToCultureInfo(this LanguageCode languageCode)
    {
        try {
            return new CultureInfo(languageCode.ToCodeString()) ;
        }
        catch {
            return CultureInfo.GetCultureInfo("en") ;
        }
    }
}
