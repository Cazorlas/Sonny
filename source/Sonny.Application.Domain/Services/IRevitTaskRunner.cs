namespace Sonny.Application.Domain.Services ;

/// <summary>
///     Interface for running Revit API code asynchronously
/// </summary>
public interface IRevitTaskRunner
{
    /// <summary>
    ///     Runs Revit API code asynchronously and returns a result
    /// </summary>
    /// <typeparam name="TResult">The type of the result</typeparam>
    /// <param name="function">The function to execute in Revit API context</param>
    /// <returns>The result of the function execution</returns>
    Task<TResult> RunAsync<TResult>(Func<TResult> function) ;

    /// <summary>
    ///     Runs Revit API code asynchronously
    /// </summary>
    /// <param name="action">The action to execute in Revit API context</param>
    /// <returns>The task indicating whether the execution has completed</returns>
    Task RunAsync(Action action) ;
}
