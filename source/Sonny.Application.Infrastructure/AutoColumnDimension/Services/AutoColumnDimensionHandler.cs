using Serilog ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.UseCases.AutoColumnDimension.Interfaces ;
using Sonny.RevitExtensions.Extensions.Elements ;
using Sonny.RevitExtensions.RevitWrapper ;

namespace Sonny.Application.UseCases.AutoColumnDimension.Services ;

/// <summary>
///     Handler for executing auto column dimension creation process
/// </summary>
public class AutoColumnDimensionHandler : IAutoColumnDimensionHandler
{
    private const string TransactionName = "Auto Column Dimension" ;
    private const int ExpectedDimensionsPerColumn = 2 ;
    private readonly IAutoColumnDimensionService _autoColumnDimensionService ;
    private readonly ILogger _logger ;
    private readonly IMessageService _messageService ;
    private readonly IResourceHelper _resourceHelper ;
    private readonly ITransactionManagerFactory _transactionManagerFactory ;

    public AutoColumnDimensionHandler(IMessageService messageService,
        ILogger logger,
        IAutoColumnDimensionService autoColumnDimensionService,
        IResourceHelper resourceHelper,
        ITransactionManagerFactory transactionManagerFactory)
    {
        _messageService = messageService ;
        _logger = logger ;
        _autoColumnDimensionService = autoColumnDimensionService ;
        _resourceHelper = resourceHelper ;
        _transactionManagerFactory = transactionManagerFactory ;
    }

    public void Execute(IRevitDocument revitDocument,
        double snapDistance,
        DimensionType? dimensionType)
    {
        _logger.Information("Starting dimension creation process") ;

        _logger.Debug("Processing view: {ViewName}",
            revitDocument.ActiveView.Name) ;

        var viewWrapper = new ViewWrapperBase(revitDocument.ActiveView) ;
        var familyInstanceWrappers = viewWrapper.FamilyInstanceWrappers.Where(x =>
            x.FamilyInstance.IsBuiltInCategory(BuiltInCategory.OST_StructuralColumns)) ;

        var columnWrappers = familyInstanceWrappers.Select(x => new ColumnWrapperBase(x.FamilyInstance))
            .Where(x => x.GetCenterPoint(viewWrapper) != null)
            .ToList() ;

        _logger.Information("Found {Count} column wrappers",
            columnWrappers.Count) ;

        if (! ValidateColumns(columnWrappers)) {
            return ;
        }

        var createdDimensions = CreateDimensions(revitDocument,
            columnWrappers,
            viewWrapper,
            snapDistance,
            dimensionType) ;

        _logger.Information("Created {Count} dimensions successfully",
            createdDimensions.Count) ;

        ShowResult(createdDimensions,
            columnWrappers.Count) ;
    }

    private bool ValidateColumns(List<ColumnWrapperBase> wrappers)
    {
        if (wrappers.Count == 0) {
            _logger.Warning("No valid columns found for dimensioning") ;
            _messageService.ShowInfo(_resourceHelper.GetString("MessageNoColumnsFound")) ;
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
        using var transaction = _transactionManagerFactory.Create(revitDocument,
            TransactionName) ;
        transaction.Start() ;

        var createdDimensions = _autoColumnDimensionService.Execute(columnWrappers,
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
        var logMessage = _resourceHelper.GetString("ExecutionResultCompleted",
                             completedTime)
                         + "\n\n"
                         + _resourceHelper.GetString("ExecutionResultTotalColumns",
                             total)
                         + "\n"
                         + _resourceHelper.GetString("ExecutionResultSuccess",
                             successCount)
                         + "\n"
                         + _resourceHelper.GetString("ExecutionResultFailed",
                             failureCount) ;

        _messageService.ShowInfo(logMessage) ;
    }
}
