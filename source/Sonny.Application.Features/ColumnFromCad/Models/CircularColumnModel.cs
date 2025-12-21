namespace Sonny.Application.Features.ColumnFromCad.Models ;

public class CircularColumnModel : ColumnModel
{
    public CircularColumnModel(Arc arc)
    {
        Center = arc.Center ;
        Diameter = arc.Radius * 2 ;
    }

    public double Diameter { get ; }
}
