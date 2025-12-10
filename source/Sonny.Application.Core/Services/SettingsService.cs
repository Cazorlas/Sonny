using System.Text.Json ;
using Sonny.Application.Core.Interfaces ;

namespace Sonny.Application.Core.Services ;

/// <summary>
///     Settings service implementation using JSON file storage
/// </summary>
public class SettingsService : ISettingsService
{
    private const string SettingsFileName = "SonnySettings.json" ;
    private readonly string _settingsFilePath ;
    private ForgeTypeId? _cachedDisplayUnit ;

    /// <summary>
    ///     Initializes a new instance of SettingsService
    /// </summary>
    public SettingsService()
    {
        // Store settings in user's AppData folder
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) ;
        var sonnyFolder = Path.Combine(appDataPath, "Sonny") ;
        Directory.CreateDirectory(sonnyFolder) ;
        _settingsFilePath = Path.Combine(sonnyFolder, SettingsFileName) ;
    }

    /// <summary>
    ///     Event raised when display unit setting changes
    /// </summary>
    public event EventHandler<ForgeTypeId>? DisplayUnitChanged ;

    /// <summary>
    ///     Get the user-selected display unit preference
    /// </summary>
    public ForgeTypeId GetDisplayUnit(Document document)
    {
        // Return cached value if available
        if (_cachedDisplayUnit != null)
        {
            return _cachedDisplayUnit ;
        }

        // Try to load from file
        if (File.Exists(_settingsFilePath))
        {
            try
            {
                var json = File.ReadAllText(_settingsFilePath) ;
                var settings = JsonSerializer.Deserialize<SettingsData>(json) ;

                if (settings?.DisplayUnitTypeId != null)
                {
                    var unitTypeId = new ForgeTypeId(settings.DisplayUnitTypeId) ;
                    _cachedDisplayUnit = unitTypeId ;
                    return unitTypeId ;
                }
            }
            catch
            {
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

        try
        {
            var settings = new SettingsData
            {
                DisplayUnitTypeId = displayUnit.TypeId
            } ;

            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }) ;
            File.WriteAllText(_settingsFilePath, json) ;

            // Raise event to notify subscribers
            DisplayUnitChanged?.Invoke(this, displayUnit) ;
        }
        catch
        {
            // Log error but don't throw - settings are not critical
        }
    }

    /// <summary>
    ///     Get default display unit based on document settings
    /// </summary>
    private static ForgeTypeId GetDefaultDisplayUnit(Document document)
    {
        var isMetric = document.DisplayUnitSystem == DisplayUnit.METRIC ;
        return isMetric ? UnitTypeId.Millimeters : UnitTypeId.Feet ;
    }

    /// <summary>
    ///     Settings data structure for JSON serialization
    /// </summary>
    private class SettingsData
    {
        public string? DisplayUnitTypeId { get ; set ; }
    }
}

