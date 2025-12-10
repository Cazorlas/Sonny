using Autodesk.Revit.UI ;
using Sonny.Application.Core.Interfaces ;

namespace Sonny.Application.Core.Services ;

/// <summary>
///     Service for providing UIDocument from Revit context
/// </summary>
public class UIDocumentProvider : IUIDocumentProvider
{
    private UIDocument? _uiDocument ;
    private readonly object _lock = new() ;

    /// <summary>
    ///     Gets the current active UIDocument
    /// </summary>
    public UIDocument GetUIDocument()
    {
        lock (_lock)
        {
            if (_uiDocument == null)
            {
                throw new InvalidOperationException(
                    "UIDocument is not available. Make sure to set it in command context.") ;
            }

            return _uiDocument ;
        }
    }

    /// <summary>
    ///     Sets the current UIDocument
    /// </summary>
    /// <param name="uidoc">The UIDocument to set</param>
    public void SetUIDocument(UIDocument uidoc)
    {
        lock (_lock)
        {
            _uiDocument = uidoc ;
        }
    }
}
