namespace Sonny.Application.Entities.ColumnFromCad ;

public class RectangularColumnModel(double shortSide, double longSide, Point3D center, double rotationAngle)
    : ColumnModel(center)
{
    /// <summary>
    ///     Short side length
    /// </summary>
    public double ShortSide { get ; } = shortSide ;

    /// <summary>
    ///     Long side length
    /// </summary>
    public double LongSide { get ; } = longSide ;

    /// <summary>
    ///     Rotation angle between BasisX and short side, in radians
    ///     Property: 0 <= RotationAngle <= 90
    /// </summary>
    public double RotationAngle { get ; } = rotationAngle ;
}
