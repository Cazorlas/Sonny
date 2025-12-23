using Autodesk.Revit.DB ;

namespace Sonny.Application.Domain.Interfaces ;

/// <summary>
///     Factory for creating transaction managers
/// </summary>
public interface ITransactionManagerFactory
{
    /// <summary>
    ///     Creates a transaction manager
    /// </summary>
    /// <param name="revitDocument">Revit document service</param>
    /// <param name="name">Transaction name</param>
    /// <param name="failuresPreprocessor">Optional failure preprocessor</param>
    /// <returns>Transaction manager instance</returns>
    ITransactionManager Create(IRevitDocument revitDocument,
        string name,
        IFailuresPreprocessor? failuresPreprocessor = null) ;

    /// <summary>
    ///     Creates a transaction group manager
    /// </summary>
    /// <param name="revitDocument">Revit document service</param>
    /// <param name="name">Transaction group name</param>
    /// <returns>Transaction group manager instance</returns>
    ITransactionGroupManager CreateGroup(IRevitDocument revitDocument,
        string name) ;
}

