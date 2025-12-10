using Serilog ;

namespace Sonny.Application.Core.Interfaces ;

/// <summary>
///     Common services container for ViewModels
///     Contains services that are used in almost all ViewModels
/// </summary>
public interface ICommonServices
{
    /// <summary>
    ///     Revit document service for accessing Revit API
    /// </summary>
    IRevitDocument RevitDocument { get ; }

    /// <summary>
    ///     Message service for showing messages to user
    /// </summary>
    IMessageService MessageService { get ; }

    /// <summary>
    ///     Logger for logging operations
    /// </summary>
    ILogger Logger { get ; }

    /// <summary>
    ///     Unit converter for converting between display units and internal units
    /// </summary>
    IUnitConverter UnitConverter { get ; }

    /// <summary>
    ///     Settings service for managing application preferences
    /// </summary>
    ISettingsService SettingsService { get ; }
}
