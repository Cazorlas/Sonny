using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.UseCases.ColumnFromCad.Services ;

namespace Sonny.Application.Infrastructure.Services ;

public class ColumnFromCadContext : IColumnFromCadContext
{
    public ImportInstance SelectedCadLink { get ; }
    public HashSet<string> LayerNames { get ; }
    public List<Family> ColumnFamilies { get ; }
    public Dictionary<ElementId, HashSet<string>> FamilyNumericParameters { get ; }

    public ColumnFromCadContext(ICadLinkSelector cadLinkSelector,
        IColumnFamilyLoader columnFamilyLoader,
        IRevitDocument revitDocument,
        IResourceHelper resourceHelper,
        ICadLayerProvider cadLayerProvider)
    {
        var uiDocument = revitDocument.UIDocument ;

        if (cadLinkSelector.SelectCadLink(uiDocument) is not { } selectedCadLink) {
            throw new InvalidOperationException(resourceHelper.GetString("MessageFailedToSelectCadLink")) ;
        }

        var layerNames = cadLayerProvider.GetAllLayerNames(selectedCadLink,
            true) ;
        if (layerNames.Count == 0) {
            throw new InvalidOperationException(resourceHelper.GetString("MessageNoLayersFoundInCadLink")) ;
        }

        var document = uiDocument.Document ;
        var families = columnFamilyLoader.GetAllColumnFamilies(document) ;

        if (families.Count == 0) {
            throw new InvalidOperationException(resourceHelper.GetString("MessageNoColumnFamiliesFound")) ;
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

