namespace SonnyApplication.Managers
{
    public class TransactionGroupManager(Document document, string name) : IDisposable
    {
        private readonly Document _document = document ;
        private bool _disposed ;

        public TransactionGroup TransactionGroup { get ; } = new(document,
            name) ;

        public void Start()
        {
            TransactionGroup.Start() ;
        }

        public TransactionStatus GetStatus()
        {
            return TransactionGroup.IsValidObject ? TransactionGroup.GetStatus() : TransactionStatus.RolledBack ;
        }

        public bool IsRolledBack()
        {
            return GetStatus() == TransactionStatus.RolledBack ;
        }

        public void RollBack()
        {
            TransactionGroup.RollBack() ;
        }

        public void Assimilate()
        {
            TransactionGroup.Assimilate() ;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return ;
            }

            if (TransactionGroup.HasStarted())
            {
                TransactionGroup.RollBack() ;
            }

            TransactionGroup.Dispose() ;
            _disposed = true ;
        }
    }
}
