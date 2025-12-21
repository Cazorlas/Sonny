namespace Sonny.Application.Core.Interfaces ;

/// <summary>
///     Generic interface for ViewModel-specific settings management
/// </summary>
/// <typeparam name="TSettings">Settings model type</typeparam>
public interface IViewModelSettingsService<TSettings> where TSettings : class, new()
{
    /// <summary>
    ///     Loads settings from storage
    /// </summary>
    /// <returns>Settings instance, or new instance if not found</returns>
    TSettings? LoadSettings() ;

    /// <summary>
    ///     Saves settings to storage
    /// </summary>
    /// <param name="settings">Settings to save</param>
    void SaveSettings(TSettings settings) ;
}
