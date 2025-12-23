using Sonny.Application.Domain.InputPorts.ColumnFromCad ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.RevitExtensions.Extensions ;

namespace Sonny.Application.Infrastructure.Services ;

public class ColumnFamilyLoader : IColumnFamilyLoader
{
    private readonly IFamilySymbolProvider _familySymbolProvider ;

    public ColumnFamilyLoader(IFamilySymbolProvider familySymbolProvider)
    {
        _familySymbolProvider = familySymbolProvider ;
    }

    public List<Family> GetAllColumnFamilies(Document document)
    {
        var structuralColumns = Category.GetCategory(document,
            BuiltInCategory.OST_StructuralColumns) ;
        var columns = Category.GetCategory(document,
            BuiltInCategory.OST_Columns) ;

        var families = document.GetAllElements<Family>()
            .Where(f => f.FamilyCategory.Id.Equals(structuralColumns.Id) || f.FamilyCategory.Id.Equals(columns.Id))
            .Where(f => f.GetFamilySymbolIds()
                .Any())
            .OrderBy(f => f.Name)
            .ToList() ;

        return families ;
    }

    public HashSet<string> GetNumericParameters(Family family)
    {
        if (_familySymbolProvider.GetFamilySymbols(family)
                .FirstOrDefault() is not { } familySymbol) {
            return [] ;
        }

        var allParameters = familySymbol.Parameters
            .Cast<Parameter>()
            .Where(p => p.StorageType is StorageType.Double or StorageType.Integer)
            .Where(p => ! p.Definition.Name.Contains("Assembly"))
            .Where(p => ! p.Definition.Name.Contains("OmniClass"))
            .Where(p => ! p.Definition.Name.Contains("Material"))
            .Where(p => ! p.Definition.Name.Contains("Category"))
            .Where(p => ! p.Definition.Name.Contains("Type"))
            .Select(p => p.Definition.Name)
            .ToHashSet() ;

        return allParameters ;
    }
}

