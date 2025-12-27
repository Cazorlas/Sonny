using Sonny.Application.Domain.Services ;
using Sonny.Application.Infrastructure.Revit.Services ;

namespace Sonny.Application.Infrastructure.Revit.Managers.Transactions ;

public class TransactionManagerFactory(
    IUIDocumentProvider uiDocumentProvider,
    IFailurePreprocessorFactory failurePreprocessorFactory) : ITransactionManagerFactory
{
    public ITransactionManager Create(string name,
        IEnumerable<FailurePreprocessorType>? failurePreprocessorTypes = null)
    {
        IFailuresPreprocessor? failuresPreprocessor = null ;
        if (failurePreprocessorTypes != null) {
            failuresPreprocessor = failurePreprocessorFactory.CreateComposite(failurePreprocessorTypes) ;
        }

        return new TransactionManager(uiDocumentProvider,
            name,
            failuresPreprocessor) ;
    }

    public ITransactionGroupManager CreateGroup(string name) =>
        new TransactionGroupManager(uiDocumentProvider,
            name) ;
}
