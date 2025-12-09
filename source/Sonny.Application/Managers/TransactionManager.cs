using Sonny.Application.Exceptions ;
using Sonny.Application.Preprocessors ;

namespace Sonny.Application.Managers ;

public class TransactionManager(
    Document document,
    string name,
    CompositeFailurePreprocessor? failuresPreprocessor = null) : IDisposable
{
    private bool _disposed ;

    public Transaction Transaction { get ; } = new(document,
        name) ;

    public void Dispose()
    {
        if (_disposed)
        {
            return ;
        }

        if (Transaction.HasStarted())
        {
            Transaction.RollBack() ;
        }

        Transaction.Dispose() ;
        _disposed = true ;
    }

    public void Start() => Transaction.Start() ;

    public TransactionStatus GetStatus() =>
        Transaction.IsValidObject ? Transaction.GetStatus() : TransactionStatus.RolledBack ;

    public void RollBack() => Transaction.RollBack() ;

    public bool Commit()
    {
        if (failuresPreprocessor != null)
        {
            var failureOptions = Transaction.GetFailureHandlingOptions() ;
            failureOptions.SetFailuresPreprocessor(failuresPreprocessor) ;
            Transaction.Commit(failureOptions) ;
        }
        else
        {
            Transaction.Commit() ;
        }

        if (GetStatus() == TransactionStatus.RolledBack)
        {
            throw new TransactionCommitFailedException() ;
        }

        return true ;
    }
}
