using Autodesk.Revit.Attributes ;
using Autodesk.Revit.UI ;
using Sonny.Application.Bases ;
using Sonny.Application.Presentation.Settings.Views ;

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
        var view = Host.GetService<SettingsView>() ;
        view.Show() ;

        return Result.Succeeded ;
    }
}
