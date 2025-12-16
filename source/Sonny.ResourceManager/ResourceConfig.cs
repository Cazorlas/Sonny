namespace Sonny.ResourceManager ;

/// <summary>
///     Configuration for a ResourceDictionary to be loaded
/// </summary>
internal class ResourceConfig
{
    /// <summary>
    ///     Unique identifier for this resource
    /// </summary>
    public string ResourceId { get ; set ; } = string.Empty ;

    /// <summary>
    ///     Assembly name where resource is located
    /// </summary>
    public string AssemblyName { get ; set ; } = string.Empty ;

    /// <summary>
    ///     Base resource path without extension. Example: "Languages/AlphaRibbon/AlphaRibbon" (will become
    ///     "Languages/AlphaRibbon/AlphaRibbon.vi.xaml")
    /// </summary>
    public string ResourcePathPattern { get ; set ; } = string.Empty ;

    /// <summary>
    ///     Default language code if resource not found
    /// </summary>
    public LanguageCode DefaultLanguageCode { get ; set ; } = LanguageCode.En ;
}
