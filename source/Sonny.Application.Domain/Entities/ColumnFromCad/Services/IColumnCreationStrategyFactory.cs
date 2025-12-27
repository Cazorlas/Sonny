using Sonny.Application.Domain.Entities.ColumnFromCad.Contexts ;
using Sonny.Application.Domain.Entities.ColumnFromCad.Models ;

namespace Sonny.Application.Domain.Entities.ColumnFromCad.Services ;

/// <summary>
///     Factory for creating column creation strategies
/// </summary>
public interface IColumnCreationStrategyFactory
{
    /// <summary>
    ///     Creates an appropriate column creation strategy based on the column model type
    /// </summary>
    /// <param name="columnModel">Column model to create strategy for</param>
    /// <param name="columnCreationContext">Context for column creation</param>
    /// <returns>Column creation strategy, or null if no suitable strategy found</returns>
    IColumnCreationStrategy? CreateStrategy(ColumnModel columnModel,
        ColumnCreationContext columnCreationContext) ;
}
