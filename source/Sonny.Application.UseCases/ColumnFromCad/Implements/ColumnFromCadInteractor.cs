using Sonny.Application.Domain.Entities.ColumnFromCad.Contexts ;
using Sonny.Application.Domain.Entities.ColumnFromCad.Models ;
using Sonny.Application.Domain.Entities.ColumnFromCad.Services ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.UseCases.ColumnFromCad.Services ;

namespace Sonny.Application.UseCases.ColumnFromCad.Implements ;

public class ColumnFromCadInteractor(
    IColumnDataExtractor columnDataExtractor,
    IResourceHelper resourceHelper,
    ITransactionManagerFactory transactionManagerFactory,
    IColumnCreationStrategyFactory columnCreationStrategyFactory,
    IProgressReporter progressReporter,
    IMessageService messageService,
    IElementSelector elementSelector,
    IRevitTaskRunner revitTaskRunner) : IColumnFromCadInteractor
{
    private readonly List<ColumnModel> _extractedColumns = [] ;

    public async Task Execute(ColumnCreationContext input)
    {
        // Extract column data
        _extractedColumns.Clear() ;
        var columnModels = await revitTaskRunner.RunAsync(() => ExtractColumnData(input)) ;

        if (columnModels.Count == 0) {
            messageService.ShowInfo(resourceHelper.GetString("MessageNoColumnsFound")) ;
            return ;
        }

        // Create columns
        var createdIds = await revitTaskRunner.RunAsync(() => CreateColumns(input)) ;

        // Show result and select columns
        if (createdIds.Count > 0) {
            // Select created columns to highlight them in Revit UI
            await revitTaskRunner.RunAsync(() => elementSelector.SelectElements(createdIds)) ;

            messageService.ShowInfo(resourceHelper.GetString("MessageSuccessfullyCreated",
                createdIds.Count)) ;
        }
        else {
            messageService.ShowWarning(resourceHelper.GetString("MessageNoColumnsCreated")) ;
        }
    }

    public List<ColumnModel> ExtractColumnData(ColumnCreationContext input)
    {
        var columnModels = columnDataExtractor.Extract(input) ;
        _extractedColumns.AddRange(columnModels) ;

        return columnModels ;
    }

    public HashSet<string> CreateColumns(ColumnCreationContext columnCreationContext)
    {
        if (_extractedColumns.Count == 0) {
            throw new InvalidOperationException(resourceHelper.GetString("MessageNoExtractedColumnsFound")) ;
        }

        var total = _extractedColumns.Count ;

        // Show progress window
        progressReporter.Show(resourceHelper.GetString("MessageCreatingColumns")) ;

        try {
            var createdIds = new HashSet<string>() ;
            var current = 0 ;

            using var transactionGroup = transactionManagerFactory.CreateGroup(
                resourceHelper.GetString("TransactionCreateColumns")) ;
            transactionGroup.Start() ;

            foreach (var columnModel in _extractedColumns) {
                current++ ;
                progressReporter.Update(current,
                    total) ;

                try {
                    using var transactionManager = transactionManagerFactory.Create(
                        resourceHelper.GetString("TransactionCreateColumn"),
                        [FailurePreprocessorType.SuppressWarnings]) ;
                    transactionManager.Start() ;

                    if (columnCreationStrategyFactory.CreateStrategy(columnModel,
                            columnCreationContext) is not { } columnCreationStrategy) {
                        continue ;
                    }

                    if (columnCreationStrategy.Execute() is not { } columnUniqueId) {
                        continue ;
                    }

                    createdIds.Add(columnUniqueId) ;

                    transactionManager.Commit() ; // Commit now → show on UI
                }
                catch {
                    // Continue with next column if one fails
                }
            }

            transactionGroup.Assimilate() ;

            return createdIds ;
        }
        finally {
            // Close progress window
            progressReporter.Close() ;
        }
    }
}
