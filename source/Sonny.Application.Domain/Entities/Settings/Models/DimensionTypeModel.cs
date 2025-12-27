namespace Sonny.Application.Domain.Entities.Settings.Models ;

public class DimensionTypeModel(string uniqueId, string name, double snapDistance)
{
    public string UniqueId { get ; } = uniqueId ;
    public string Name { get ; } = name ;
    public double SnapDistance { get ; } = snapDistance ;
    public override string ToString() => Name ;
}
