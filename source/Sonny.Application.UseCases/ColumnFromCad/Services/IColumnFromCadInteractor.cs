using Sonny.Application.Domain.Entites.ColumnFromCad.Contexts ;
using Sonny.Application.Domain.Entites.ColumnFromCad.Models ;

namespace Sonny.Application.UseCases.ColumnFromCad.Services ;

public interface IColumnFromCadInteractor
{
    List<ColumnModel> ExtractColumnData(ImportInstance cadInstance,
        string selectedLayer,
        bool isModelByHatch) ;

    List<ElementId> CreateColumns(ColumnCreationContext context) ;
}
