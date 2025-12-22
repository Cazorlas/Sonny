namespace Sonny.Application.UI ;

/// <summary>
///     Manager for loading UI styles into Application.Resources
/// </summary>
/// <remarks>
///     In Revit, WPF Application.Current is managed by Revit itself.
///     This manager ensures styles are loaded into the correct WPF Application instance,
///     not the Revit Application (Autodesk.Revit.ApplicationServices.Application).
/// </remarks>
public static class UIStyleManager
{
    /// <summary>
    ///     Loads the default theme (Themes.xaml) into Application.Resources
    /// </summary>
    /// <returns>True if loaded successfully, false otherwise</returns>
    public static bool LoadTheme()
    {
        var app = GetOrCreateApplication() ;
        if (app == null) {
            return false ;
        }

        try {
            var themeUri = new Uri("/Sonny.Application.UI;component/Themes.xaml",
                UriKind.RelativeOrAbsolute) ;
            var themeDictionary = new System.Windows.ResourceDictionary { Source = themeUri } ;

            // Remove existing theme if present
            RemoveTheme(app) ;

            // Add theme to Application resources
            app.Resources.MergedDictionaries.Add(themeDictionary) ;

            return true ;
        }
        catch {
            return false ;
        }
    }

    /// <summary>
    ///     Removes the theme from the specified Application.Resources
    /// </summary>
    /// <param name="app">The WPF Application instance</param>
    private static void RemoveTheme(System.Windows.Application app)
    {
        var themeUri = new Uri("/Sonny.Application.UI;component/Themes.xaml",
            UriKind.RelativeOrAbsolute) ;
        var dictionariesToRemove = new List<System.Windows.ResourceDictionary>() ;

        foreach (var dictionary in app.Resources.MergedDictionaries) {
            if (dictionary.Source == themeUri) {
                dictionariesToRemove.Add(dictionary) ;
            }
        }

        foreach (var dictionary in dictionariesToRemove) {
            app.Resources.MergedDictionaries.Remove(dictionary) ;
        }
    }

    /// <summary>
    ///     Gets the WPF Application instance, ensuring it exists
    /// </summary>
    /// <returns>The current WPF Application instance, or null if cannot be created</returns>
    private static System.Windows.Application? GetOrCreateApplication()
    {
        // In Revit, System.Windows.Application.Current is usually initialized by Revit itself
        // If it exists, use it (this is the Revit's WPF Application)
        if (System.Windows.Application.Current != null) {
            return System.Windows.Application.Current ;
        }

        // If Application.Current is null, create a new one for standalone scenarios
        // This should rarely happen in Revit, but provides fallback for testing
        try {
            var app = new System.Windows.Application { ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown } ;
            return app ;
        }
        catch {
            // Application already exists in another thread or cannot be created
            return null ;
        }
    }
}
