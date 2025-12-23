using Sonny.Application.UseCases.FailuresPreprocessors ;
using Sonny.Application.UseCases.Interfaces ;
using Sonny.Application.UseCases.Managers ;
using Sonny.Application.UseCases.Preprocessors ;
using Sonny.Application.UseCases.ColumnFromCad.Contexts ;
using Sonny.Application.UseCases.ColumnFromCad.Interfaces ;
using Sonny.Application.UseCases.ColumnFromCad.Models ;
using Sonny.Application.UseCases.ColumnFromCad.Strategies ;

namespace Sonny.Application.UseCases.ColumnFromCad.Services ;

public class ColumnFromCadOrchestrator(
    IRectangularColumnExtractor rectangularExtractor,
    ICircularColumnExtractor circularExtractor,
    IResourceHelper resourceHelper) : IColumnFromCadOrchestrator
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
            throw new InvalidOperationException(resourceHelper.GetString("MessageNoExtractedColumnsFound")) ;
        }

        var createdIds = new List<ElementId>() ;
        var total = _extractedColumns.Count ;
        var current = 0 ;

        using var transactionGroup = new TransactionGroupManager(columnCreationContext.Document,
            resourceHelper.GetString("TransactionCreateColumns")) ;
        transactionGroup.Start() ;

        foreach (var columnModel in _extractedColumns) {
            current++ ;
            columnCreationContext.ProgressCallback?.Invoke(current,
                total) ;

            try {
                var compositeFailurePreprocessor = new CompositeFailurePreprocessor() ;
                compositeFailurePreprocessor.AddPreprocessor(new SuppressWarningsPreprocessor()) ;
                using var transactionManager = new TransactionManager(columnCreationContext.Document,
                    resourceHelper.GetString("TransactionCreateColumn"),
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
