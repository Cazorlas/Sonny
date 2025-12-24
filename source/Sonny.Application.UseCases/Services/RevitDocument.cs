using Autodesk.Revit.UI ;
using Sonny.Application.Domain.Interfaces ;

namespace Sonny.Application.UseCases.Services ;

public class RevitDocument : IRevitDocument
{
    private readonly IDocumentQuery _documentQuery ;

    /// <summary>
    ///     Initializes a new instance of RevitDocument
    /// </summary>
    /// <param name="uiDocumentProvider">UIDocument provider</param>
    /// <param name="documentQuery">Document query service</param>
    public RevitDocument(
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
