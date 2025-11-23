using Autodesk.Revit.UI ;
using Revit.Async ;
using SonnyApplication ;

namespace SonnyApplication.Bases ;

/// <summary>
/// Base class for all external commands with automatic RevitTask initialization
/// </summary>
public abstract class BaseExternalCommand : IExternalCommand
{
    /// <summary>
    /// Executes the command with automatic RevitTask initialization
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
    /// Executes the command logic (to be implemented by derived classes)
    /// </summary>
    /// <param name="commandData">The external command data</param>
    /// <param name="message">The message</param>
    /// <param name="elements">The element set</param>
    /// <returns>The result of the command execution</returns>
    protected abstract Result ExecuteInternal(ExternalCommandData commandData,
        ref string message,
        ElementSet elements) ;

    /// <summary>
    /// Gets a service from the DI container
    /// </summary>
    /// <typeparam name="T">The type of service to get</typeparam>
    /// <returns>The service instance</returns>
    protected T GetService<T>() where T : class
    {
        return Host.GetService<T>() ;
    }
}
