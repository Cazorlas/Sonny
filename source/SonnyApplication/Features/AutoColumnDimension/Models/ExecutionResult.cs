namespace SonnyApplication.Features.AutoColumnDimension.Models ;

/// <summary>
/// Result of execution for AutoColumnDimension feature
/// </summary>
public class ExecutionResult
{
    /// <summary>
    /// Number of successfully created dimensions
    /// </summary>
    public int SuccessCount { get ; set ; }

    /// <summary>
    /// Number of failed dimension creations
    /// </summary>
    public int FailureCount { get ; set ; }

    /// <summary>
    /// Time when execution started
    /// </summary>
    public DateTime ExecutionTime { get ; set ; } = DateTime.Now ;

    /// <summary>
    /// Gets formatted log message for display
    /// </summary>
    /// <returns>Formatted log message</returns>
    public string GetLogMessage()
    {
        var total = SuccessCount + FailureCount ;
        return $"Execution completed at {ExecutionTime:yyyy-MM-dd HH:mm:ss}\n\n" +
               $"Total columns processed: {total}\n" +
               $"Successfully created dimensions: {SuccessCount}\n" +
               $"Failed: {FailureCount}" ;
    }
}

