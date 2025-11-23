using SonnyApplication.Features.AutoColumnDimension.Interfaces ;
using SonnyRevitExtensions.Extensions.Elements ;
using SonnyRevitExtensions.Extensions.GeometryObjects.Solids ;
using SonnyRevitExtensions.Extensions.Views ;
using SonnyRevitExtensions.Extensions.XYZs ;
using SonnyRevitExtensions.RevitWrapper ;

namespace SonnyApplication.Features.AutoColumnDimension.Services ;

/// <summary>
/// Immutable context containing information to create dimension for column
/// </summary>
public class ColumnDimensionContext
{
    public List<PlanarFace> PlanarFaces { get ; }
    public XYZ MaxPoint { get ; }
    public XYZ FirstDirection { get ; }
    public GridWrapperBase? FirstGridWrapper { get ; }
    public XYZ SecondDirection { get ; }
    public GridWrapperBase? SecondGridWrapper { get ; }

    private ColumnDimensionContext(List<PlanarFace> planarFaces,
        XYZ maxPoint,
        XYZ firstDirection,
        XYZ secondDirection,
        GridWrapperBase? firstGridWrapper,
        GridWrapperBase? secondGridWrapper)
    {
        PlanarFaces = planarFaces ;
        MaxPoint = maxPoint ;
        FirstDirection = firstDirection ;
        SecondDirection = secondDirection ;
        FirstGridWrapper = firstGridWrapper ;
        SecondGridWrapper = secondGridWrapper ;
    }

    public static ColumnDimensionContext? Create(ColumnWrapperBase columnWrapper,
        ViewWrapperBase viewWrapper,
        IGridFinder gridFinder)
    {
        if (columnWrapper.GetBoundingBoxXyz(viewWrapper) is not { } boundingBox)
        {
            return null ;
        }

        var options = viewWrapper.View.CreateDimensionOptions() ;
        var solids = columnWrapper.Element.GetSolids(options) ;
        var planarFaces = solids.GetPlanarFaces()
            .ToList() ;

        var midPoint = 0.5 * (boundingBox.Min + boundingBox.Max) ;

        XYZ firstDirection ;
        XYZ secondDirection ;
        GridWrapperBase? firstGridWrapper = null ;
        GridWrapperBase? secondGridWrapper = null ;

        if (viewWrapper.IsViewPlan)
        {
            firstDirection = columnWrapper.FamilyInstance.HandOrientation ;
            secondDirection = columnWrapper.FamilyInstance.FacingOrientation ;

            firstGridWrapper = gridFinder.GetNearestGrid(secondDirection,
                midPoint,
                firstDirection,
                viewWrapper) ;

            secondGridWrapper = gridFinder.GetNearestGrid(firstDirection,
                midPoint,
                secondDirection,
                viewWrapper) ;
        }
        else
        {
            firstDirection = viewWrapper.View.UpDirection ;
            secondDirection = viewWrapper.View.RightDirection ;

            if (! firstDirection.IsParallel(XYZ.BasisZ))
            {
                firstGridWrapper = gridFinder.GetNearestGrid(viewWrapper.View.ViewDirection,
                    midPoint,
                    firstDirection,
                    viewWrapper) ;
            }

            if (! secondDirection.IsParallel(XYZ.BasisZ))
            {
                secondGridWrapper = gridFinder.GetNearestGrid(viewWrapper.View.ViewDirection,
                    midPoint,
                    secondDirection,
                    viewWrapper) ;
            }
        }

        var maxPoint = new XYZ(boundingBox.Max.X,
            boundingBox.Max.Y,
            boundingBox.Max.Z) ;

        return new ColumnDimensionContext(planarFaces,
            maxPoint,
            firstDirection,
            secondDirection,
            firstGridWrapper,
            secondGridWrapper) ;
    }
}


