using Sonny.Application.Domain.Services ;

namespace Sonny.Application.Infrastructure.Revit.Services ;

public interface IFailurePreprocessorFactory
{
    IFailuresPreprocessor? CreateComposite(IEnumerable<FailurePreprocessorType> types) ;
}
