using Sonny.Application.Domain.Entites.ColumnFromCad.Models ;
using Sonny.Application.Domain.Interfaces ;

namespace Sonny.Application.Infrastructure.Services ;

public class ColumnModelFactory : IColumnModelFactory
{
    private readonly IGeometryHelper _geometryHelper ;
    private readonly IPoint3DConverter _point3DConverter ;

    public ColumnModelFactory(IGeometryHelper geometryHelper, IPoint3DConverter point3DConverter)
    {
        _geometryHelper = geometryHelper ;
        _point3DConverter = point3DConverter ;
    }

    public RectangularColumnModel CreateRectangular(List<Curve> curves)
    {
        if (curves.Count < 4) {
            throw new ArgumentException("Curves list must contain at least 4 curves to form a rectangle.",
                nameof(curves)) ;
        }

        double shortSide ;
        double longSide ;
        Curve shortSideCurve ;
        Curve longSideCurve ;

        if (curves[0].Length > curves[1].Length) {
            shortSide = curves[1].Length ;
            longSide = curves[0].Length ;
            shortSideCurve = curves[1] ;
            longSideCurve = curves[0] ;
        }
        else {
            shortSide = curves[0].Length ;
            longSide = curves[1].Length ;
            shortSideCurve = curves[0] ;
            longSideCurve = curves[1] ;
        }

        var line = Line.CreateBound(curves[0].GetEndPoint(0),
            curves[1].GetEndPoint(1)) ;

        var centerXyz = _geometryHelper.GetMidpoint(line) ;
        var center = _point3DConverter.FromXyz(centerXyz) ;
        var rotationAngle = 0.0 ;

        if (_geometryHelper.GetDirection(shortSideCurve) is { } direction) {
            // Calculate the angle between BasisX and the short side direction vector
            rotationAngle = XYZ.BasisX.AngleTo(direction) ;

            // Adjust rotation angle based on the quadrant of the direction vector
            if (direction.X > 0 && direction.Y < 0) {
                // Quadrant IV
                rotationAngle = Math.PI - rotationAngle ;
            }
            else if (direction.X > 0 && direction.Y > 0) {
                // Quadrant I
                rotationAngle = Math.PI / 2 + rotationAngle ;
            }
            else if (direction.X < 0 && direction.Y < 0) {
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
        var center = _point3DConverter.FromXyz(arc.Center) ;
        var diameter = arc.Radius * 2 ;
        return new CircularColumnModel(diameter, center) ;
    }
}

