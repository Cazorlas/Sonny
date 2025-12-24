using Autodesk.Revit.DB ;
using Sonny.Application.Domain.Interfaces ;

namespace Sonny.Application.Infrastructure.Managers ;

public class TransactionManagerFactory : ITransactionManagerFactory
{
    public ITransactionManager Create(IRevitDocument revitDocument,
        string name,
        IFailuresPreprocessor? failuresPreprocessor = null)
    {
        return new TransactionManager(revitDocument.Document,
            name,
            failuresPreprocessor) ;
    }

    public ITransactionGroupManager CreateGroup(IRevitDocument revitDocument,
        string name)
    {
        return new TransactionGroupManager(revitDocument.Document,
            name) ;
    }
}

