using Autodesk.Revit.UI ;

namespace Sonny.Application.Core.Interfaces ;

/// <summary>
///     Interface for providing UIDocument from Revit context
/// </summary>
public interface IUIDocumentProvider
{
    /// <summary>
    ///     Gets the current active UIDocument
    /// </summary>
    UIDocument GetUIDocument() ;

    /// <summary>
    ///     Sets the current UIDocument
    /// </summary>
    /// <param name="uidoc">The UIDocument to set</param>
    void SetUIDocument(UIDocument uidoc) ;
}
