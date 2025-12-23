using Sonny.Application.UseCases.ColumnFromCad.Contexts ;
using Sonny.Application.UseCases.ColumnFromCad.Models ;

namespace Sonny.Application.UseCases.ColumnFromCad.Interfaces ;

public interface IColumnFromCadOrchestrator
{
    List<ColumnModel> ExtractColumnData(ImportInstance cadInstance,
        string selectedLayer,
        bool isModelByHatch) ;

    List<ElementId> CreateColumns(ColumnCreationContext context) ;
}
