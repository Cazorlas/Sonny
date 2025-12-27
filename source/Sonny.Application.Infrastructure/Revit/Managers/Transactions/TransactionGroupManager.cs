using Sonny.Application.Domain.Entities ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.Infrastructure.Revit.Services ;

namespace Sonny.Application.Infrastructure.Revit.Managers.Transactions ;

/// <summary>
///     Infrastructure implementation of transaction group manager
/// </summary>
public class TransactionGroupManager : ITransactionGroupManager
{
    private readonly TransactionGroup _transactionGroup ;
    private bool _disposed ;

    /// <summary>
    ///     Initializes a new instance of TransactionGroupManager
    /// </summary>
    /// <param name="uiDocumentProvider">UIDocument provider</param>
    /// <param name="name">Transaction group name</param>
    public TransactionGroupManager(IUIDocumentProvider uiDocumentProvider,
        string name)
    {
        var document = uiDocumentProvider.GetUIDocument()
            .Document ;
        _transactionGroup = new TransactionGroup(document,
            name) ;
    }

    public void Dispose()
    {
        if (_disposed) {
            return ;
        }

        if (_transactionGroup.HasStarted()) {
            _transactionGroup.RollBack() ;
        }

        _transactionGroup.Dispose() ;
        _disposed = true ;
    }

    public void Start() => _transactionGroup.Start() ;

    public DomainTransactionStatus GetStatus()
    {
        if (! _transactionGroup.IsValidObject) {
            return DomainTransactionStatus.RolledBack ;
        }

        var revitStatus = _transactionGroup.GetStatus() ;
        return TransactionStatusConverter.Convert(revitStatus) ;
    }

    public bool IsRolledBack() => GetStatus() == DomainTransactionStatus.RolledBack ;

    public void RollBack() => _transactionGroup.RollBack() ;

    public void Assimilate() => _transactionGroup.Assimilate() ;
}
