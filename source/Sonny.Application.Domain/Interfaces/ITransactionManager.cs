using Sonny.Application.Domain.Exceptions ;
using Sonny.Application.Entities ;

namespace Sonny.Application.Domain.Interfaces ;

/// <summary>
///     Interface for managing Revit transactions
/// </summary>
public interface ITransactionManager : IDisposable
{
    /// <summary>
    ///     Starts the transaction
    /// </summary>
    void Start() ;

    /// <summary>
    ///     Gets the current transaction state
    /// </summary>
    /// <returns>Transaction state</returns>
    DomainTransactionStatus GetStatus() ;

    /// <summary>
    ///     Rolls back the transaction
    /// </summary>
    void RollBack() ;

    /// <summary>
    ///     Commits the transaction
    /// </summary>
    /// <returns>True if commit successful</returns>
    /// <exception cref="TransactionCommitFailedException">Thrown when commit fails</exception>
    bool Commit() ;
}

