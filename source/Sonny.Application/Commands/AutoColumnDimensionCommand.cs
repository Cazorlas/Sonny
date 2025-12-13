using Autodesk.Revit.Attributes ;
using Autodesk.Revit.UI ;
using Sonny.Application.Bases ;
using Sonny.Application.Features.AutoColumnDimension.Views ;

namespace Sonny.Application.Commands ;

[Transaction(TransactionMode.Manual)]
public class AutoColumnDimensionCommand : BaseExternalCommand
{
    protected override Result ExecuteInternal(ExternalCommandData commandData,
        ref string message,
        ElementSet elements)
    {
        var view = Host.GetService<AutoColumnDimensionView>() ;
        view.Show() ;

        return Result.Succeeded ;
    }
}
