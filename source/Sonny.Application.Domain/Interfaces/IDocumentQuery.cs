using Autodesk.Revit.DB ;

namespace Sonny.Application.UseCases.Interfaces ;

/// <summary>
///     Interface for querying elements from Revit Document
/// </summary>
public interface IDocumentQuery
{
    /// <summary>
    ///     Get all elements of specified type from document
    /// </summary>
    /// <typeparam name="TElement">Type of element to query</typeparam>
    /// <returns>Enumerable collection of elements</returns>
    IEnumerable<TElement> GetAllElements<TElement>() where TElement : Element ;
}


