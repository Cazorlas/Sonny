using Sonny.Application.Domain.Entities.ColumnFromCad.Contexts ;
using Sonny.Application.Domain.Entities.ColumnFromCad.Models ;

namespace Sonny.Application.Domain.Entities.ColumnFromCad.Services ;

/// <summary>
///     Interface for extracting column data from CAD link
/// </summary>
public interface IColumnDataExtractor
{
    /// <summary>
    ///     Extracts column data from CAD link based on context settings
    /// </summary>
    /// <param name="context">Column creation context containing settings</param>
    /// <returns>List of extracted column models</returns>
    List<ColumnModel> Extract(ColumnCreationContext context) ;
}
