using Sonny.Application.Features.ColumnFromCad.Interfaces ;
using Sonny.Application.Features.ColumnFromCad.Models ;
using Sonny.RevitExtensions.Extensions ;
using Sonny.RevitExtensions.Extensions.CurveLoops ;
using Sonny.RevitExtensions.Extensions.Elements ;
using Sonny.RevitExtensions.Extensions.GeometryObjects ;
using Sonny.RevitExtensions.Extensions.GeometryObjects.Curves ;
using Sonny.RevitExtensions.Extensions.GeometryObjects.Solids ;

namespace Sonny.Application.Features.ColumnFromCad.Services ;

public class CircularColumnExtractor : ICircularColumnExtractor
{
    private const double Tolerance = 1e-4 ;

    public List<CircularColumnModel> ExtractFromBoundaryLines(ImportInstance cadInstance,
        string selectedLayer)
    {
        var columns = new List<CircularColumnModel>() ;

        var arcs = cadInstance.GetArcs()
            .Where(x => x.IsOnLayer(selectedLayer,
                cadInstance.Document))
            .ToList() ;

        foreach (var arc in arcs)
        {
            columns.Add(new CircularColumnModel(arc)) ;
        }

        return columns ;
    }

    public List<CircularColumnModel> ExtractFromPlanarFaces(ImportInstance cadInstance,
        string selectedLayer)
    {
        var columns = new List<CircularColumnModel>() ;
        var planarFaces = cadInstance.GetSolids()
            .GetPlanarFaces()
            .Where(x => x.IsOnLayer(selectedLayer,
                cadInstance.Document)) ;

        foreach (var planarFace in planarFaces)
        {
            var curveLoops = planarFace.GetEdgesAsCurveLoops() ;
            if (curveLoops.Count == 0)
            {
                continue ;
            }

            var curveLoop = curveLoops.First() ;
            var curves = curveLoop.GetCurves()
                .ToList() ;

            // Skip rectangular (4 curves)
            if (curves.Count == 4)
            {
                continue ;
            }

            // Try to detect circular column
            var points = curves.GetXYZPoints()
                .ToList() ;
            if (points.Count >= 3)
            {
                var arc = Arc.Create(points[1],
                    points[3],
                    points[2]) ;
                var arcCenter = arc.Center ;

                var first = points.First() ;
                var distanceTo = first.DistanceTo(arcCenter) ;

                var allPointsSameDistance = points.All(x =>
                {
                    var abs = Math.Abs(x.DistanceTo(arcCenter) - distanceTo) ;
                    return abs < Tolerance ;
                }) ;

                if (allPointsSameDistance)
                {
                    columns.Add(new CircularColumnModel(arc)) ;
                }
            }
        }

        return columns ;
    }
}
