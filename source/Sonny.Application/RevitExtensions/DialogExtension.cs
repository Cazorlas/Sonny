using System.Windows ;
using System.Windows.Interop ;
using Autodesk.Windows ;

namespace Sonny.Application.RevitExtensions ;

/// <summary>
///     Extension methods for Window dialogs
/// </summary>
public static class DialogExtension
{
    public static void SetOwnerByRevit(this Window window)
    {
        if (HwndSource.FromHwnd(ComponentManager.ApplicationWindow)
                ?.RootVisual is Window mainWindow)
        {
            window.Owner = mainWindow ;
        }
    }
}
