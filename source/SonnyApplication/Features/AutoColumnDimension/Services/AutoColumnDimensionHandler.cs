using Serilog ;
using SonnyApplication.Features.AutoColumnDimension.Interfaces ;
using SonnyApplication.Features.AutoColumnDimension.Models ;
using SonnyApplication.Interfaces ;
using SonnyApplication.Managers ;
using SonnyRevitExtensions.Extensions.Elements ;
using SonnyRevitExtensions.RevitWrapper ;

namespace SonnyApplication.Features.AutoColumnDimension.Services ;

/// <summary>
/// Handler for executing auto column dimension creation process
/// </summary>
public class AutoColumnDimensionHandler : IAutoColumnDimensionHandler
{
    private const string TransactionName = "Auto Column Dimension" ;
    private const int ExpectedDimensionsPerColumn = 2 ;

    private readonly IMessageService _messageService ;
    private readonly ILogger _logger ;
    private readonly IAutoColumnDimensionService _autoColumnDimensionService ;

    /// <summary>
    /// Initializes a new instance of AutoColumnDimensionHandler
    /// </summary>
    /// <param name="messageService">The message service</param>
    /// <param name="logger">The logger</param>
    /// <param name="autoColumnDimensionService">The auto column dimension service</param>
    public AutoColumnDimensionHandler(IMessageService messageService,
        ILogger logger,
        IAutoColumnDimensionService autoColumnDimensionService)
    {
        _messageService = messageService ;
        _logger = logger ;
        _autoColumnDimensionService = autoColumnDimensionService ;
    }

    /// <summary>
    /// Executes the dimension creation process
    /// </summary>
    /// <param name="revitDocument">The Revit document service</param>
    /// <param name="snapDistance">The snap distance for dimensions</param>
    /// <param name="dimensionType">The dimension type to use</param>
    public void Execute(IRevitDocument revitDocument,
        double snapDistance,
        DimensionType? dimensionType)
    {
        _logger.Information("Starting dimension creation process") ;

        if (revitDocument.ActiveView == null)
        {
            _logger.Warning("No active view found") ;
            _messageService.ShowError("No active view found.") ;
            return ;
        }

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

        if (! ValidateColumns(columnWrappers))
            return ;

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

    /// <summary>
    /// Validates that columns exist for dimensioning
    /// </summary>
    /// <param name="wrappers">List of column wrappers</param>
    /// <returns>True if validation passes, false otherwise</returns>
    private bool ValidateColumns(List<ColumnWrapperBase> wrappers)
    {
        if (wrappers.Count == 0)
        {
            _logger.Warning("No valid columns found for dimensioning") ;
            _messageService.ShowInfo("No structural columns found in the active view.") ;
            return false ;
        }

        return true ;
    }

    /// <summary>
    /// Creates dimensions for columns within a transaction
    /// </summary>
    /// <param name="revitDocument"></param>
    /// <param name="columnWrappers">List of column wrappers</param>
    /// <param name="viewWrapper">The view wrapper</param>
    /// <param name="snapDistance">The snap distance</param>
    /// <param name="dimensionType">The dimension type</param>
    /// <returns>List of created dimension element wrappers</returns>
    private List<ElementWrapperBase> CreateDimensions(IRevitDocument revitDocument,
        List<ColumnWrapperBase> columnWrappers,
        ViewWrapperBase viewWrapper,
        double snapDistance,
        DimensionType? dimensionType)
    {
        using var transaction = new TransactionManager(revitDocument.Document,
            TransactionName) ;
        transaction.Start() ;

        var createdDimensions = _autoColumnDimensionService.Execute(columnWrappers,
            viewWrapper,
            snapDistance,
            dimensionType) ;

        transaction.Commit() ;

        return createdDimensions ;
    }

    /// <summary>
    /// Shows the execution result to the user
    /// </summary>
    /// <param name="createdDimensions">List of created dimensions</param>
    /// <param name="columnCount">Number of columns processed</param>
    private void ShowResult(List<ElementWrapperBase> createdDimensions,
        int columnCount)
    {
        var expectedDimensionCount = columnCount * ExpectedDimensionsPerColumn ;
        var result = new ExecutionResult
        {
            SuccessCount = createdDimensions.Count,
            FailureCount = Math.Max(0,
                expectedDimensionCount - createdDimensions.Count)
        } ;

        var logMessage = result.GetLogMessage() ;
        _messageService.ShowInfo(logMessage) ;
    }
}
