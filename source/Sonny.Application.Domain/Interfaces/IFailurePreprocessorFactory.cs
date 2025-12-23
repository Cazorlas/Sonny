using Autodesk.Revit.DB ;

namespace Sonny.Application.Domain.Interfaces ;

/// <summary>
///     Factory for creating failure preprocessors
/// </summary>
public interface IFailurePreprocessorFactory
{
    /// <summary>
    ///     Creates a preprocessor that suppresses warnings
    /// </summary>
    /// <returns>Failure preprocessor instance</returns>
    IFailuresPreprocessor CreateSuppressWarningsPreprocessor() ;

    /// <summary>
    ///     Creates a composite failure preprocessor that can chain multiple preprocessors
    /// </summary>
    /// <returns>Composite failure preprocessor instance</returns>
    ICompositeFailurePreprocessor CreateCompositeFailurePreprocessor() ;
}

