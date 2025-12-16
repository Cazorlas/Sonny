using System.Windows ;

namespace Sonny.ResourceManager ;

/// <summary>
///     Helper class for accessing localized resources from Application.Resources
///     Provides convenient methods to retrieve localized strings
/// </summary>
/// <remarks>
///     This class follows Single Responsibility Principle by focusing solely on
///     retrieving localized strings from Application.Resources. It can be used
///     by any project that uses ResourceDictionaryManager for localization.
/// </remarks>
public static class ResourceHelper
{
    /// <summary>
    ///     Get localized string from Application.Resources
    /// </summary>
    /// <param name="key">Resource key</param>
    /// <returns>Localized string or key if not found</returns>
    public static string GetString(string key)
    {
        if (Application.Current?.Resources.Contains(key) == true)
        {
            return Application.Current
                       .Resources[key]
                       ?.ToString()
                   ?? key ;
        }

        return key ;
    }

    /// <summary>
    ///     Get localized string with format arguments
    /// </summary>
    /// <param name="key">Resource key</param>
    /// <param name="args">Format arguments</param>
    /// <returns>Formatted localized string</returns>
    public static string GetString(string key,
        params object[] args)
    {
        var format = GetString(key) ;
        return args.Length > 0
            ? string.Format(format,
                args)
            : format ;
    }
}
