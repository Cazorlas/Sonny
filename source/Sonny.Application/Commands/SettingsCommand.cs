using Autodesk.Revit.Attributes ;
using Autodesk.Revit.UI ;
using Sonny.Application.Bases ;
using Sonny.Application.Features.Settings.ViewModels ;
using Sonny.Application.Features.Settings.Views ;
using Sonny.Application.Utils ;

namespace Sonny.Application.Commands ;

/// <summary>
///     Command to open Settings dialog
/// </summary>
[Transaction(TransactionMode.Manual)]
public class SettingsCommand : BaseExternalCommand
{
    protected override Result ExecuteInternal(ExternalCommandData commandData,
        ref string message,
        ElementSet elements)
    {
        var commonServices = ServiceUtils.CreateCommonServices() ;
        var viewModel = new SettingsViewModel(commonServices) ;
        var view = new SettingsView(viewModel) ;
        view.Show() ;

        return Result.Succeeded ;
    }
}
