using Autodesk.Revit.UI ;
using Sonny.Application.Core.Interfaces ;
using Sonny.RevitExtensions.Extensions ;

namespace Sonny.Application.Core.Services ;

/// <summary>
///     Implementation of IRevitDocument using Revit API
/// </summary>
public class RevitDocumentService : IRevitDocument
{
    private readonly UIDocument _uidoc ;

    /// <summary>
    ///     Initializes a new instance of RevitDocumentService
    /// </summary>
    /// <param name="uidoc">The UIDocument</param>
    public RevitDocumentService(UIDocument uidoc) => _uidoc = uidoc ;

    /// <summary>
    ///     Gets the Revit Document
    /// </summary>
    public Document Document => _uidoc.Document ;

    /// <summary>
    ///     Gets the active view
    /// </summary>
    public View ActiveView => _uidoc.ActiveView ;

    /// <summary>
    ///     Gets the UIApplication
    /// </summary>
    public UIApplication Application => _uidoc.Application ;

    /// <summary>
    ///     Gets dimension types from the document
    /// </summary>
    /// <returns>List of DimensionType</returns>
    public List<DimensionType> GetDimensionTypes() =>
        Document.GetAllElements<DimensionType>()
            .Where(x => x.StyleType == DimensionStyleType.Linear)
            .ToList() ;
}
