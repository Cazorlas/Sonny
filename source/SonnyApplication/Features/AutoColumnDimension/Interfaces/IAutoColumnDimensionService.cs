using Autodesk.Revit.DB ;
using SonnyRevitExtensions.RevitWrapper ;

namespace SonnyApplication.Features.AutoColumnDimension.Interfaces ;

/// <summary>
/// Interface for automatically creating dimensions for columns
/// </summary>
public interface IAutoColumnDimensionService
{
    /// <summary>
    /// Creates dimensions for all columns in the view
    /// </summary>
    /// <param name="columnWrappers">List of column wrappers to dimension</param>
    /// <param name="viewWrapper">The view wrapper</param>
    /// <param name="snapDistance">The snap distance for dimensions</param>
    /// <param name="dimensionType">Optional dimension type to use</param>
    /// <returns>List of created dimension element wrappers</returns>
    List<ElementWrapperBase> Execute(List<ColumnWrapperBase> columnWrappers,
        ViewWrapperBase viewWrapper,
        double snapDistance = 5.0,
        DimensionType? dimensionType = null) ;
}

