using Sonny.Application.Domain.Entities.ColumnFromCad.Models ;

namespace Sonny.Application.Infrastructure.Features.ColumnFromCad.Services ;

/// <summary>
///     Factory for creating column models
/// </summary>
public interface IColumnModelFactory
{
    /// <summary>
    ///     Creates a rectangular column model from curves
    /// </summary>
    /// <param name="curves">List of curves forming a rectangle</param>
    /// <returns>Rectangular column model</returns>
    RectangularColumnModel CreateRectangular(List<Curve> curves) ;

    /// <summary>
    ///     Creates a circular column model from arc
    /// </summary>
    /// <param name="arc">Arc representing the circle</param>
    /// <returns>Circular column model</returns>
    CircularColumnModel CreateCircular(Arc arc) ;
}
