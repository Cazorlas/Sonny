using Sonny.Application.Features.ColumnFromCad.Models ;

namespace Sonny.Application.Features.ColumnFromCad.Interfaces ;

/// <summary>
///     Interface for extracting rectangular columns from CAD link
/// </summary>
public interface IRectangularColumnExtractor
{
    /// <summary>
    ///     Extracts rectangular columns from boundary lines (poly lines)
    /// </summary>
    /// <param name="cadInstance">The CAD import instance</param>
    /// <param name="selectedLayer">The selected layer name</param>
    /// <returns>List of rectangular column data</returns>
    List<RectangularColumnModel> ExtractFromBoundaryLines(ImportInstance cadInstance,
        string selectedLayer) ;

    /// <summary>
    ///     Extracts rectangular columns from planar faces (hatch)
    /// </summary>
    /// <param name="cadInstance">The CAD import instance</param>
    /// <param name="selectedLayer">The selected layer name</param>
    /// <returns>List of rectangular column data</returns>
    List<RectangularColumnModel> ExtractFromPlanarFaces(ImportInstance cadInstance,
        string selectedLayer) ;
}
