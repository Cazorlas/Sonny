using Sonny.Application.Infrastructure.Features.AutoColumnDimension.Services ;
using Sonny.RevitExtensions.Extensions.XYZs ;
using Sonny.RevitExtensions.RevitWrapper ;

namespace Sonny.Application.Infrastructure.Features.AutoColumnDimension.Implements ;

public class GridFinder : IGridFinder
{
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
