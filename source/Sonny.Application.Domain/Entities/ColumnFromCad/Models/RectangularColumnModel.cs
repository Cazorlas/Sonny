namespace Sonny.Application.Domain.Entities.ColumnFromCad.Models ;

public class RectangularColumnModel(double shortSide, double longSide, Point3D center, double rotationAngle)
    : ColumnModel(center)
{
    public double ShortSide { get ; } = shortSide ;
    public double LongSide { get ; } = longSide ;
    public double RotationAngle { get ; } = rotationAngle ;
}
