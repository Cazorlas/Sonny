namespace Sonny.Application.Domain.Services ;

public interface ITransactionGroupManager : IDisposable
{
    void Start() ;
    void Assimilate() ;
}
