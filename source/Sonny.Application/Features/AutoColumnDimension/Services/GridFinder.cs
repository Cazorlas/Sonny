using Sonny.Application.Features.AutoColumnDimension.Interfaces ;
using Sonny.RevitExtensions.Extensions.XYZs ;
using Sonny.RevitExtensions.RevitWrapper ;

namespace Sonny.Application.Features.AutoColumnDimension.Services ;

/// <summary>
///     Service for finding the nearest grid based on direction and point
/// </summary>
public class GridFinder : IGridFinder
{
    /// <summary>
    ///     Gets the nearest grid to a point based on grid direction and product direction
    /// </summary>
    /// <param name="gridDirection">The direction of the grid line</param>
    /// <param name="midPoint">The point to find the nearest grid from</param>
    /// <param name="productDirection">The direction perpendicular to the grid for distance calculation</param>
    /// <param name="viewWrapper">The view wrapper containing grid wrappers</param>
    /// <returns>The nearest grid wrapper, or null if not found</returns>
    public GridWrapperBase? GetNearestGrid(XYZ gridDirection,
        XYZ midPoint,
        XYZ productDirection,
        ViewWrapperBase viewWrapper) =>
        viewWrapper.GridWrappers
            .Where(x => x.Line != null && x.Line.Direction.IsParallel(gridDirection))
            .OrderBy(x => midPoint.DistancePointToLine(x.Line!,
                productDirection))
            .FirstOrDefault() ;
}
