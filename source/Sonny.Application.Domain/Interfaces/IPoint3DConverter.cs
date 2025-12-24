using Sonny.Application.Domain.Entites ;

namespace Sonny.Application.Domain.Interfaces;

/// <summary>
///     Converts between Point3D and Revit API XYZ
/// </summary>
public interface IPoint3DConverter
{
    /// <summary>
    ///     Converts Point3D to Revit API XYZ
    /// </summary>
    /// <param name="point">The Point3D to convert</param>
    /// <returns>The equivalent XYZ</returns>
    XYZ ToXyz(Point3D point);

    /// <summary>
    ///     Converts Revit API XYZ to Point3D
    /// </summary>
    /// <param name="xyz">The XYZ to convert</param>
    /// <returns>The equivalent Point3D</returns>
    Point3D FromXyz(XYZ xyz);
}


