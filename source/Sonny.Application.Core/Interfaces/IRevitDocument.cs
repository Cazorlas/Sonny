using Autodesk.Revit.UI ;

namespace Sonny.Application.Core.Interfaces ;

/// <summary>
///     Interface to abstract Revit Document operations
/// </summary>
public interface IRevitDocument
{
    /// <summary>
    ///     Gets the Revit Document
    /// </summary>
    Document Document { get ; }

    /// <summary>
    ///     Gets the Revit UIDocument
    /// </summary>
    UIDocument UIDocument { get ; }

    /// <summary>
    ///     Gets the active view
    /// </summary>
    View ActiveView { get ; }

    /// <summary>
    ///     Gets the UIApplication
    /// </summary>
    UIApplication Application { get ; }

    /// <summary>
    ///     Gets dimension types from the document
    /// </summary>
    /// <returns>List of DimensionType</returns>
    List<DimensionType> GetDimensionTypes() ;
}
