namespace Sonny.Application.Infrastructure.Revit.Services ;

/// <summary>
///     Interface for composite failure preprocessor that can chain multiple preprocessors
/// </summary>
public interface ICompositeFailurePreprocessor : IFailuresPreprocessor
{
    /// <summary>
    ///     Adds a preprocessor to the chain
    /// </summary>
    /// <param name="preprocessor">Preprocessor to add</param>
    void AddPreprocessor(IFailuresPreprocessor preprocessor) ;
}
