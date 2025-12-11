using Nice3point.Revit.Toolkit.External ;
using Revit.Async ;
using Sonny.Application.Core.Interfaces ;
using Sonny.Application.Features ;
using Sonny.Application.Modules ;

namespace Sonny.Application ;

/// <summary>
///     Application entry point
/// </summary>
[UsedImplicitly]
public class SonnyApp : ExternalApplication
{
    private readonly SonnyModule _module = new() ;

    public override void OnStartup()
    {
        // Initialize RevitTask for async Revit API calls
        RevitTask.Initialize(Application) ;

        Host.Start() ;
        // Initialize resources if not already initialized
        var settingsService = Host.GetService<ISettingsService>() ;
        var currentLanguage = settingsService.GetLanguage() ;
        SonnyResourcesInitializer.Initialize(currentLanguage) ;

        // Initialize EasyRibbon module
        _module.OnStartup(Application) ;
    }

    public override void OnShutdown() =>
        // Shutdown EasyRibbon module
        _module.OnShutdown(Application) ;
}
