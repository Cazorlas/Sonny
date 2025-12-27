namespace Sonny.Application.Domain.Entities.ColumnFromCad.Services ;

/// <summary>
///     Interface for column creation strategy
/// </summary>
public interface IColumnCreationStrategy
{
    /// <summary>
    ///     Executes the strategy to create a column element
    /// </summary>
    /// <returns>Created column element, or null if creation failed</returns>
    string? Execute() ;
}
