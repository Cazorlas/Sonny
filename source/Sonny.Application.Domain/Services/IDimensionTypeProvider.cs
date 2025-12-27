using Sonny.Application.Domain.Entities.Settings.Models ;

namespace Sonny.Application.Domain.Services ;

/// <summary>
///     Provides dimension types from document
/// </summary>
public interface IDimensionTypeProvider
{
    /// <summary>
    ///     Gets all available dimension types
    /// </summary>
    /// <returns>List of dimension type models</returns>
    List<DimensionTypeModel> GetDimensionTypes() ;
}
