namespace Sonny.Application.Domain.Services ;

/// <summary>
///     Factory for creating transaction managers
/// </summary>
public interface ITransactionManagerFactory
{
    /// <summary>
    ///     Creates a transaction manager
    /// </summary>
    /// <param name="name">Transaction name</param>
    /// <param name="failurePreprocessorTypes">List of failure preprocessor types to use (will be combined into composite)</param>
    /// <returns>Transaction manager instance</returns>
    ITransactionManager Create(string name,
        IEnumerable<FailurePreprocessorType>? failurePreprocessorTypes = null) ;

    /// <summary>
    ///     Creates a transaction group manager
    /// </summary>
    /// <param name="name">Transaction group name</param>
    /// <returns>Transaction group manager instance</returns>
    ITransactionGroupManager CreateGroup(string name) ;
}
