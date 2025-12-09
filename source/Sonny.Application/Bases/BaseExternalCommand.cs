using Autodesk.Revit.UI ;
using Revit.Async ;
using Serilog ;
using Sonny.Application.Interfaces ;
using Sonny.Application.Services ;

namespace Sonny.Application.Bases ;

/// <summary>
///     Base class for all external commands with automatic RevitTask initialization
/// </summary>
public abstract class BaseExternalCommand : IExternalCommand
{
    /// <summary>
    ///     Executes the command with automatic RevitTask initialization
    /// </summary>
    /// <param name="commandData">The external command data</param>
    /// <param name="message">The message</param>
    /// <param name="elements">The element set</param>
    /// <returns>The result of the command execution</returns>
    public Result Execute(ExternalCommandData commandData,
        ref string message,
        ElementSet elements)
    {
        // Initialize RevitTask in Command context (valid Revit API context)
        RevitTask.Initialize(commandData.Application) ;

        // Call the derived class implementation
        return ExecuteInternal(commandData,
            ref message,
            elements) ;
    }

    /// <summary>
    ///     Executes the command logic (to be implemented by derived classes)
    /// </summary>
    /// <param name="commandData">The external command data</param>
    /// <param name="message">The message</param>
    /// <param name="elements">The element set</param>
    /// <returns>The result of the command execution</returns>
    protected abstract Result ExecuteInternal(ExternalCommandData commandData,
        ref string message,
        ElementSet elements) ;

    /// <summary>
    ///     Gets a service from the DI container
    /// </summary>
    /// <typeparam name="T">The type of service to get</typeparam>
    /// <returns>The service instance</returns>
    protected T GetService<T>() where T : class => Host.GetService<T>() ;

    /// <summary>
    ///     Creates a CommonServices instance for ViewModels
    /// </summary>
    /// <param name="commandData">The external command data</param>
    /// <returns>CommonServices instance</returns>
    protected ICommonServices CreateCommonServices(ExternalCommandData commandData)
    {
        var revitDocumentService = new RevitDocumentService(commandData.Application.ActiveUIDocument) ;
        var messageService = GetService<IMessageService>() ;
        var logger = GetService<ILogger>() ;
        var unitConverter = GetService<IUnitConverter>() ;

        return new CommonServices(revitDocumentService,
            messageService,
            logger,
            unitConverter) ;
    }
}
