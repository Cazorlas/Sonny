using Sonny.Application.Domain.Entites ;
using Sonny.Application.Domain.Interfaces;

namespace Sonny.Application.Infrastructure.Services;

public class Point3DConverter : IPoint3DConverter
{
    public XYZ ToXyz(Point3D point) => new XYZ(point.X, point.Y, point.Z);

    public Point3D FromXyz(XYZ xyz) => new Point3D(xyz.X, xyz.Y, xyz.Z);
}


