using Autodesk.Revit.UI ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.Infrastructure.SelectionFilters ;
using Sonny.RevitExtensions.Extensions ;

namespace Sonny.Application.Infrastructure.Services ;

public class CadLinkSelector(IResourceHelper resourceHelper) : ICadLinkSelector
{
    public ImportInstance? SelectCadLink(UIDocument uiDocument)
    {
        try {
            var typesFilter = new List<Type> { typeof( ImportInstance ) } ;

            var reference = uiDocument.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element,
                new TypeSelectionFilter(typesFilter),
                resourceHelper.GetString("MessageSelectCadLink")) ;

            return uiDocument.Document.GetElementById<ImportInstance>(reference) ;
        }
        catch (Exception) {
            return null ;
        }
    }
}

