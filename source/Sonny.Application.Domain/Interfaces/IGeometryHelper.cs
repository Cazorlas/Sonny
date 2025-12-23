namespace Sonny.Application.Domain.Interfaces ;

/// <summary>
///     Interface for geometry helper operations
/// </summary>
public interface IGeometryHelper
{
    /// <summary>
    ///     Gets the midpoint of a line
    /// </summary>
    /// <param name="line">Line to get midpoint from</param>
    /// <returns>Midpoint XYZ</returns>
    XYZ GetMidpoint(Line line) ;

    /// <summary>
    ///     Gets the direction vector of a curve
    /// </summary>
    /// <param name="curve">Curve to get direction from</param>
    /// <returns>Direction vector, or null if not available</returns>
    XYZ? GetDirection(Curve curve) ;

    /// <summary>
    ///     Converts a value from internal units to millimeters
    /// </summary>
    /// <param name="value">Value in internal units</param>
    /// <returns>Value in millimeters</returns>
    double ToMillimeters(double value) ;
}

