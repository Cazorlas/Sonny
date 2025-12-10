using Autodesk.Revit.Attributes ;
using Autodesk.Revit.UI ;
using Sonny.Application.Core.Bases ;
using Sonny.Application.Features.AutoColumnDimension.Interfaces ;
using Sonny.Application.Features.AutoColumnDimension.ViewModels ;
using Sonny.Application.Features.AutoColumnDimension.Views ;
using Sonny.Application.Utils ;

namespace Sonny.Application.Commands ;

[Transaction(TransactionMode.Manual)]
public class AutoColumnDimensionCommand : BaseExternalCommand
{
    protected override Result ExecuteInternal(ExternalCommandData commandData,
        ref string message,
        ElementSet elements)
    {
        var commonServices = ServiceUtils.CreateCommonServices(commandData.Application.ActiveUIDocument) ;
        var handler = Host.GetService<IAutoColumnDimensionHandler>() ;
        var viewModel = new AutoColumnDimensionViewModel(commonServices,
            handler) ;
        var view = new AutoColumnDimensionView(viewModel) ;
        view.Show() ;

        return Result.Succeeded ;
    }
}
