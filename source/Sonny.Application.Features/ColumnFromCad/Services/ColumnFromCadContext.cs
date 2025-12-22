using Sonny.Application.Core.Interfaces ;
using Sonny.Application.Features.ColumnFromCad.Interfaces ;
using Sonny.RevitExtensions.Extensions ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Features.ColumnFromCad.Services ;

public class ColumnFromCadContext : IColumnFromCadContext
{
    public ImportInstance SelectedCadLink { get ; }
    public HashSet<string> LayerNames { get ; }
    public List<Family> ColumnFamilies { get ; }
    public Dictionary<ElementId, HashSet<string>> FamilyNumericParameters { get ; }

    public ColumnFromCadContext(ICadLinkSelector cadLinkSelector,
        IColumnFamilyLoader columnFamilyLoader,
        IRevitDocument revitDocument)
    {
        var uiDocument = revitDocument.UIDocument ;

        if (cadLinkSelector.SelectCadLink(uiDocument) is not { } selectedCadLink) {
            throw new InvalidOperationException(ResourceHelper.GetString("MessageFailedToSelectCadLink")) ;
        }

        if (selectedCadLink.GetAllLayerNames(true) is not { Count: > 0 } layerNames) {
            throw new InvalidOperationException(ResourceHelper.GetString("MessageNoLayersFoundInCadLink")) ;
        }

        var document = uiDocument.Document ;
        var families = columnFamilyLoader.GetAllColumnFamilies(document) ;

        if (families.Count == 0) {
            throw new InvalidOperationException(ResourceHelper.GetString("MessageNoColumnFamiliesFound")) ;
        }

        // Load parameters for all families
        var familyParameters = new Dictionary<ElementId, HashSet<string>>() ;
        foreach (var family in families) {
            var parameters = columnFamilyLoader.GetNumericParameters(family) ;
            if (parameters.Count == 0) {
                continue ;
            }

            familyParameters[family.Id] = parameters ;
        }

        SelectedCadLink = selectedCadLink ;
        LayerNames = layerNames ;
        ColumnFamilies = families ;
        FamilyNumericParameters = familyParameters ;
    }
}
