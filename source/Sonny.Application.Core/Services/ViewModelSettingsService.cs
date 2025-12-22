#if NETCOREAPP
using System.Text.Json ;
#else
using Newtonsoft.Json ;
#endif
using Sonny.Application.Core.Interfaces ;

namespace Sonny.Application.Core.Services ;

public class ViewModelSettingsService<TSettings> : IViewModelSettingsService<TSettings> where TSettings : class, new()
{
    private readonly string _settingsFilePath ;

    public ViewModelSettingsService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) ;
        var sonnyFolder = Path.Combine(appDataPath,
            "Sonny") ;
        Directory.CreateDirectory(sonnyFolder) ;
        
        // Automatically generate filename from TSettings type name
        var settingsFileName = typeof(TSettings).Name + ".json" ;
        _settingsFilePath = Path.Combine(sonnyFolder,
            settingsFileName) ;
    }

    public TSettings? LoadSettings()
    {
        if (! File.Exists(_settingsFilePath))
        {
            return null ;
        }

        try
        {
            var json = File.ReadAllText(_settingsFilePath) ;
#if NETCOREAPP
            return JsonSerializer.Deserialize<TSettings>(json) ?? new TSettings() ;
#else
            return JsonConvert.DeserializeObject<TSettings>(json) ?? new TSettings() ;
#endif
        }
        catch
        {
            // If deserialization fails, return new instance
            return null ;
        }
    }

    public void SaveSettings(TSettings settings)
    {
        try
        {
#if NETCOREAPP
            var json = JsonSerializer.Serialize(settings,
                new JsonSerializerOptions { WriteIndented = true }) ;
#else
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented) ;
#endif
            File.WriteAllText(_settingsFilePath,
                json) ;
        }
        catch
        {
            // Log error but don't throw - settings are not critical
        }
    }
}
