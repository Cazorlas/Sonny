namespace Sonny.Application.Domain.Entities.Settings.Models ;

public class FamilyModel(string uniqueId, string name)
{
    public string UniqueId { get ; } = uniqueId ;
    public string Name { get ; } = name ;
    public override string ToString() => Name ;
}
