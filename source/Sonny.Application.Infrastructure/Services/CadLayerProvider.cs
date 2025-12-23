using Sonny.Application.Domain.Interfaces ;
using Sonny.RevitExtensions.Extensions ;

namespace Sonny.Application.Infrastructure.Services ;

public class CadLayerProvider : ICadLayerProvider
{
    public HashSet<string> GetAllLayerNames(ImportInstance cadLink,
        bool includeHidden = false)
    {
        return cadLink.GetAllLayerNames(includeHidden) ;
    }
}

