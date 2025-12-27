using Sonny.Application.Domain.Entities.Settings.Models ;

namespace Sonny.Application.UseCases.ColumnFromCad.Services ;

public interface IColumnFromCadContext
{
    string SelectedCadLinkId { get ; }
    HashSet<string> LayerNames { get ; }
    List<FamilyModel> ColumnFamilies { get ; }
    List<LevelModel> Levels { get ; }

    /// <summary>
    ///     Numeric parameters for the first family (or selected family)
    ///     Key: Family Id (as string), Value: List of parameter names
    /// </summary>
    Dictionary<string, HashSet<string>> FamilyNumericParameters { get ; }
}
