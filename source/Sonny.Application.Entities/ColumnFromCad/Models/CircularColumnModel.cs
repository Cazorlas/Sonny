namespace Sonny.Application.Entities.ColumnFromCad ;

public class CircularColumnModel(double diameter, Point3D center) : ColumnModel(center)
{
    public double Diameter { get ; } = diameter ;
}
