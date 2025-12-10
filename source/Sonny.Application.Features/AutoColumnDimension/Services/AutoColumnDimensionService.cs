using Serilog ;
using Sonny.Application.Features.AutoColumnDimension.Interfaces ;
using Sonny.RevitExtensions.RevitWrapper ;

namespace Sonny.Application.Features.AutoColumnDimension.Services ;

/// <summary>
///     Service for automatically creating dimensions for columns
/// </summary>
public class AutoColumnDimensionService : IAutoColumnDimensionService
{
    private readonly IDimensionCreator _dimensionCreator ;
    private readonly IGridFinder _gridFinder ;
    private readonly ILogger _logger ;

    /// <summary>
    ///     Initializes a new instance of AutoColumnDimensionService
    /// </summary>
    /// <param name="gridFinder">The grid finder service</param>
    /// <param name="dimensionCreator">The dimension creator service</param>
    /// <param name="logger">The logger</param>
    public AutoColumnDimensionService(IGridFinder gridFinder,
        IDimensionCreator dimensionCreator,
        ILogger logger)
    {
        _gridFinder = gridFinder ;
        _dimensionCreator = dimensionCreator ;
        _logger = logger ;
    }

    /// <summary>
    ///     Creates dimensions for all columns in the view
    /// </summary>
    /// <param name="columnWrappers">List of column wrappers to dimension</param>
    /// <param name="viewWrapper">The view wrapper</param>
    /// <param name="snapDistance">The snap distance for dimensions</param>
    /// <param name="dimensionType">Optional dimension type to use</param>
    /// <returns>List of created dimension element wrappers</returns>
    public List<ElementWrapperBase> Execute(List<ColumnWrapperBase> columnWrappers,
        ViewWrapperBase viewWrapper,
        double snapDistance = 5.0,
        DimensionType? dimensionType = null)
    {
        var allDimensions = new List<ElementWrapperBase>() ;

        foreach (var columnWrapper in columnWrappers)
        {
            try
            {
                var dimensions = CreateDimensions(columnWrapper,
                    viewWrapper,
                    snapDistance,
                    dimensionType) ;

                allDimensions.AddRange(dimensions) ;
            }
            catch (Exception ex)
            {
                // Log error but continue processing other columns
                _logger.Warning(ex,
                    "Failed to create dimensions for column") ;
            }
        }

        return allDimensions ;
    }

    /// <summary>
    ///     Creates dimensions for a single column
    /// </summary>
    /// <param name="columnWrapper">The column wrapper</param>
    /// <param name="viewWrapper">The view wrapper</param>
    /// <param name="snapDistance">The snap distance for dimensions</param>
    /// <param name="dimensionType">Optional dimension type to use</param>
    /// <returns>List of created dimension element wrappers</returns>
    private List<ElementWrapperBase> CreateDimensions(ColumnWrapperBase columnWrapper,
        ViewWrapperBase viewWrapper,
        double snapDistance,
        DimensionType? dimensionType)
    {
        // Use ColumnDimensionContext to calculate dimension parameters
        if (ColumnDimensionContext.Create(columnWrapper,
                viewWrapper,
                _gridFinder) is not { } context)
        {
            return [] ;
        }

        var allDimensions = new List<ElementWrapperBase>() ;

        // Create first dimension
        var firstDimensions = _dimensionCreator.DimensionByDirection(context.PlanarFaces,
            context.FirstDirection,
            context.SecondDirection,
            context.FirstGridWrapper,
            context.MaxPoint,
            snapDistance,
            viewWrapper,
            dimensionType) ;

        allDimensions.AddRange(firstDimensions) ;

        // Create second dimension
        var secondDimensions = _dimensionCreator.DimensionByDirection(context.PlanarFaces,
            context.SecondDirection,
            context.FirstDirection,
            context.SecondGridWrapper,
            context.MaxPoint,
            snapDistance,
            viewWrapper,
            dimensionType) ;

        allDimensions.AddRange(secondDimensions) ;

        return allDimensions ;
    }
}
