using Sonny.RevitExtensions.Extensions.GeometryObjects.Curves ;
using Sonny.RevitExtensions.Extensions.GeometryObjects.Curves.Lines ;

namespace Sonny.Application.Features.ColumnFromCad.Models ;

public class RectangularColumnModel : ColumnModel
{
    public RectangularColumnModel(List<Curve> curves)
    {
        if (curves.Count < 4)
        {
            throw new ArgumentException("Curves list must contain at least 4 curves to form a rectangle.",
                nameof( curves )) ;
        }

        if (curves[0].Length > curves[1].Length)
        {
            ShortSide = curves[1].Length ;
            LongSide = curves[0].Length ;
            ShortSideCurve = curves[1] ;
            LongSideCurve = curves[0] ;
        }
        else
        {
            ShortSide = curves[0].Length ;
            LongSide = curves[1].Length ;
            ShortSideCurve = curves[0] ;
            LongSideCurve = curves[1] ;
        }

        var line = Line.CreateBound(curves[0]
                .GetEndPoint(0),
            curves[1]
                .GetEndPoint(1)) ;

        Center = line.GetMidpoint() ;
        RotationAngle = 0 ;

        if (ShortSideCurve.Direction() is not { } direction)
        {
            return ;
        }

        // Calculate the angle between BasisX and the short side direction vector
        // AngleTo returns angle in range [0, PI]
        RotationAngle = XYZ.BasisX.AngleTo(direction) ;

        // Adjust rotation angle based on the quadrant of the direction vector
        // This ensures the rotation angle is correctly calculated for all orientations
        if (direction.X > 0
            && direction.Y < 0)
        {
            // Quadrant IV: Positive X, Negative Y
            // Adjust angle: 180° - angle
            RotationAngle = Math.PI - RotationAngle ;
        }
        else if (direction.X > 0
                 && direction.Y > 0)
        {
            // Quadrant I: Positive X, Positive Y
            // Adjust angle: 90° + angle
            RotationAngle = Math.PI / 2 + RotationAngle ;
        }
        else if (direction.X < 0
                 && direction.Y < 0)
        {
            // Quadrant III: Negative X, Negative Y
            // Adjust angle: 180° - angle
            RotationAngle = Math.PI - RotationAngle ;
        }
        // Quadrant II (Negative X, Positive Y) uses the angle as-is from AngleTo
    }

    /// <summary>
    ///     Short side length
    /// </summary>
    public double ShortSide { get ; }

    /// <summary>
    ///     Short side curve
    /// </summary>
    public Curve ShortSideCurve { get ; }

    /// <summary>
    ///     Long side length
    /// </summary>
    public double LongSide { get ; }

    /// <summary>
    ///     Long side curve
    /// </summary>
    public Curve LongSideCurve { get ; }

    /// <summary>
    ///     Rotation angle between BasisX and short side, in radians
    ///     Property: 0 <= RotationAngle <= 90
    /// </summary>
    public double RotationAngle { get ; }
}
