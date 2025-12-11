using Sonny.ResourceManager ;

namespace Sonny.Application.Features.AutoColumnDimension.Models ;

/// <summary>
///     Result of execution for AutoColumnDimension feature
/// </summary>
public class ExecutionResult
{
    /// <summary>
    ///     Number of successfully created dimensions
    /// </summary>
    public int SuccessCount { get ; set ; }

    /// <summary>
    ///     Number of failed dimension creations
    /// </summary>
    public int FailureCount { get ; set ; }

    /// <summary>
    ///     Time when execution started
    /// </summary>
    public DateTime ExecutionTime { get ; set ; } = DateTime.Now ;

    /// <summary>
    ///     Gets formatted log message for display
    /// </summary>
    /// <returns>Formatted log message</returns>
    public string GetLogMessage()
    {
        var total = SuccessCount + FailureCount ;
        var completedTime = ExecutionTime.ToString("yyyy-MM-dd HH:mm:ss") ;
        return ResourceHelper.GetString("ExecutionResultCompleted", completedTime) + "\n\n"
               + ResourceHelper.GetString("ExecutionResultTotalColumns", total) + "\n"
               + ResourceHelper.GetString("ExecutionResultSuccess", SuccessCount) + "\n"
               + ResourceHelper.GetString("ExecutionResultFailed", FailureCount) ;
    }
}
