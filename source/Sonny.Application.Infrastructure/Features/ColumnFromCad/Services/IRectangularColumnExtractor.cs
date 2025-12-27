using Sonny.Application.Domain.Entities.ColumnFromCad.Models ;

namespace Sonny.Application.Infrastructure.Features.ColumnFromCad.Services ;

public interface IRectangularColumnExtractor
{
    List<RectangularColumnModel> ExtractFromBoundaryLines(ImportInstance cadInstance,
        string selectedLayer) ;

    List<RectangularColumnModel> ExtractFromPlanarFaces(ImportInstance cadInstance,
        string selectedLayer) ;
}
