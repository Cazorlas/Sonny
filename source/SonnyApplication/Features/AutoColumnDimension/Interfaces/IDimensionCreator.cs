using Sonny.RevitExtensions.RevitWrapper ;

namespace SonnyApplication.Features.AutoColumnDimension.Interfaces ;

/// <summary>
/// Interface for creating dimensions by direction
/// </summary>
public interface IDimensionCreator
{
    /// <summary>
    /// Creates dimensions for planar faces along a specified direction
    /// </summary>
    /// <param name="planarFaces">List of planar faces to dimension</param>
    /// <param name="direction">The direction along which to create the dimension</param>
    /// <param name="offsetDirection">The direction for offsetting the dimension line</param>
    /// <param name="gridWrapper">Optional grid wrapper to include as reference</param>
    /// <param name="point">The point where the dimension line passes through</param>
    /// <param name="snapDistance">The snap distance for the dimension</param>
    /// <param name="viewWrapper">The view wrapper containing the view</param>
    /// <param name="dimensionType">Optional dimension type to use</param>
    /// <returns>List of created dimension element wrappers</returns>
    List<ElementWrapperBase> DimensionByDirection(List<PlanarFace> planarFaces,
        XYZ direction,
        XYZ offsetDirection,
        GridWrapperBase? gridWrapper,
        XYZ point,
        double snapDistance,
        ViewWrapperBase viewWrapper,
        DimensionType? dimensionType) ;
}
