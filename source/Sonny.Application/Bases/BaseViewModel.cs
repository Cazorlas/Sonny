using Serilog ;
using Sonny.Application.Interfaces ;

namespace Sonny.Application.Bases ;

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
    protected BaseViewModel(ICommonServices commonServices)
    {
        RevitDocument = commonServices.RevitDocument ;
        MessageService = commonServices.MessageService ;
        Logger = commonServices.Logger ;
        UnitConverter = commonServices.UnitConverter ;

        // Initialize display unit from document settings
        DisplayUnit = UnitConverter.GetDefaultDisplayUnit(RevitDocument.Document) ;
    }

    #endregion

    #region Common Services (Dependency Injection)

    /// <summary>
    ///     Revit document service for accessing Revit API
    /// </summary>
    protected IRevitDocument RevitDocument { get ; }

    /// <summary>
    ///     Message service for showing messages to user
    /// </summary>
    protected IMessageService MessageService { get ; }

    /// <summary>
    ///     Logger for logging operations
    /// </summary>
    protected ILogger Logger { get ; }

    /// <summary>
    ///     Unit converter for converting between display units and internal units
    /// </summary>
    protected IUnitConverter UnitConverter { get ; }

    #endregion

    #region Common Properties

    /// <summary>
    ///     Action to close the window (set by View)
    /// </summary>
    public Action? CloseWindowAction { get ; set ; }

    /// <summary>
    ///     Display unit type (default: millimeters for metric, feet for imperial)
    /// </summary>
    protected ForgeTypeId DisplayUnit { get ; }

    /// <summary>
    ///     Display unit name for UI (e.g., "mm", "cm", "ft")
    /// </summary>
    public string DisplayUnitName => UnitConverter.GetUnitDisplayName(DisplayUnit) ;

    #endregion

    #region Common Helper Methods

    /// <summary>
    ///     Close the window
    /// </summary>
    protected void CloseWindow() => CloseWindowAction?.Invoke() ;

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
        if (ex != null)
        {
            Logger.Error(ex,
                message) ;
        }
        else
        {
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

    #endregion
}
