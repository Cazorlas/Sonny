using Sonny.Application.Features.ColumnFromCad.Contexts ;
using Sonny.Application.Features.ColumnFromCad.Models ;

namespace Sonny.Application.Features.ColumnFromCad.Interfaces ;

public interface IColumnFromCadOrchestrator
{
    List<ColumnModel> ExtractColumnData(ImportInstance cadInstance,
        string selectedLayer,
        bool isModelByHatch) ;

    List<ElementId> CreateColumns(ColumnCreationContext context) ;
}
