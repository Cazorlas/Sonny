namespace Sonny.ResourceManager ;

/// <summary>
///     Manages registration and storage of ResourceDictionary configurations
/// </summary>
/// <remarks>
///     This class is responsible for registering, storing, and querying resource configurations.
///     It follows Single Responsibility Principle by focusing solely on resource registration management.
/// </remarks>
internal class ResourceRegistry
{
    private readonly Dictionary<string, ResourceConfig> _registeredResources = new() ;

    /// <summary>
    ///     Register a ResourceDictionary configuration
    /// </summary>
    /// <param name="assemblyName">Assembly name where resource is located</param>
    /// <param name="resourcePathPattern">
    ///     Base path without extension. Example: "Languages/AlphaRibbon/AlphaRibbon" (will
    ///     become "Languages/AlphaRibbon/AlphaRibbon.vi.xaml")
    /// </param>
    /// <param name="defaultLanguageCode">Default language code enum if resource not found</param>
    /// <returns>Generated resource ID (format: "{assemblyName}:{resourcePathPattern}")</returns>
    public string RegisterResource(string assemblyName,
        string resourcePathPattern,
        LanguageCode defaultLanguageCode = LanguageCode.En)
    {
        if (string.IsNullOrWhiteSpace(assemblyName)) {
            throw new ArgumentException("AssemblyName cannot be null or empty",
                nameof( assemblyName )) ;
        }

        if (string.IsNullOrWhiteSpace(resourcePathPattern)) {
            throw new ArgumentException(
                "ResourcePathPattern cannot be null or empty. Please provide a base path without extension. Example: \"Languages/AlphaRibbon/AlphaRibbon\"",
                nameof( resourcePathPattern )) ;
        }

        // Auto-generate resource ID from assembly name and path pattern
        var resourceId = $"{assemblyName}:{resourcePathPattern}" ;

        _registeredResources[resourceId] = new ResourceConfig
        {
            ResourceId = resourceId,
            AssemblyName = assemblyName,
            ResourcePathPattern = resourcePathPattern,
            DefaultLanguageCode = defaultLanguageCode
        } ;

        return resourceId ;
    }

    /// <summary>
    ///     Check if a resource is registered
    /// </summary>
    /// <param name="resourceId">Resource identifier</param>
    /// <returns>True if registered</returns>
    public bool IsRegistered(string resourceId) => _registeredResources.ContainsKey(resourceId) ;

    /// <summary>
    ///     Get resource configuration by resource ID
    /// </summary>
    /// <param name="resourceId">Resource identifier</param>
    /// <returns>ResourceConfig if found, null otherwise</returns>
    public ResourceConfig? GetConfig(string resourceId)
#if NETCOREAPP
        => _registeredResources.GetValueOrDefault(resourceId);
#else
        =>
            _registeredResources.TryGetValue(resourceId,
                out var config)
                ? config
                : null ;
#endif

    /// <summary>
    ///     Get all registered resource IDs
    /// </summary>
    /// <returns>List of resource IDs</returns>
    internal IEnumerable<string> GetRegisteredResourceIds() => _registeredResources.Keys ;

    /// <summary>
    ///     Get all registered resource configurations
    /// </summary>
    /// <returns>List of ResourceConfig</returns>
    public IEnumerable<ResourceConfig> GetAllConfigs() => _registeredResources.Values ;
}
