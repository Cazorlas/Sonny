using System.Windows ;
using Serilog ;
using Sonny.Application.Domain.Entities.Settings ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.Presentation.Services ;

namespace Sonny.Application.Presentation.Bases ;

/// <summary>
///     Base ViewModel with common dependencies for Sonny Application
/// </summary>
public abstract class BaseViewModel : ObservableObject
{
    #region Constructor

    /// <summary>
    ///     Initializes a new instance of BaseViewModel
    /// </summary>
    /// <param name="commonServices">Common services container</param>
    /// <param name="displayUnitProvider">Display unit provider</param>
    protected BaseViewModel(ICommonServices commonServices,
        IDisplayUnitProvider displayUnitProvider)
    {
        MessageService = commonServices.MessageService ;
        Logger = commonServices.Logger ;
        UnitConverter = commonServices.UnitConverter ;
        SettingsService = commonServices.SettingsService ;
        ResourceHelper = commonServices.ResourceHelper ;

        // Initialize display unit from user settings (or default)
        DisplayUnit = SettingsService.GetDisplayUnitOrDefault(displayUnitProvider.GetDefaultDisplayUnit) ;

        // Subscribe to display unit changes
        SettingsService.DisplayUnitChanged += OnDisplayUnitChanged ;
    }

    /// <summary>
    ///     Handle display unit changed event
    /// </summary>
    private void OnDisplayUnitChanged(object? sender,
        AppDisplayUnit newUnit)
    {
        var oldUnit = DisplayUnit ;
        DisplayUnit = newUnit ;
        OnPropertyChanged(nameof( DisplayUnit )) ;
        OnPropertyChanged(nameof( DisplayUnitName )) ;

        // Allow derived classes to handle unit conversion
        OnDisplayUnitChanged(oldUnit,
            newUnit) ;
    }

    /// <summary>
    ///     Called when display unit changes, allowing derived classes to convert values
    /// </summary>
    /// <param name="oldUnit">Previous display unit</param>
    /// <param name="newUnit">New display unit</param>
    protected virtual void OnDisplayUnitChanged(AppDisplayUnit oldUnit,
        AppDisplayUnit newUnit)
    {
        // Override in derived classes to convert values when unit changes
    }

    #endregion

    #region Common Services (Dependency Injection)

    protected IMessageService MessageService { get ; }

    protected ILogger Logger { get ; }

    /// <summary>
    ///     Unit converter for converting between display units and internal units
    /// </summary>
    protected IUnitConverter UnitConverter { get ; }

    /// <summary>
    ///     Settings service for managing application preferences
    /// </summary>
    protected ISettingsService SettingsService { get ; }

    protected IResourceHelper ResourceHelper { get ; }

    #endregion

    #region Common Properties

    /// <summary>
    ///     The window (set by View)
    /// </summary>
    public Window? Window { get ; set ; }

    /// <summary>
    ///     Display unit type (default: millimeters for metric, feet for imperial)
    /// </summary>
    protected AppDisplayUnit DisplayUnit { get ; private set ; }

    /// <summary>
    ///     Display unit name for UI (e.g., "mm", "cm", "ft")
    /// </summary>
    public string DisplayUnitName => UnitConverter.GetUnitDisplayName(DisplayUnit) ;

    #endregion

    #region Common Helper Methods

    /// <summary>
    ///     Close the window
    /// </summary>
    protected void CloseWindow() => Window?.Close() ;

    /// <summary>
    ///     Log information message
    /// </summary>
    protected void LogInfo(string message) => Logger.Information(message) ;

    /// <summary>
    ///     Log warning message
    /// </summary>
    protected void LogWarning(string message) => Logger.Warning(message) ;

    /// <summary>
    ///     Log error message
    /// </summary>
    protected void LogError(string message,
        Exception? ex = null)
    {
        if (ex != null) {
            Logger.Error(ex,
                message) ;
        }
        else {
            Logger.Error(message) ;
        }
    }

    /// <summary>
    ///     Show error message to user
    /// </summary>
    protected void ShowError(string message) => MessageService.ShowError(message) ;

    /// <summary>
    ///     Show info message to user
    /// </summary>
    protected void ShowInfo(string message) => MessageService.ShowInfo(message) ;

    /// <summary>
    ///     Show warning message to user
    /// </summary>
    protected void ShowWarning(string message) => MessageService.ShowWarning(message) ;

    #endregion
}
