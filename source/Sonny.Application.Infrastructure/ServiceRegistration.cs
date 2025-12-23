using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.UseCases.Interfaces ;
using Sonny.Application.Infrastructure.Services ;

namespace Sonny.Application.Infrastructure ;

/// <summary>
///     Service registration for Infrastructure Layer implementations
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    ///     Adds Infrastructure Layer services to the service collection
    /// </summary>
    /// <param name="services">Service collection</param>
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        // Register IDocumentQuery implementation
        services.AddScoped<IDocumentQuery>(sp =>
        {
            var revitDocument = sp.GetRequiredService<IRevitDocument>() ;
            return new DocumentQuery(revitDocument.Document) ;
        }) ;
    }
}


