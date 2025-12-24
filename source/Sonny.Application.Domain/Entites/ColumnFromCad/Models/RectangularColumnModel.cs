namespace Sonny.Application.Domain.Entites.ColumnFromCad.Models ;

public class RectangularColumnModel(double shortSide, double longSide, Point3D center, double rotationAngle)
    : ColumnModel(center)
{
    public double ShortSide { get ; } = shortSide ;

    public double LongSide { get ; } = longSide ;

    /// <summary>
    ///     Rotation angle between BasisX and short side, in radians
    ///     Property: 0 <= RotationAngle <= 90
    /// </summary>
    public double RotationAngle { get ; } = rotationAngle ;
}
