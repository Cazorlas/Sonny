namespace Sonny.Application.Entities ;

/// <summary>
///     Represents a 3D point in space
/// </summary>
public class Point3D(double x, double y, double z)
{
    /// <summary>
    ///     Gets the X coordinate
    /// </summary>
    public double X { get ; } = x ;

    /// <summary>
    ///     Gets the Y coordinate
    /// </summary>
    public double Y { get ; } = y ;

    /// <summary>
    ///     Gets the Z coordinate
    /// </summary>
    public double Z { get ; } = z ;
}
