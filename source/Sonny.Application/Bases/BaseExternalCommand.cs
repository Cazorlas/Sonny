using Autodesk.Revit.UI ;
using Revit.Async ;
using Sonny.Application.Core.Interfaces ;

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
        // Initialize Host if not already initialized
        Host.Start() ;

        // Set UIDocument in provider for DI container
        var uiDocumentProvider = Host.GetService<IUIDocumentProvider>() ;
        uiDocumentProvider.SetUIDocument(commandData.Application.ActiveUIDocument) ;

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
}
