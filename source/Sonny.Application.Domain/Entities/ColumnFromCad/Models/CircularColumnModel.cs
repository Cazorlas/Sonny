namespace Sonny.Application.Domain.Entities.ColumnFromCad.Models ;

public class CircularColumnModel(double diameter, Point3D center) : ColumnModel(center)
{
    public double Diameter { get ; } = diameter ;
}
