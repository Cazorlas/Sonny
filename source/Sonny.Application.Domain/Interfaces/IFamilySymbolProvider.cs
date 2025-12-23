namespace Sonny.Application.Domain.Interfaces ;

/// <summary>
///     Interface for providing family symbols from a family
/// </summary>
public interface IFamilySymbolProvider
{
    /// <summary>
    ///     Gets all family symbols from the specified family
    /// </summary>
    /// <param name="family">Family to get symbols from</param>
    /// <returns>Enumerable collection of family symbols</returns>
    IEnumerable<FamilySymbol> GetFamilySymbols(Family family) ;
}

