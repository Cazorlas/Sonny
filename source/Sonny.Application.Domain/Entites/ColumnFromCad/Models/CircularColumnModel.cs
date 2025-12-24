namespace Sonny.Application.Domain.Entites.ColumnFromCad.Models ;

public class CircularColumnModel(double diameter, Point3D center) : ColumnModel(center)
{
    public double Diameter { get ; } = diameter ;
}
