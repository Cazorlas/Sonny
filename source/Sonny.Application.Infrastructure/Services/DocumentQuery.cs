using Autodesk.Revit.DB ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.RevitExtensions.Extensions ;

namespace Sonny.Application.Infrastructure.Services ;

/// <summary>
///     Implementation of IDocumentQuery using Sonny.RevitExtensions extension methods
/// </summary>
public class DocumentQuery : IDocumentQuery
{
    private readonly Document _document ;

    /// <summary>
    ///     Initializes a new instance of DocumentQuery
    /// </summary>
    /// <param name="document">Revit Document instance</param>
    public DocumentQuery(Document document)
    {
        _document = document ;
    }

    /// <summary>
    ///     Get all elements of specified type from document
    /// </summary>
    /// <typeparam name="TElement">Type of element to query</typeparam>
    /// <returns>Enumerable collection of elements</returns>
    public IEnumerable<TElement> GetAllElements<TElement>() where TElement : Element
    {
        return _document.GetAllElements<TElement>() ; // Extension method from Sonny.RevitExtensions
    }
}

