using Sonny.Application.Domain.Entities.ColumnFromCad.Contexts ;
using Sonny.Application.Domain.Entities.ColumnFromCad.Models ;

namespace Sonny.Application.UseCases.ColumnFromCad.Services ;

public interface IColumnFromCadInteractor
{
    Task Execute(ColumnCreationContext input) ;
    List<ColumnModel> ExtractColumnData(ColumnCreationContext input) ;
    HashSet<string> CreateColumns(ColumnCreationContext context) ;
}
