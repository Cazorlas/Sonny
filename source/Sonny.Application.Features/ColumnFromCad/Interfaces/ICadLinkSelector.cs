using Autodesk.Revit.UI ;

namespace Sonny.Application.Features.ColumnFromCad.Interfaces ;

/// <summary>
///     Interface for selecting CAD link from document
/// </summary>
public interface ICadLinkSelector
{
    /// <summary>
    ///     Selects a CAD link (ImportInstance) from the document
    /// </summary>
    /// <param name="uiDocument">Revit ui document</param>
    /// <returns>Selected ImportInstance, or null if cancelled</returns>
    ImportInstance? SelectCadLink(UIDocument uiDocument) ;
}
