using Autodesk.Revit.UI ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.Infrastructure.Revit.SelectionFilters ;
using Sonny.Application.Infrastructure.Revit.Services ;
using Sonny.RevitExtensions.Extensions ;

namespace Sonny.Application.Infrastructure.Revit.Implements ;

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
