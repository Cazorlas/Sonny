using Sonny.Application.Domain.Interfaces ;

namespace Sonny.Application.UseCases.AutoColumnDimension.Models ;

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
    /// <param name="resourceHelper">Resource helper for localization</param>
    /// <returns>Formatted log message</returns>
    public string GetLogMessage(IResourceHelper resourceHelper)
    {
        var total = SuccessCount + FailureCount ;
        var completedTime = ExecutionTime.ToString("yyyy-MM-dd HH:mm:ss") ;
        return resourceHelper.GetString("ExecutionResultCompleted",
                   completedTime)
               + "\n\n"
               + resourceHelper.GetString("ExecutionResultTotalColumns",
                   total)
               + "\n"
               + resourceHelper.GetString("ExecutionResultSuccess",
                   SuccessCount)
               + "\n"
               + resourceHelper.GetString("ExecutionResultFailed",
                   FailureCount) ;
    }
}
