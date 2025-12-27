using Sonny.Application.Domain.Services ;
using Sonny.Application.Infrastructure.Revit.Services ;

namespace Sonny.Application.Infrastructure.Revit.Implements ;

public class ElementSelector(IRevitDocument revitDocument) : IElementSelector
{
    public void SelectElements(ICollection<string> uniqueIds)
    {
        if (uniqueIds.Count == 0) {
            return ;
        }

        var elementIds = uniqueIds.Select(uniqueId => revitDocument.Document.GetElement(uniqueId))
            .Where(element => element != null)
            .Select(element => element!.Id)
            .ToList() ;

        if (elementIds.Count > 0) {
            revitDocument.UIDocument.Selection.SetElementIds(elementIds) ;
        }
    }
}
