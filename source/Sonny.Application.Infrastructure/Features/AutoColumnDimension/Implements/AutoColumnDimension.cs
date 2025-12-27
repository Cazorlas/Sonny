using Serilog ;
using Sonny.Application.Infrastructure.Features.AutoColumnDimension.Contexts ;
using Sonny.Application.Infrastructure.Features.AutoColumnDimension.Services ;
using Sonny.RevitExtensions.RevitWrapper ;

namespace Sonny.Application.Infrastructure.Features.AutoColumnDimension.Implements ;

public class AutoColumnDimension(IGridFinder gridFinder, IDimensionCreator dimensionCreator, ILogger logger)
    : IAutoColumnDimension
{
    public List<ElementWrapperBase> Execute(List<ColumnWrapperBase> columnWrappers,
        ViewWrapperBase viewWrapper,
        double snapDistance = 5.0,
        DimensionType? dimensionType = null)
    {
        var allDimensions = new List<ElementWrapperBase>() ;

        foreach (var columnWrapper in columnWrappers) {
            try {
                var dimensions = CreateDimensions(columnWrapper,
                    viewWrapper,
                    snapDistance,
                    dimensionType) ;

                allDimensions.AddRange(dimensions) ;
            }
            catch (Exception ex) {
                // Log error but continue processing other columns
                logger.Warning(ex,
                    "Failed to create dimensions for column") ;
            }
        }

        return allDimensions ;
    }

    private List<ElementWrapperBase> CreateDimensions(ColumnWrapperBase columnWrapper,
        ViewWrapperBase viewWrapper,
        double snapDistance,
        DimensionType? dimensionType)
    {
        // Use ColumnDimensionContext to calculate dimension parameters
        if (ColumnDimensionContext.Create(columnWrapper,
                viewWrapper,
                gridFinder) is not { } context) {
            return [] ;
        }

        var allDimensions = new List<ElementWrapperBase>() ;

        // Create first dimension
        var firstDimensions = dimensionCreator.DimensionByDirection(context.PlanarFaces,
            context.FirstDirection,
            context.SecondDirection,
            context.FirstGridWrapper,
            context.MaxPoint,
            snapDistance,
            viewWrapper,
            dimensionType) ;

        allDimensions.AddRange(firstDimensions) ;

        // Create second dimension
        var secondDimensions = dimensionCreator.DimensionByDirection(context.PlanarFaces,
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
