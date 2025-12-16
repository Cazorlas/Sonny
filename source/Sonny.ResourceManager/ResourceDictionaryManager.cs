using System.Globalization ;
using System.Windows ;

namespace Sonny.ResourceManager ;

/// <summary>
///     Central manager for loading ResourceDictionary into Application.Resources
///     Supports multi-language and multiple resource dictionaries
///     Integrates with WPFLocalizeExtension for enhanced localization support
/// </summary>
/// <remarks>
///     This class provides a centralized facade for managing ResourceDictionary loading
///     for WPF applications with multi-language support. It coordinates between
///     ResourceRegistry, ResourceLoader, ResourcePathBuilder, and CultureManager
///     to provide a simple, unified API.
/// </remarks>
public class ResourceDictionaryManager
{
    #region Constructor

    private ResourceDictionaryManager()
    {
        _registry = new ResourceRegistry() ;
        var pathBuilder = new ResourcePathBuilder() ;
        _loader = new ResourceLoader(pathBuilder) ;
        _cultureManager = new CultureManager() ;

        // Subscribe to culture changed event to reload resources
        _cultureManager.CultureChanged += OnCultureChanged ;
    }

    #endregion

    #region Singleton

    /// <summary>
    ///     Gets the singleton instance of ResourceDictionaryManager
    /// </summary>
    public static ResourceDictionaryManager Instance => s_instance ??= new ResourceDictionaryManager() ;

    #endregion

    #region Events

    /// <summary>
    ///     Event raised when culture is changed
    /// </summary>
    public event EventHandler<CultureChangedEventArgs>? CultureChanged
    {
        add => _cultureManager.CultureChanged += value ;
        remove => _cultureManager.CultureChanged -= value ;
    }

    #endregion

    #region Registration Methods

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
        LanguageCode defaultLanguageCode = LanguageCode.En) =>
        _registry.RegisterResource(assemblyName,
            resourcePathPattern,
            defaultLanguageCode) ;

    #endregion

    #region Query Methods

    /// <summary>
    ///     Check if a resource is registered
    /// </summary>
    /// <param name="resourceId">Resource identifier</param>
    /// <returns>True if registered</returns>
    public bool IsRegistered(string resourceId) => _registry.IsRegistered(resourceId) ;

    #endregion

    #region Fields

    private static ResourceDictionaryManager? s_instance ;
    private readonly ResourceRegistry _registry ;
    private readonly ResourceLoader _loader ;
    private readonly CultureManager _cultureManager ;

    #endregion

    #region Properties

    /// <summary>
    ///     Gets or sets the current culture
    /// </summary>
    /// <remarks>
    ///     When set, automatically reloads all registered resources and updates WPFLocalizeExtension
    /// </remarks>
    public CultureInfo CurrentCulture
    {
        get => _cultureManager.CurrentCulture ;
        set => _cultureManager.CurrentCulture = value ;
    }

    /// <summary>
    ///     Gets the current language code as string (e.g., "en", "vi", "ja")
    /// </summary>
    public string CurrentLanguageCode => _cultureManager.CurrentLanguageCode ;

    /// <summary>
    ///     Gets the current language code as LanguageCode enum
    /// </summary>
    public LanguageCode CurrentLanguageCodeEnum => _cultureManager.CurrentLanguageCodeEnum ;

    #endregion

    #region Loading Methods

    /// <summary>
    ///     Load all registered ResourceDictionaries into Application.Resources
    /// </summary>
    /// <param name="languageCode">Language code enum (null = use current language)</param>
    public void LoadAllResources(LanguageCode? languageCode = null)
    {
        var langCode = languageCode ?? CurrentLanguageCodeEnum ;

        if (Application.Current == null)
        {
            return ;
        }

        var configs = _registry.GetAllConfigs() ;

        foreach (var config in configs)
        {
            _loader.LoadResource(config,
                langCode) ;
        }
    }

    /// <summary>
    ///     Unload a specific ResourceDictionary from Application.Resources
    /// </summary>
    /// <param name="resourceId">Resource identifier</param>
    /// <returns>True if unloaded successfully</returns>
    public bool UnloadResource(string resourceId) => _loader.UnloadResource(resourceId) ;

    #endregion

    #region Language Methods

    /// <summary>
    ///     Change language and reload all resources
    /// </summary>
    /// <param name="languageCode">Language code enum</param>
    public void ChangeLanguage(LanguageCode languageCode)
    {
        _cultureManager.ChangeLanguage(languageCode) ;
        ReloadAllResources() ;
    }

    /// <summary>
    ///     Change language using CultureInfo
    /// </summary>
    /// <param name="culture">Culture to set</param>
    public void ChangeLanguage(CultureInfo culture)
    {
        _cultureManager.ChangeLanguage(culture) ;
        ReloadAllResources() ;
    }

    #endregion

    #region Private Methods

    /// <summary>
    ///     Load a specific ResourceDictionary into Application.Resources
    /// </summary>
    /// <param name="resourceId">Resource identifier</param>
    /// <param name="languageCode">Language code enum (null = use current language)</param>
    /// <returns>True if loaded successfully</returns>
    private bool LoadResource(string resourceId,
        LanguageCode? languageCode = null)
    {
        var langCode = languageCode ?? CurrentLanguageCodeEnum ;

        var config = _registry.GetConfig(resourceId) ;
        if (config == null)
        {
            return false ;
        }

        return _loader.LoadResource(config,
            langCode) ;
    }

    /// <summary>
    ///     Reload all resources with current language
    /// </summary>
    private void ReloadAllResources()
    {
        foreach (var resourceId in _registry.GetRegisteredResourceIds())
        {
            LoadResource(resourceId,
                CurrentLanguageCodeEnum) ;
        }
    }

    /// <summary>
    ///     Handle culture changed event to reload resources
    /// </summary>
    private void OnCultureChanged(object? sender,
        CultureChangedEventArgs e) =>
        ReloadAllResources() ;

    #endregion
}
