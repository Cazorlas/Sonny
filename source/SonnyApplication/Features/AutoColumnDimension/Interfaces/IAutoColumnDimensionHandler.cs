using SonnyApplication.Interfaces ;

namespace SonnyApplication.Features.AutoColumnDimension.Interfaces ;

/// <summary>
/// Interface for handling auto column dimension creation process
/// </summary>
public interface IAutoColumnDimensionHandler
{
    /// <summary>
    /// Executes the dimension creation process
    /// </summary>
    /// <param name="revitDocument">The Revit document service</param>
    /// <param name="snapDistance">The snap distance for dimensions</param>
    /// <param name="dimensionType">The dimension type to use</param>
    void Execute(IRevitDocument revitDocument,
        double snapDistance,
        DimensionType? dimensionType) ;
}

