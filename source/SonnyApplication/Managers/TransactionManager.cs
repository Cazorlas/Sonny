using SonnyApplication.Exceptions ;
using SonnyApplication.Preprocessors ;

namespace SonnyApplication.Managers
{
    public class TransactionManager(
        Document document,
        string name,
        CompositeFailurePreprocessor? failuresPreprocessor = null) : IDisposable
    {
        private readonly Document _document = document ;
        private bool _disposed ;
        public Transaction Transaction { get ; } = new(document,
            name) ;

        public void Start()
        {
            Transaction.Start() ;
        }

        public TransactionStatus GetStatus()
        {
            return Transaction.IsValidObject ? Transaction.GetStatus() : TransactionStatus.RolledBack ;
        }

        public void RollBack()
        {
            Transaction.RollBack() ;
        }

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
    }
}
