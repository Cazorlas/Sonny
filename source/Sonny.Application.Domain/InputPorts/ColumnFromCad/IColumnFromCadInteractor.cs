using Sonny.Application.Entities.ColumnFromCad ;
using Sonny.Application.Entities.ColumnFromCad.Contexts ;

namespace Sonny.Application.Domain.InputPorts.ColumnFromCad ;

public interface IColumnFromCadInteractor
{
    List<ColumnModel> ExtractColumnData(ImportInstance cadInstance,
        string selectedLayer,
        bool isModelByHatch) ;

    List<ElementId> CreateColumns(ColumnCreationContext context) ;
}
