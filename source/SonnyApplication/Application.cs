using Nice3point.Revit.Toolkit.External ;
using Revit.Async ;
using SonnyApplication.Modules ;

namespace SonnyApplication ;

/// <summary>
///     Application entry point
/// </summary>
[UsedImplicitly]
public class Application : ExternalApplication
{
  private readonly SonnyModule _module = new() ;

  public override void OnStartup()
  {
    // Initialize RevitTask for async Revit API calls
    RevitTask.Initialize(Application) ;

    Host.Start() ;

    // Initialize EasyRibbon module
    _module.OnStartup(Application) ;
  }

  public override void OnShutdown()
  {
    // Shutdown EasyRibbon module
    _module.OnShutdown(Application) ;
  }
}
