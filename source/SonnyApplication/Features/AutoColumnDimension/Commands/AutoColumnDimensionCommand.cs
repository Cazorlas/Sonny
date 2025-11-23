using Autodesk.Revit.Attributes ;
using Autodesk.Revit.UI ;
using Serilog ;
using SonnyApplication.Bases ;
using SonnyApplication.Features.AutoColumnDimension.Interfaces ;
using SonnyApplication.Features.AutoColumnDimension.ViewModels ;
using SonnyApplication.Features.AutoColumnDimension.Views ;
using SonnyApplication.Interfaces ;
using SonnyApplication.Services ;

namespace SonnyApplication.Features.AutoColumnDimension.Commands ;

[Transaction(TransactionMode.Manual)]
public class AutoColumnDimensionCommand : BaseExternalCommand
{
    protected override Result ExecuteInternal(ExternalCommandData commandData,
        ref string message,
        ElementSet elements)
    {
        var revitDocumentService = new RevitDocumentService(commandData.Application.ActiveUIDocument) ;
        var messageService = GetService<IMessageService>() ;
        var logger = GetService<ILogger>() ;
        var handler = GetService<IAutoColumnDimensionHandler>() ;
        var viewModel = new AutoColumnDimensionViewModel(revitDocumentService,
            messageService,
            logger,
            handler) ;
        var view = new AutoColumnDimensionView(viewModel) ;
        view.Show() ;

        return Result.Succeeded ;
    }
}
