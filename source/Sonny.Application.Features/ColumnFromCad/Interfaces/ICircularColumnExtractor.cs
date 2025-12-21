using Sonny.Application.Features.ColumnFromCad.Models ;

namespace Sonny.Application.Features.ColumnFromCad.Interfaces ;

/// <summary>
///     Interface for extracting circular columns from CAD link
/// </summary>
public interface ICircularColumnExtractor
{
    /// <summary>
    ///     Extracts circular columns from boundary lines (arcs)
    /// </summary>
    /// <param name="cadInstance">The CAD import instance</param>
    /// <param name="selectedLayer">The selected layer name</param>
    /// <returns>List of circular column data</returns>
    List<CircularColumnModel> ExtractFromBoundaryLines(ImportInstance cadInstance,
        string selectedLayer) ;

    /// <summary>
    ///     Extracts circular columns from planar faces (hatch)
    /// </summary>
    /// <param name="cadInstance">The CAD import instance</param>
    /// <param name="selectedLayer">The selected layer name</param>
    /// <returns>List of circular column data</returns>
    List<CircularColumnModel> ExtractFromPlanarFaces(ImportInstance cadInstance,
        string selectedLayer) ;
}
