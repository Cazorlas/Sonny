using Autodesk.Revit.UI ;
using Sonny.Application.UseCases.Interfaces ;
using Sonny.Application.UseCases.SelectionFilters ;
using Sonny.Application.Features.ColumnFromCad.Interfaces ;
using Sonny.RevitExtensions.Extensions ;

namespace Sonny.Application.Features.ColumnFromCad.Services ;

public class CadLinkSelector : ICadLinkSelector
{
    private readonly IResourceHelper _resourceHelper ;

    public CadLinkSelector(IResourceHelper resourceHelper)
    {
        _resourceHelper = resourceHelper ;
    }

    public ImportInstance? SelectCadLink(UIDocument uiDocument)
    {
        try {
            var typesFilter = new List<Type> { typeof( ImportInstance ) } ;

            var reference = uiDocument.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element,
                new TypeSelectionFilter(typesFilter),
                _resourceHelper.GetString("MessageSelectCadLink")) ;

            return uiDocument.Document.GetElementById<ImportInstance>(reference) ;
        }
        catch (Exception) {
            return null ;
        }
    }
}
