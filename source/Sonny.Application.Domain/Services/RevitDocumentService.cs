using Autodesk.Revit.UI ;
using Sonny.Application.Domain.Interfaces ;

namespace Sonny.Application.Domain.Services ;

public class RevitDocumentService : IRevitDocument
{
    private readonly IDocumentQuery _documentQuery ;

    /// <summary>
    ///     Initializes a new instance of RevitDocumentService
    /// </summary>
    /// <param name="uiDocumentProvider">UIDocument provider</param>
    /// <param name="documentQuery">Document query service</param>
    public RevitDocumentService(
        IUIDocumentProvider uiDocumentProvider,
        IDocumentQuery documentQuery)
    {
        UIDocument = uiDocumentProvider.GetUIDocument() ;
        _documentQuery = documentQuery ;
    }

    public Document Document => UIDocument.Document ;

    public UIDocument UIDocument { get ; }

    public View ActiveView => UIDocument.ActiveView ;

    public UIApplication Application => UIDocument.Application ;

    public List<DimensionType> GetDimensionTypes() =>
        _documentQuery.GetAllElements<DimensionType>()
            .Where(x => x.StyleType == DimensionStyleType.Linear)
            .ToList() ;
}
