namespace Sonny.Application.Domain.Services ;

/// <summary>
///     Interface for selecting elements in Revit UI
/// </summary>
public interface IElementSelector
{
    /// <summary>
    ///     Selects elements by their unique IDs
    /// </summary>
    /// <param name="uniqueIds">Collection of unique IDs to select</param>
    void SelectElements(ICollection<string> uniqueIds) ;
}
