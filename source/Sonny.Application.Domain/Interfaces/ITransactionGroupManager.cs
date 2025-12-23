using Sonny.Application.Entities ;

namespace Sonny.Application.Domain.Interfaces ;

/// <summary>
///     Interface for managing Revit transaction groups
/// </summary>
public interface ITransactionGroupManager : IDisposable
{
    /// <summary>
    ///     Starts the transaction group
    /// </summary>
    void Start() ;

    /// <summary>
    ///     Gets the current transaction group state
    /// </summary>
    /// <returns>Transaction state</returns>
    DomainTransactionStatus GetStatus() ;

    /// <summary>
    ///     Checks if transaction group is rolled back
    /// </summary>
    /// <returns>True if rolled back</returns>
    bool IsRolledBack() ;

    /// <summary>
    ///     Rolls back the transaction group
    /// </summary>
    void RollBack() ;

    /// <summary>
    ///     Assimilates the transaction group
    /// </summary>
    void Assimilate() ;
}

