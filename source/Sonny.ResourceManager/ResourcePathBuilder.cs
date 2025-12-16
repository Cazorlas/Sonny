namespace Sonny.ResourceManager ;

/// <summary>
///     Builds resource file paths from base path patterns
/// </summary>
/// <remarks>
///     This class is responsible for building resource paths by appending language code
///     to the base path. It follows Single Responsibility Principle by focusing solely
///     on path construction logic.
/// </remarks>
internal class ResourcePathBuilder
{
    /// <summary>
    ///     Build resource path by appending language code to base path
    /// </summary>
    /// <param name="config">Resource configuration</param>
    /// <param name="languageCode">Language code to use in path</param>
    /// <returns>Built resource path string (e.g., "Languages/AlphaRibbon/AlphaRibbon.vi.xaml")</returns>
    public string BuildPath(ResourceConfig config,
        LanguageCode languageCode)
    {
        var languageCodeString = languageCode.ToCodeString() ;
        return $"{config.ResourcePathPattern}.{languageCodeString}.xaml" ;
    }
}
