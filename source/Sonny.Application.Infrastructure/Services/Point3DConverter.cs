using Sonny.Application.Domain.Interfaces;
using Sonny.Application.Entities;

namespace Sonny.Application.Infrastructure.Services;

public class Point3DConverter : IPoint3DConverter
{
    public XYZ ToXyz(Point3D point) => new XYZ(point.X, point.Y, point.Z);

    public Point3D FromXyz(XYZ xyz) => new Point3D(xyz.X, xyz.Y, xyz.Z);
}

