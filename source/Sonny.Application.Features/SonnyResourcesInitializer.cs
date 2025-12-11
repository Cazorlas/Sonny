using Sonny.ResourceManager ;

namespace Sonny.Application.Features ;

/// <summary>
///     Initializes and registers all Sonny resources with ResourceDictionaryManager
///     Automatically loads resources when assembly is loaded
/// </summary>
public static class SonnyResourcesInitializer
{
    private static bool s_isInitialized ;

    /// <summary>
    ///     Initialize all Sonny resources
    ///     Should be called once when application starts
    /// </summary>
    /// <param name="languageCode">Language code to load resources</param>
    public static void Initialize(LanguageCode languageCode = LanguageCode.En)
    {
        if (s_isInitialized)
        {
            return ;
        }

        RegisterAllResources(languageCode) ;
        LoadAllResources(languageCode) ;

        s_isInitialized = true ;
    }

    /// <summary>
    ///     Register all Sonny resources with ResourceDictionaryManager
    /// </summary>
    private static void RegisterAllResources(LanguageCode languageCode)
    {
        var manager = ResourceDictionaryManager.Instance ;

        // Register AutoColumnDimension resource
        manager.RegisterResource("Sonny.Application.Features",
            "Resources/Languages/AutoColumnDimension/AutoColumnDimension",
            defaultLanguageCode: languageCode) ;

        // TODO: Add more resources here as needed
        // Example:
        // manager.RegisterResource(
        //     assemblyName: "Sonny.Application.Features",
        //     resourcePathPattern: "Languages/NewModule/NewResource",
        //     defaultLanguageCode: LanguageCode.En
        // );
    }

    /// <summary>
    ///     Load all registered resources
    /// </summary>
    /// <param name="languageCode">Language code to load</param>
    public static void LoadAllResources(LanguageCode languageCode = LanguageCode.En)
    {
        var manager = ResourceDictionaryManager.Instance ;
        manager.LoadAllResources(languageCode) ;
    }
}
