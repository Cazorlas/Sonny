#if NETCOREAPP
using System.Text.Json ;
#else
using Newtonsoft.Json ;
#endif
using Sonny.Application.Core.Interfaces ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Core.Services ;

/// <summary>
///     Settings service implementation using JSON file storage
/// </summary>
public class SettingsService : ISettingsService
{
    private const string SettingsFileName = "SonnySettings.json" ;
    private readonly string _settingsFilePath ;
    private ForgeTypeId? _cachedDisplayUnit ;
    private LanguageCode? _cachedLanguage ;

    /// <summary>
    ///     Initializes a new instance of SettingsService
    /// </summary>
    public SettingsService()
    {
        // Store settings in user's AppData folder
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) ;
        var sonnyFolder = Path.Combine(appDataPath,
            "Sonny") ;
        Directory.CreateDirectory(sonnyFolder) ;
        _settingsFilePath = Path.Combine(sonnyFolder,
            SettingsFileName) ;
    }

    /// <summary>
    ///     Event raised when display unit setting changes
    /// </summary>
    public event EventHandler<ForgeTypeId>? DisplayUnitChanged ;

    /// <summary>
    ///     Event raised when language setting changes
    /// </summary>
    public event EventHandler<LanguageCode>? LanguageChanged ;

    /// <summary>
    ///     Get the user-selected display unit preference
    /// </summary>
    public ForgeTypeId GetDisplayUnit(Document document)
    {
        // Return cached value if available
        if (_cachedDisplayUnit != null) {
            return _cachedDisplayUnit ;
        }

        // Try to load from file
        if (File.Exists(_settingsFilePath)) {
            try {
                var settings = LoadSettingsData() ;

                if (settings?.DisplayUnitTypeId != null) {
                    var unitTypeId = new ForgeTypeId(settings.DisplayUnitTypeId) ;
                    _cachedDisplayUnit = unitTypeId ;
                    return unitTypeId ;
                }
            }
            catch {
                // If deserialization fails, fall back to default
            }
        }

        // Fall back to default based on document
        var defaultUnit = GetDefaultDisplayUnit(document) ;
        _cachedDisplayUnit = defaultUnit ;
        return defaultUnit ;
    }

    /// <summary>
    ///     Set the user-selected display unit preference
    /// </summary>
    public void SetDisplayUnit(ForgeTypeId displayUnit)
    {
        _cachedDisplayUnit = displayUnit ;

        try {
            // Load existing settings to preserve other settings
            var settings = LoadSettingsData() ;
            settings.DisplayUnitTypeId = displayUnit.TypeId ;

            SaveSettingsData(settings) ;

            // Raise event to notify subscribers
            DisplayUnitChanged?.Invoke(this,
                displayUnit) ;
        }
        catch {
            // Log error but don't throw - settings are not critical
        }
    }

    /// <summary>
    ///     Get the user-selected language preference
    /// </summary>
    public LanguageCode GetLanguage()
    {
        // Return cached value if available
        if (_cachedLanguage != null) {
            return _cachedLanguage.Value ;
        }

        // Try to load from file
        if (File.Exists(_settingsFilePath)) {
            try {
                var settings = LoadSettingsData() ;

                if (settings.LanguageCode != null
                    && Enum.TryParse<LanguageCode>(settings.LanguageCode,
                        out var languageCode)) {
                    _cachedLanguage = languageCode ;
                    return languageCode ;
                }
            }
            catch {
                // If deserialization fails, fall back to default
            }
        }

        // Fall back to default
        const LanguageCode defaultLanguage = LanguageCode.En ;
        _cachedLanguage = defaultLanguage ;
        return defaultLanguage ;
    }

    /// <summary>
    ///     Set the user-selected language preference
    /// </summary>
    public void SetLanguage(LanguageCode languageCode)
    {
        _cachedLanguage = languageCode ;

        try {
            // Load existing settings to preserve other settings
            var settings = LoadSettingsData() ;
            settings.LanguageCode = languageCode.ToString() ;

            SaveSettingsData(settings) ;

            // Raise event to notify subscribers
            LanguageChanged?.Invoke(this,
                languageCode) ;
        }
        catch {
            // Log error but don't throw - settings are not critical
        }
    }

    #region Private Methods

    /// <summary>
    ///     Get default display unit based on document settings
    /// </summary>
    private static ForgeTypeId GetDefaultDisplayUnit(Document document)
    {
        var isMetric = document.DisplayUnitSystem == DisplayUnit.METRIC ;
        return isMetric ? UnitTypeId.Millimeters : UnitTypeId.Feet ;
    }

    /// <summary>
    ///     Load settings data from file
    /// </summary>
    private SettingsData LoadSettingsData()
    {
        if (File.Exists(_settingsFilePath)) {
            try {
                var json = File.ReadAllText(_settingsFilePath) ;
#if NETCOREAPP
                return JsonSerializer.Deserialize<SettingsData>(json) ?? new SettingsData() ;
#else
                return JsonConvert.DeserializeObject<SettingsData>(json) ?? new SettingsData() ;
#endif
            }
            catch {
                return new SettingsData() ;
            }
        }

        return new SettingsData() ;
    }

    /// <summary>
    ///     Save settings data to file
    /// </summary>
    private void SaveSettingsData(SettingsData settings)
    {
#if NETCOREAPP
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }) ;
#else
        var json = JsonConvert.SerializeObject(settings,
            Formatting.Indented) ;
#endif
        File.WriteAllText(_settingsFilePath,
            json) ;
    }

    /// <summary>
    ///     Settings data structure for JSON serialization
    /// </summary>
    private class SettingsData
    {
        public string? DisplayUnitTypeId { get ; set ; }
        public string? LanguageCode { get ; set ; }
    }

    #endregion
}
