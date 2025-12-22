namespace Sonny.Application.Core.Managers ;

public class TransactionGroupManager(Document document, string name) : IDisposable
{
    private bool _disposed ;

    public TransactionGroup TransactionGroup { get ; } = new(document,
        name) ;

    public void Dispose()
    {
        if (_disposed) {
            return ;
        }

        if (TransactionGroup.HasStarted()) {
            TransactionGroup.RollBack() ;
        }

        TransactionGroup.Dispose() ;
        _disposed = true ;
    }

    public void Start() => TransactionGroup.Start() ;

    public TransactionStatus GetStatus() =>
        TransactionGroup.IsValidObject ? TransactionGroup.GetStatus() : TransactionStatus.RolledBack ;

    public bool IsRolledBack() => GetStatus() == TransactionStatus.RolledBack ;

    public void RollBack() => TransactionGroup.RollBack() ;

    public void Assimilate() => TransactionGroup.Assimilate() ;
}
