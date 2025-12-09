using Autodesk.Revit.UI;
using Sonny.EasyRibbon;
using Sonny.EasyRibbon.Modules;
using SonnyApplication.Ribbon;

namespace SonnyApplication.Modules;

/// <summary>
///     Sonny application module for EasyRibbon integration
/// </summary>
public class SonnyModule : IApplicationModule
{
    public string ModuleName => "Sonny Application Module";

    public void OnStartup(UIControlledApplication application)
    {
        // Create ribbon UI using Sonny.EasyRibbon
        CreateUIApp.CreateUI<SonnyTab>(application);
    }

    public void OnShutdown(UIControlledApplication application)
    {
        // Cleanup if needed (optional)
    }
}

