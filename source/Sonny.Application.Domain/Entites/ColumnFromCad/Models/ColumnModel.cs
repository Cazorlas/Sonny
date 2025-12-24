namespace Sonny.Application.Domain.Entites.ColumnFromCad.Models ;

public abstract class ColumnModel(Point3D center)
{
    public Point3D Center { get ; } = center ;
}
