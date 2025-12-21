namespace Sonny.Application.Features.ColumnFromCad.Interfaces ;

/// <summary>
///     Context interface for storing column from CAD business data
///     Separates business data from ViewModel (UI state)
/// </summary>
public interface IColumnFromCadContext
{
    /// <summary>
    ///     Selected CAD link instance
    /// </summary>
    ImportInstance SelectedCadLink { get ; }

    /// <summary>
    ///     Available layers from CAD link
    /// </summary>
    HashSet<string> LayerNames { get ; }

    /// <summary>
    ///     All available column families from document
    /// </summary>
    List<Family> ColumnFamilies { get ; }

    /// <summary>
    ///     Numeric parameters for the first family (or selected family)
    ///     Key: Family Id, Value: List of parameter names
    /// </summary>
    Dictionary<ElementId, HashSet<string>> FamilyNumericParameters { get ; }
}
