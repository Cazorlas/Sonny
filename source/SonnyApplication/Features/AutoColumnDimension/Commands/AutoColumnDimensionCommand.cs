using Autodesk.Revit.Attributes ;
using Autodesk.Revit.UI ;
using SonnyApplication.Bases ;
using SonnyApplication.Features.AutoColumnDimension.Interfaces ;
using SonnyApplication.Features.AutoColumnDimension.ViewModels ;
using SonnyApplication.Features.AutoColumnDimension.Views ;

namespace SonnyApplication.Features.AutoColumnDimension.Commands ;

[Transaction(TransactionMode.Manual)]
public class AutoColumnDimensionCommand : BaseExternalCommand
{
    protected override Result ExecuteInternal(ExternalCommandData commandData,
        ref string message,
        ElementSet elements)
    {
        var commonServices = CreateCommonServices(commandData) ;
        var handler = GetService<IAutoColumnDimensionHandler>() ;
        var viewModel = new AutoColumnDimensionViewModel(commonServices,
            handler) ;
        var view = new AutoColumnDimensionView(viewModel) ;
        view.Show() ;

        return Result.Succeeded ;
    }
}
