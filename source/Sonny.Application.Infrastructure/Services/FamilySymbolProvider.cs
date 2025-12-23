using Sonny.Application.Domain.Interfaces ;
using Sonny.RevitExtensions.Extensions.Families ;

namespace Sonny.Application.Infrastructure.Services ;

public class FamilySymbolProvider : IFamilySymbolProvider
{
    public IEnumerable<FamilySymbol> GetFamilySymbols(Family family)
    {
        return family.GetFamilySymbols() ;
    }
}

