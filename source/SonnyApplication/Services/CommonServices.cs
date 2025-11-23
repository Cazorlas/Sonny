using Serilog ;
using SonnyApplication.Interfaces ;

namespace SonnyApplication.Services ;

/// <summary>
/// Implementation of common services container
/// </summary>
public class CommonServices : ICommonServices
{
    /// <summary>
    /// Initializes a new instance of CommonServices
    /// </summary>
    /// <param name="revitDocument">Revit document service</param>
    /// <param name="messageService">Message service</param>
    /// <param name="logger">Logger</param>
    /// <param name="unitConverter">Unit converter</param>
    public CommonServices(IRevitDocument revitDocument,
        IMessageService messageService,
        ILogger logger,
        IUnitConverter unitConverter)
    {
        RevitDocument = revitDocument ;
        MessageService = messageService ;
        Logger = logger ;
        UnitConverter = unitConverter ;
    }

    /// <summary>
    /// Revit document service for accessing Revit API
    /// </summary>
    public IRevitDocument RevitDocument { get ; }

    /// <summary>
    /// Message service for showing messages to user
    /// </summary>
    public IMessageService MessageService { get ; }

    /// <summary>
    /// Logger for logging operations
    /// </summary>
    public ILogger Logger { get ; }

    /// <summary>
    /// Unit converter for converting between display units and internal units
    /// </summary>
    public IUnitConverter UnitConverter { get ; }
}

