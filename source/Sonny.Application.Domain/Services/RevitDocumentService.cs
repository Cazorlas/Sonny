using Autodesk.Revit.UI ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.RevitExtensions.Extensions ;

namespace Sonny.Application.Domain.Services ;

public class RevitDocumentService(IUIDocumentProvider uiDocumentProvider) : IRevitDocument
{
    public Document Document => UIDocument.Document ;

    public UIDocument UIDocument { get ; } = uiDocumentProvider.GetUIDocument() ;

    public View ActiveView => UIDocument.ActiveView ;

    public UIApplication Application => UIDocument.Application ;

    public List<DimensionType> GetDimensionTypes() =>
        Document.GetAllElements<DimensionType>()
            .Where(x => x.StyleType == DimensionStyleType.Linear)
            .ToList() ;
}
