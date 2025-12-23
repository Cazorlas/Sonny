namespace Sonny.Application.Entities ;

public enum DomainTransactionStatus
{
    Uninitialized,
    Started,
    Committed,
    RolledBack
}
