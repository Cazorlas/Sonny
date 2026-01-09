using Autodesk.Revit.Attributes ;
using Autodesk.Revit.UI ;
using Sonny.Application.Bases ;
using Sonny.Application.Domain.Services ;

namespace Sonny.Application.Commands ;

[Transaction(TransactionMode.Manual)]
public class LoginCommand : BaseExternalCommand
{
    protected override bool ShouldCheckLicense() => false ;

    protected override Result ExecuteInternal(ExternalCommandData commandData,
        ref string message,
        ElementSet elements)
    {
        var licenseValidator = Host.GetService<ILicenseValidator>() ;
        licenseValidator.ShowLicenseWindow() ;

        return Result.Succeeded ;
    }
}
