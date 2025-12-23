using Autodesk.Revit.UI ;
using Sonny.Application.Domain.InputPorts.ColumnFromCad ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.Infrastructure.SelectionFilters ;
using Sonny.RevitExtensions.Extensions ;

namespace Sonny.Application.Infrastructure.Services ;

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

