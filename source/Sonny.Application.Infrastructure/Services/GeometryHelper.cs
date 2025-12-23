using Sonny.Application.Domain.Interfaces ;
using Sonny.RevitExtensions.Extensions.GeometryObjects.Curves ;
using Sonny.RevitExtensions.Extensions.GeometryObjects.Curves.Lines ;

namespace Sonny.Application.Infrastructure.Services ;

public class GeometryHelper : IGeometryHelper
{
    public XYZ GetMidpoint(Line line)
    {
        return line.GetMidpoint() ;
    }

    public XYZ? GetDirection(Curve curve)
    {
        return curve.Direction() ;
    }

    public double ToMillimeters(double value)
    {
        return value.ToMillimeters() ;
    }
}

