using Serilog ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.Presentation.Services ;

namespace Sonny.Application.Presentation.Implements ;

public class CommonServices(
    IMessageService messageService,
    ILogger logger,
    IUnitConverter unitConverter,
    ISettingsService settingsService,
    IResourceHelper resourceHelper) : ICommonServices
{
    public IMessageService MessageService { get ; } = messageService ;
    public ILogger Logger { get ; } = logger ;
    public IUnitConverter UnitConverter { get ; } = unitConverter ;
    public ISettingsService SettingsService { get ; } = settingsService ;
    public IResourceHelper ResourceHelper { get ; } = resourceHelper ;
}
