namespace Sonny.Application.Preprocessors ;

public class CompositeFailurePreprocessor : IFailuresPreprocessor
{
    private readonly List<IFailuresPreprocessor> _preprocessors = [] ;

    /// <summary>
    ///     Executes all registered preprocessors in order until one returns non-Continue result
    /// </summary>
    /// <param name="failuresAccessor">Failure accessor from transaction</param>
    /// <returns>
    ///     • Continue - All preprocessors returned Continue
    ///     • ProceedWithCommit - First preprocessor that returned ProceedWithCommit
    ///     • WaitForUserInput - First preprocessor that returned WaitForUserInput
    /// </returns>
    public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
    {
        foreach (var preprocessor in _preprocessors)
        {
            var result = preprocessor.PreprocessFailures(failuresAccessor) ;

            // Stop chain if preprocessor wants to commit or wait for user
            if (result == FailureProcessingResult.Continue)
            {
                continue ;
            }

            return result ;
        }

        // All preprocessors returned Continue
        return FailureProcessingResult.Continue ;
    }

    public void AddPreprocessor(IFailuresPreprocessor preprocessor) => _preprocessors.Add(preprocessor) ;
}
