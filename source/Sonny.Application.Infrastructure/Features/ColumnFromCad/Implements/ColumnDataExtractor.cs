using Sonny.Application.Domain.Entities.ColumnFromCad.Contexts ;
using Sonny.Application.Domain.Entities.ColumnFromCad.Models ;
using Sonny.Application.Domain.Entities.ColumnFromCad.Services ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.Infrastructure.Features.ColumnFromCad.Services ;
using Sonny.Application.Infrastructure.Revit.Services ;
using Sonny.RevitExtensions.Extensions ;

namespace Sonny.Application.Infrastructure.Features.ColumnFromCad.Implements ;

public class ColumnDataExtractor(
    IRevitDocument revitDocument,
    IResourceHelper resourceHelper,
    IRectangularColumnExtractor rectangularExtractor,
    ICircularColumnExtractor circularExtractor) : IColumnDataExtractor
{
    public List<ColumnModel> Extract(ColumnCreationContext context)
    {
        if (string.IsNullOrEmpty(context.Settings.SelectedCadLinkId)) {
            throw new InvalidOperationException(resourceHelper.GetString("MessageFailedToSelectCadLink")) ;
        }

        var cadInstance = revitDocument.Document.GetElementById<ImportInstance>(context.Settings.SelectedCadLinkId!) ;
        if (cadInstance == null) {
            throw new InvalidOperationException(resourceHelper.GetString("MessageFailedToSelectCadLink")) ;
        }

        var selectedLayer = context.Settings.SelectedLayer ?? string.Empty ;
        var isModelByHatch = context.Settings.IsModelByHatch ;

        var extractedColumns = new List<ColumnModel>() ;

        if (isModelByHatch) {
            // Extract from planar faces (hatch)
            extractedColumns.AddRange(rectangularExtractor.ExtractFromPlanarFaces(cadInstance,
                selectedLayer)) ;
            extractedColumns.AddRange(circularExtractor.ExtractFromPlanarFaces(cadInstance,
                selectedLayer)) ;
        }
        else {
            // Extract from boundary lines (poly lines and arcs)
            extractedColumns.AddRange(rectangularExtractor.ExtractFromBoundaryLines(cadInstance,
                selectedLayer)) ;
            extractedColumns.AddRange(circularExtractor.ExtractFromBoundaryLines(cadInstance,
                selectedLayer)) ;
        }

        return extractedColumns ;
    }
}
