using Autodesk.Revit.UI ;
using Sonny.Application.Core.Interfaces ;
using Sonny.RevitExtensions.Extensions ;

namespace Sonny.Application.Core.Services ;

/// <summary>
///     Implementation of IRevitDocument using Revit API
/// </summary>
public class RevitDocumentService : IRevitDocument
{
    private readonly UIDocument _uiDocument ;

    /// <summary>
    ///     Initializes a new instance of RevitDocumentService
    /// </summary>
    /// <param name="uiDocumentProvider">The UIDocument provider</param>
    public RevitDocumentService(IUIDocumentProvider uiDocumentProvider)
    {
        _uiDocument = uiDocumentProvider.GetUIDocument() ;
    }

    /// <summary>
    ///     Gets the Revit Document
    /// </summary>
    public Document Document => _uiDocument.Document ;

    /// <summary>
    ///     Gets the active view
    /// </summary>
    public View ActiveView => _uiDocument.ActiveView ;

    /// <summary>
    ///     Gets the UIApplication
    /// </summary>
    public UIApplication Application => _uiDocument.Application ;

    /// <summary>
    ///     Gets dimension types from the document
    /// </summary>
    /// <returns>List of DimensionType</returns>
    public List<DimensionType> GetDimensionTypes() =>
        Document.GetAllElements<DimensionType>()
            .Where(x => x.StyleType == DimensionStyleType.Linear)
            .ToList() ;
}
