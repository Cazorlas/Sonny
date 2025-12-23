using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.UseCases.Interfaces ;
using Sonny.Application.Infrastructure.Services ;

namespace Sonny.Application.Infrastructure ;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        // Register IDocumentQuery implementation
        // Note: Using factory to avoid circular dependency (IRevitDocument depends on IDocumentQuery)
        services.AddScoped<IDocumentQuery>(sp =>
        {
            var revitDocument = sp.GetRequiredService<IRevitDocument>() ;
            return new DocumentQuery(revitDocument) ;
        }) ;

        // Register IResourceHelper implementation
        services.AddSingleton<IResourceHelper, ResourceHelperService>() ;
    }
}


