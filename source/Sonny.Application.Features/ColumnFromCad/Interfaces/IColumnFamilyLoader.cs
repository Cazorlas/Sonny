namespace Sonny.Application.Features.ColumnFromCad.Interfaces ;

/// <summary>
///     Interface for loading column families and parameters from document
/// </summary>
public interface IColumnFamilyLoader
{
    /// <summary>
    ///     Gets all column families from document (structural and architectural columns)
    /// </summary>
    /// <param name="document">Revit document</param>
    /// <returns>List of column families, ordered by name</returns>
    List<Family> GetAllColumnFamilies(Document document) ;

    /// <summary>
    ///     Gets all numeric parameters from a family's first symbol
    /// </summary>
    /// <param name="family">Column family</param>
    /// <returns>List of parameter names (excluding Assembly, OmniClass, Material, Category, Type)</returns>
    HashSet<string> GetNumericParameters(Family family) ;
}
