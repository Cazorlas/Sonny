using Sonny.Application.Domain.Entites ;

namespace Sonny.Application.Infrastructure.Managers ;

/// <summary>
///     Helper class for converting Revit API TransactionStatus to Domain TransactionStatus
/// </summary>
internal static class TransactionStatusConverter
{
    /// <summary>
    ///     Converts Revit API TransactionStatus to Domain TransactionStatus
    /// </summary>
    /// <param name="revitStatus">Revit API transaction status</param>
    /// <returns>Domain transaction status</returns>
    public static DomainTransactionStatus Convert(Autodesk.Revit.DB.TransactionStatus revitStatus)
    {
        return revitStatus switch
        {
            TransactionStatus.Uninitialized => DomainTransactionStatus.Uninitialized,
            TransactionStatus.Started => DomainTransactionStatus.Started,
            TransactionStatus.Committed => DomainTransactionStatus.Committed,
            _ => DomainTransactionStatus.RolledBack
        } ;
    }
}

