using System.Diagnostics ;
using Sonny.Application.Features.AutoColumnDimension.Interfaces ;
using Sonny.RevitExtensions.Extensions.GeometryObjects.Faces.PlanarFaces ;
using Sonny.RevitExtensions.Extensions.XYZs ;
using Sonny.RevitExtensions.RevitWrapper ;
using Sonny.RevitExtensions.Utilities ;

namespace Sonny.Application.Features.AutoColumnDimension.Services ;

/// <summary>
///     Service for creating dimensions by direction
/// </summary>
public class DimensionCreator : IDimensionCreator
{
    public List<ElementWrapperBase> DimensionByDirection(List<PlanarFace> planarFaces,
        XYZ direction,
        XYZ offsetDirection,
        GridWrapperBase? gridWrapper,
        XYZ point,
        double snapDistance,
        ViewWrapperBase viewWrapper,
        DimensionType? dimensionType)
    {
        var dimensionWrappers = new List<ElementWrapperBase>() ;

        var planarFacesByDirection = planarFaces.Where(x => x.FaceNormal.IsParallel(direction))
            .ToList() ;

        // Remove coplanar faces to avoid duplicates
        planarFacesByDirection = planarFacesByDirection.RemoveCoplanarFaces()
            .ToList() ;
        if (planarFacesByDirection.Count < 2)
        {
            return dimensionWrappers ;
        }

        var basis = offsetDirection.IsParallel(XYZ.BasisX)
            ? XYZ.BasisX
            : offsetDirection.IsParallel(XYZ.BasisY)
                ? XYZ.BasisY
                : offsetDirection.IsParallel(XYZ.BasisZ)
                    ? XYZ.BasisZ
                    : offsetDirection ;
        var offset = basis * snapDistance ;
        var line = Line.CreateUnbound(point + offset,
            direction) ;

        if (gridWrapper != null
            && planarFacesByDirection.TrueForAll(x => ! PlanarFaceExtensions.AreFacesCoplanar(x.FaceNormal,
                x.Origin,
                direction,
                gridWrapper.Line!.Origin)))
        {
            var referenceArray = new ReferenceArray() ;
            planarFacesByDirection.ForEach(x => referenceArray.Append(x.Reference)) ;
            referenceArray.Append(new Reference(gridWrapper.Element)) ;

            try
            {
                var newDimension = CreateElement.CreateDimension(viewWrapper.View,
                    line,
                    referenceArray,
                    dimensionType,
                    Constraint.Tolerance1E4) ;

                dimensionWrappers.Add(new DimensionWrapperBase(newDimension)) ;

                line = Line.CreateUnbound(point + 2 * offset,
                    direction) ;
            }
            catch (Exception ex)
            {
                // Log error but continue to create dimension without grid reference
                // Exception details are logged by the caller if logger is available
                Debug.WriteLine($"Failed to create dimension with grid reference: {ex.Message}") ;
            }
        }

        var referenceArray1 = new ReferenceArray() ;
        planarFacesByDirection.ForEach(x => referenceArray1.Append(x.Reference)) ;

        try
        {
            var newDimension1 = CreateElement.CreateDimension(viewWrapper.View,
                line,
                referenceArray1,
                dimensionType,
                Constraint.Tolerance1E4) ;

            dimensionWrappers.Add(new DimensionWrapperBase(newDimension1)) ;
        }
        catch (Exception ex)
        {
            // Log error but return partial results
            // Exception details are logged by the caller if logger is available
            Debug.WriteLine($"Failed to create dimension: {ex.Message}") ;
        }

        return dimensionWrappers ;
    }
}
