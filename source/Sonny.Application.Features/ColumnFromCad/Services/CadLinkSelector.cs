using Autodesk.Revit.UI ;
using Sonny.Application.Core.SelectionFilters ;
using Sonny.Application.Features.ColumnFromCad.Interfaces ;
using Sonny.RevitExtensions.Extensions ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Features.ColumnFromCad.Services ;

public class CadLinkSelector : ICadLinkSelector
{
    public ImportInstance? SelectCadLink(UIDocument uiDocument)
    {
        try {
            var typesFilter = new List<Type> { typeof( ImportInstance ) } ;

            var reference = uiDocument.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element,
                new TypeSelectionFilter(typesFilter),
                ResourceHelper.GetString("MessageSelectCadLink")) ;

            return uiDocument.Document.GetElementById<ImportInstance>(reference) ;
        }
        catch (Exception) {
            return null ;
        }
    }
}
