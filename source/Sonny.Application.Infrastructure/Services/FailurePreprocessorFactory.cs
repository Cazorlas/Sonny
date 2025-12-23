using Autodesk.Revit.DB ;
using Sonny.Application.Domain.Interfaces ;

namespace Sonny.Application.Infrastructure.Services ;

public class FailurePreprocessorFactory : IFailurePreprocessorFactory
{
    public IFailuresPreprocessor CreateSuppressWarningsPreprocessor()
    {
        return new SuppressWarningsPreprocessor() ;
    }

    public ICompositeFailurePreprocessor CreateCompositeFailurePreprocessor()
    {
        return new CompositeFailurePreprocessor() ;
    }
}

