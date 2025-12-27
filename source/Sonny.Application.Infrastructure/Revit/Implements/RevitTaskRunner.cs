using Revit.Async ;
using Sonny.Application.Domain.Services ;

namespace Sonny.Application.Infrastructure.Revit.Implements ;

/// <summary>
///     Implementation of IRevitTaskRunner using Revit.Async
/// </summary>
public class RevitTaskRunner : IRevitTaskRunner
{
    public Task<TResult> RunAsync<TResult>(Func<TResult> function) => RevitTask.RunAsync(function) ;

    public Task RunAsync(Action action) => RevitTask.RunAsync(action) ;
}
