using Sonny.Application.Domain.Entities ;
using Sonny.Application.Infrastructure.Revit.Services ;

namespace Sonny.Application.Infrastructure.Revit.Implements ;

public class Point3DConverter : IPoint3DConverter
{
    public XYZ ToXyz(Point3D point) =>
        new(point.X,
            point.Y,
            point.Z) ;

    public Point3D FromXyz(XYZ xyz) =>
        new(xyz.X,
            xyz.Y,
            xyz.Z) ;
}
