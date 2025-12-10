using Autodesk.Revit.UI ;
using Revit.Async ;

namespace Sonny.Application.Core.Bases ;

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
}
