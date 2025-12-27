namespace Sonny.Application.UseCases.AutoColumnDimension.Services ;

public interface IAutoColumnDimensionInteractor
{
    void Execute(double snapDistance,
        string? dimensionTypeUniqueId = null) ;
}
