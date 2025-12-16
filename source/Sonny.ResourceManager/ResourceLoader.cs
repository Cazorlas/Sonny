using System.Windows ;

namespace Sonny.ResourceManager ;

/// <summary>
///     Loads and unloads ResourceDictionary instances into Application.Resources
/// </summary>
/// <remarks>
///     This class is responsible for the actual loading and unloading of ResourceDictionary
///     instances into WPF Application.Resources. It follows Single Responsibility Principle
///     by focusing solely on resource loading/unloading operations.
/// </remarks>
internal class ResourceLoader
{
    private readonly Dictionary<string, ResourceDictionary> _loadedResources = new() ;
    private readonly ResourcePathBuilder _pathBuilder ;

    /// <summary>
    ///     Initializes a new instance of ResourceLoader
    /// </summary>
    /// <param name="pathBuilder">Resource path builder instance</param>
    public ResourceLoader(ResourcePathBuilder pathBuilder) =>
        _pathBuilder = pathBuilder ?? throw new ArgumentNullException(nameof( pathBuilder )) ;

    /// <summary>
    ///     Load a ResourceDictionary into Application.Resources
    /// </summary>
    /// <param name="config">Resource configuration</param>
    /// <param name="languageCode">Language code to load</param>
    /// <returns>True if loaded successfully</returns>
    public bool LoadResource(ResourceConfig config,
        LanguageCode languageCode)
    {
        if (Application.Current == null)
        {
            return false ;
        }

        try
        {
            // Remove old resource if exists
            UnloadResource(config.ResourceId) ;

            // Build resource URI
            var path = _pathBuilder.BuildPath(config,
                languageCode) ;
            Uri resourceUri = new($"/{config.AssemblyName};component/{path}",
                UriKind.RelativeOrAbsolute) ;
            ResourceDictionary resourceDict = new() { Source = resourceUri } ;

            // Add to Application resources
            Application.Current.Resources.MergedDictionaries.Add(resourceDict) ;

            // Track loaded resource
            _loadedResources[config.ResourceId] = resourceDict ;

            return true ;
        }
        catch
        {
            // Try fallback language
            if (languageCode != config.DefaultLanguageCode)
            {
                return LoadResource(config,
                    config.DefaultLanguageCode) ;
            }

            return false ;
        }
    }

    /// <summary>
    ///     Unload a specific ResourceDictionary from Application.Resources
    /// </summary>
    /// <param name="resourceId">Resource identifier</param>
    /// <returns>True if unloaded successfully</returns>
    public bool UnloadResource(string resourceId)
    {
        if (Application.Current == null)
        {
            return false ;
        }

        // Try to find by tracked resource first
        if (_loadedResources.TryGetValue(resourceId,
                out var trackedDict))
        {
            Application.Current.Resources.MergedDictionaries.Remove(trackedDict) ;
            _loadedResources.Remove(resourceId) ;
            return true ;
        }

        // Fallback: search by resourceId in URI
        var oldDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source != null
            && d.Source.OriginalString.IndexOf(resourceId,
                StringComparison.OrdinalIgnoreCase)
            >= 0) ;

        if (oldDict != null)
        {
            Application.Current.Resources.MergedDictionaries.Remove(oldDict) ;
            return true ;
        }

        return false ;
    }
}
