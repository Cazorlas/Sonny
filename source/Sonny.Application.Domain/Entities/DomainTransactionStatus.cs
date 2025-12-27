namespace Sonny.Application.Domain.Entities ;

public enum DomainTransactionStatus
{
    Uninitialized,
    Started,
    Committed,
    RolledBack
}
