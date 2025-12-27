using Serilog ;
using Sonny.Application.Domain.Services ;

namespace Sonny.Application.Presentation.Services ;

/// <summary>
///     Common services container for ViewModels
///     Contains services that are used in almost all ViewModels
/// </summary>
public interface ICommonServices
{
    IMessageService MessageService { get ; }
    ILogger Logger { get ; }
    IUnitConverter UnitConverter { get ; }
    ISettingsService SettingsService { get ; }
    IResourceHelper ResourceHelper { get ; }
}
