using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.Entities ;

namespace Sonny.Application.Infrastructure.Managers ;

/// <summary>
///     Infrastructure implementation of transaction group manager
/// </summary>
public class TransactionGroupManager : ITransactionGroupManager
{
    private readonly Autodesk.Revit.DB.TransactionGroup _transactionGroup ;
    private bool _disposed ;

    /// <summary>
    ///     Initializes a new instance of TransactionGroupManager
    /// </summary>
    /// <param name="document">Revit document</param>
    /// <param name="name">Transaction group name</param>
    public TransactionGroupManager(Autodesk.Revit.DB.Document document,
        string name)
    {
        _transactionGroup = new Autodesk.Revit.DB.TransactionGroup(document,
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
        if (!_transactionGroup.IsValidObject) {
            return DomainTransactionStatus.RolledBack ;
        }

        var revitStatus = _transactionGroup.GetStatus() ;
        return TransactionStatusConverter.Convert(revitStatus) ;
    }

    public bool IsRolledBack() => GetStatus() == DomainTransactionStatus.RolledBack ;

    public void RollBack() => _transactionGroup.RollBack() ;

    public void Assimilate() => _transactionGroup.Assimilate() ;
}

