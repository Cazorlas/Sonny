using Serilog ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.UseCases.AutoColumnDimension.Services ;
using Sonny.RevitExtensions.Extensions.Elements ;
using Sonny.RevitExtensions.RevitWrapper ;

namespace Sonny.Application.Infrastructure.AutoColumnDimension.Services ;

/// <summary>
///     Interactor for executing auto column dimension creation process
/// </summary>
public class AutoColumnDimensionInteractor(
    IMessageService messageService,
    ILogger logger,
    IAutoColumnDimension autoColumnDimension,
    IResourceHelper resourceHelper,
    ITransactionManagerFactory transactionManagerFactory) : IAutoColumnDimensionInteractor
{
    private const string TransactionName = "Auto Column Dimension" ;
    private const int ExpectedDimensionsPerColumn = 2 ;

    public void Execute(IRevitDocument revitDocument,
        double snapDistance,
        DimensionType? dimensionType)
    {
        logger.Information("Starting dimension creation process") ;

        logger.Debug("Processing view: {ViewName}",
            revitDocument.ActiveView.Name) ;

        var viewWrapper = new ViewWrapperBase(revitDocument.ActiveView) ;
        var familyInstanceWrappers = viewWrapper.FamilyInstanceWrappers.Where(x =>
            x.FamilyInstance.IsBuiltInCategory(BuiltInCategory.OST_StructuralColumns)) ;

        var columnWrappers = familyInstanceWrappers.Select(x => new ColumnWrapperBase(x.FamilyInstance))
            .Where(x => x.GetCenterPoint(viewWrapper) != null)
            .ToList() ;

        logger.Information("Found {Count} column wrappers",
            columnWrappers.Count) ;

        if (! ValidateColumns(columnWrappers)) {
            return ;
        }

        var createdDimensions = CreateDimensions(revitDocument,
            columnWrappers,
            viewWrapper,
            snapDistance,
            dimensionType) ;

        logger.Information("Created {Count} dimensions successfully",
            createdDimensions.Count) ;

        ShowResult(createdDimensions,
            columnWrappers.Count) ;
    }

    private bool ValidateColumns(List<ColumnWrapperBase> wrappers)
    {
        if (wrappers.Count == 0) {
            logger.Warning("No valid columns found for dimensioning") ;
            messageService.ShowInfo(resourceHelper.GetString("MessageNoColumnsFound")) ;
            return false ;
        }

        return true ;
    }

    private List<ElementWrapperBase> CreateDimensions(IRevitDocument revitDocument,
        List<ColumnWrapperBase> columnWrappers,
        ViewWrapperBase viewWrapper,
        double snapDistance,
        DimensionType? dimensionType)
    {
        using var transaction = transactionManagerFactory.Create(revitDocument,
            TransactionName) ;
        transaction.Start() ;

        var createdDimensions = autoColumnDimension.Execute(columnWrappers,
            viewWrapper,
            snapDistance,
            dimensionType) ;

        transaction.Commit() ;

        return createdDimensions ;
    }

    private void ShowResult(List<ElementWrapperBase> createdDimensions,
        int columnCount)
    {
        var expectedDimensionCount = columnCount * ExpectedDimensionsPerColumn ;
        var successCount = createdDimensions.Count ;
        var failureCount = Math.Max(0,
            expectedDimensionCount - successCount) ;

        var total = successCount + failureCount ;
        var completedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") ;
        var logMessage = resourceHelper.GetString("ExecutionResultCompleted",
                             completedTime)
                         + "\n\n"
                         + resourceHelper.GetString("ExecutionResultTotalColumns",
                             total)
                         + "\n"
                         + resourceHelper.GetString("ExecutionResultSuccess",
                             successCount)
                         + "\n"
                         + resourceHelper.GetString("ExecutionResultFailed",
                             failureCount) ;

        messageService.ShowInfo(logMessage) ;
    }
}
