using Sonny.Application.Domain.Entities.Settings.Models ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.Infrastructure.Revit.Services ;
using Sonny.Application.UseCases.ColumnFromCad.Services ;
using Sonny.RevitExtensions.Extensions ;
using Sonny.RevitExtensions.Extensions.Families ;

namespace Sonny.Application.Infrastructure.Features.ColumnFromCad.Implements ;

public class ColumnFromCadContext : IColumnFromCadContext
{
    public string SelectedCadLinkId { get ; }
    public HashSet<string> LayerNames { get ; }
    public List<FamilyModel> ColumnFamilies { get ; }
    public List<LevelModel> Levels { get ; }
    public Dictionary<string, HashSet<string>> FamilyNumericParameters { get ; }

    public ColumnFromCadContext(ICadLinkSelector cadLinkSelector,
        IRevitDocument revitDocument,
        IResourceHelper resourceHelper)
    {
        var uiDocument = revitDocument.UIDocument ;

        if (cadLinkSelector.SelectCadLink(uiDocument) is not { } selectedCadLink) {
            throw new InvalidOperationException(resourceHelper.GetString("MessageFailedToSelectCadLink")) ;
        }

        var layerNames = selectedCadLink.GetAllLayerNames(true) ;
        if (layerNames.Count == 0) {
            throw new InvalidOperationException(resourceHelper.GetString("MessageNoLayersFoundInCadLink")) ;
        }

        var document = uiDocument.Document ;
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

        if (families.Count == 0) {
            throw new InvalidOperationException(resourceHelper.GetString("MessageNoColumnFamiliesFound")) ;
        }

        // Convert families to Domain models
        var familyModels = families.Select(f => new FamilyModel(f.UniqueId,
                f.Name))
            .ToList() ;

        // Load parameters for all families (use string key instead of ElementId)
        var familyParameters = new Dictionary<string, HashSet<string>>() ;
        foreach (var family in families) {
            if (family.GetFamilySymbols()
                    .FirstOrDefault() is not { } familySymbol) {
                continue ;
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
            if (allParameters.Count == 0) {
                continue ;
            }

            familyParameters[family.UniqueId] = allParameters ;
        }

        // Load levels and convert to Domain models
        var levels = document.GetAllElements<Level>()
            .OrderBy(level => level.Elevation)
            .Select(level => new LevelModel(level.UniqueId,
                level.Name))
            .ToList() ;

        SelectedCadLinkId = selectedCadLink.UniqueId ;
        LayerNames = layerNames ;
        ColumnFamilies = familyModels ;
        Levels = levels ;
        FamilyNumericParameters = familyParameters ;
    }
}
