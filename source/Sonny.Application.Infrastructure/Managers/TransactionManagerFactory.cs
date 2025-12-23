using Autodesk.Revit.DB ;
using Sonny.Application.Domain.Interfaces ;

namespace Sonny.Application.Infrastructure.Managers ;

/// <summary>
///     Infrastructure implementation of transaction manager factory
/// </summary>
public class TransactionManagerFactory : ITransactionManagerFactory
{
    /// <summary>
    ///     Creates a transaction manager
    /// </summary>
    /// <param name="revitDocument">Revit document service</param>
    /// <param name="name">Transaction name</param>
    /// <param name="failuresPreprocessor">Optional failure preprocessor</param>
    /// <returns>Transaction manager instance</returns>
    public ITransactionManager Create(IRevitDocument revitDocument,
        string name,
        IFailuresPreprocessor? failuresPreprocessor = null)
    {
        return new TransactionManager(revitDocument.Document,
            name,
            failuresPreprocessor) ;
    }

    /// <summary>
    ///     Creates a transaction group manager
    /// </summary>
    /// <param name="revitDocument">Revit document service</param>
    /// <param name="name">Transaction group name</param>
    /// <returns>Transaction group manager instance</returns>
    public ITransactionGroupManager CreateGroup(IRevitDocument revitDocument,
        string name)
    {
        return new TransactionGroupManager(revitDocument.Document,
            name) ;
    }
}

