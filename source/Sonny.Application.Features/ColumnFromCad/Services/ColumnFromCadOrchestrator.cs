using Sonny.Application.Core.FailuresPreprocessors ;
using Sonny.Application.Core.Managers ;
using Sonny.Application.Core.Preprocessors ;
using Sonny.Application.Features.ColumnFromCad.Contexts ;
using Sonny.Application.Features.ColumnFromCad.Interfaces ;
using Sonny.Application.Features.ColumnFromCad.Models ;
using Sonny.Application.Features.ColumnFromCad.Strategies ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Features.ColumnFromCad.Services ;

public class ColumnFromCadOrchestrator(
    IRectangularColumnExtractor rectangularExtractor,
    ICircularColumnExtractor circularExtractor) : IColumnFromCadOrchestrator
{
    private readonly List<ColumnModel> _extractedColumns = [] ;

    public List<ColumnModel> ExtractColumnData(ImportInstance cadInstance,
        string selectedLayer,
        bool isModelByHatch)
    {
        if (isModelByHatch) {
            // Extract from planar faces (hatch)
            _extractedColumns.AddRange(rectangularExtractor.ExtractFromPlanarFaces(cadInstance,
                selectedLayer)) ;
            _extractedColumns.AddRange(circularExtractor.ExtractFromPlanarFaces(cadInstance,
                selectedLayer)) ;
        }
        else {
            // Extract from boundary lines (poly lines and arcs)
            _extractedColumns.AddRange(rectangularExtractor.ExtractFromBoundaryLines(cadInstance,
                selectedLayer)) ;
            _extractedColumns.AddRange(circularExtractor.ExtractFromBoundaryLines(cadInstance,
                selectedLayer)) ;
        }

        return _extractedColumns ;
    }

    public List<ElementId> CreateColumns(ColumnCreationContext columnCreationContext)
    {
        if (_extractedColumns.Count == 0) {
            throw new InvalidOperationException(ResourceHelper.GetString("MessageNoExtractedColumnsFound")) ;
        }

        var createdIds = new List<ElementId>() ;
        var total = _extractedColumns.Count ;
        var current = 0 ;

        using var transactionGroup = new TransactionGroupManager(columnCreationContext.Document,
            ResourceHelper.GetString("TransactionCreateColumns")) ;
        transactionGroup.Start() ;

        foreach (var columnModel in _extractedColumns) {
            current++ ;
            columnCreationContext.ProgressCallback?.Invoke(current,
                total) ;

            try {
                var compositeFailurePreprocessor = new CompositeFailurePreprocessor() ;
                compositeFailurePreprocessor.AddPreprocessor(new SuppressWarningsPreprocessor()) ;
                using var transactionManager = new TransactionManager(columnCreationContext.Document,
                    ResourceHelper.GetString("TransactionCreateColumn"),
                    compositeFailurePreprocessor) ;
                transactionManager.Start() ;

                if (ColumnCreationStrategy.CreateInstance(columnModel,
                        columnCreationContext) is not { } columnCreationStrategy) {
                    continue ;
                }

                if (columnCreationStrategy.Execute() is not { } column) {
                    continue ;
                }

                createdIds.Add(column.Id) ;

                transactionManager.Commit() ; // Commit now → show on UI
            }
            catch {
                // Continue with next column if one fails
            }
        }

        transactionGroup.Assimilate() ;

        return createdIds ;
    }
}
