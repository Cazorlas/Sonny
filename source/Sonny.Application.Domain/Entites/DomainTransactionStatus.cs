namespace Sonny.Application.Domain.Entites ;

public enum DomainTransactionStatus
{
    Uninitialized,
    Started,
    Committed,
    RolledBack
}
