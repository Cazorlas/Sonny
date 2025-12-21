namespace Sonny.Application.Core.FailuresPreprocessors ;

/// <summary>
///     Failure preprocessor to delete warnings
/// </summary>
public class SuppressWarningsPreprocessor : IFailuresPreprocessor
{
    /// <summary>
    ///     Preprocesses failures and deletes warnings
    /// </summary>
    /// <param name="failuresAccessor">The failures accessor</param>
    /// <returns>Continue processing result</returns>
    public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
    {
        var failures = failuresAccessor.GetFailureMessages() ;
        foreach (var failure in failures)
        {
            var severity = failure.GetSeverity() ;
            if (severity == FailureSeverity.Warning)
            {
                failuresAccessor.DeleteWarning(failure) ;
            }
        }

        return FailureProcessingResult.Continue ;
    }
}
