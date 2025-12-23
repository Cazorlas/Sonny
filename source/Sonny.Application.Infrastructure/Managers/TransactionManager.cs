using Autodesk.Revit.DB ;
using Sonny.Application.Domain.Exceptions ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.Entities ;

namespace Sonny.Application.Infrastructure.Managers ;

public class TransactionManager : ITransactionManager
{
    private readonly Transaction _transaction ;
    private readonly IFailuresPreprocessor? _failuresPreprocessor ;
    private bool _disposed ;

    public TransactionManager(Document document,
        string name,
        IFailuresPreprocessor? failuresPreprocessor = null)
    {
        _transaction = new Transaction(document,
            name) ;
        _failuresPreprocessor = failuresPreprocessor ;
    }

    public void Dispose()
    {
        if (_disposed) {
            return ;
        }

        if (_transaction.HasStarted()) {
            _transaction.RollBack() ;
        }

        _transaction.Dispose() ;
        _disposed = true ;
    }

    public void Start() => _transaction.Start() ;

    public DomainTransactionStatus GetStatus()
    {
        if (!_transaction.IsValidObject) {
            return DomainTransactionStatus.RolledBack ;
        }

        var revitStatus = _transaction.GetStatus() ;
        return TransactionStatusConverter.Convert(revitStatus) ;
    }

    public void RollBack() => _transaction.RollBack() ;

    public bool Commit()
    {
        if (_failuresPreprocessor != null) {
            var failureOptions = _transaction.GetFailureHandlingOptions() ;
            failureOptions.SetFailuresPreprocessor(_failuresPreprocessor) ;
            _transaction.Commit(failureOptions) ;
        }
        else {
            _transaction.Commit() ;
        }

        if (GetStatus() == DomainTransactionStatus.RolledBack) {
            throw new TransactionCommitFailedException() ;
        }

        return true ;
    }
}

