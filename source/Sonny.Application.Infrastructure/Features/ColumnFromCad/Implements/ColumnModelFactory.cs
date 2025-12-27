using Sonny.Application.Domain.Entities.ColumnFromCad.Models ;
using Sonny.Application.Infrastructure.Features.ColumnFromCad.Services ;
using Sonny.Application.Infrastructure.Revit.Services ;
using Sonny.RevitExtensions.Extensions.GeometryObjects.Curves ;
using Sonny.RevitExtensions.Extensions.GeometryObjects.Curves.Lines ;

namespace Sonny.Application.Infrastructure.Features.ColumnFromCad.Implements ;

public class ColumnModelFactory(IPoint3DConverter point3DConverter) : IColumnModelFactory
{
    public RectangularColumnModel CreateRectangular(List<Curve> curves)
    {
        if (curves.Count < 4) {
            throw new ArgumentException("Curves list must contain at least 4 curves to form a rectangle.",
                nameof( curves )) ;
        }

        double shortSide ;
        double longSide ;
        Curve shortSideCurve ;

        if (curves[0].Length > curves[1].Length) {
            shortSide = curves[1].Length ;
            longSide = curves[0].Length ;
            shortSideCurve = curves[1] ;
        }
        else {
            shortSide = curves[0].Length ;
            longSide = curves[1].Length ;
            shortSideCurve = curves[0] ;
        }

        var line = Line.CreateBound(curves[0]
                .GetEndPoint(0),
            curves[1]
                .GetEndPoint(1)) ;

        var centerXyz = line.GetMidpoint() ;
        var center = point3DConverter.FromXyz(centerXyz) ;
        var rotationAngle = 0.0 ;

        if (shortSideCurve.Direction() is { } direction) {
            // Calculate the angle between BasisX and the short side direction vector
            rotationAngle = XYZ.BasisX.AngleTo(direction) ;

            // Adjust rotation angle based on the quadrant of the direction vector
            if (direction.X > 0
                && direction.Y < 0) {
                // Quadrant IV
                rotationAngle = Math.PI - rotationAngle ;
            }
            else if (direction.X > 0
                     && direction.Y > 0) {
                // Quadrant I
                rotationAngle = Math.PI / 2 + rotationAngle ;
            }
            else if (direction.X < 0
                     && direction.Y < 0) {
                // Quadrant III
                rotationAngle = Math.PI - rotationAngle ;
            }
            // Quadrant II uses the angle as-is
        }

        return new RectangularColumnModel(shortSide,
            longSide,
            center,
            rotationAngle) ;
    }

    public CircularColumnModel CreateCircular(Arc arc)
    {
        var center = point3DConverter.FromXyz(arc.Center) ;
        var diameter = arc.Radius * 2 ;
        return new CircularColumnModel(diameter,
            center) ;
    }
}
