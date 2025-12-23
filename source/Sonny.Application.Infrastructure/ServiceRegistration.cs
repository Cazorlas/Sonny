using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.Infrastructure.Managers ;
using Sonny.Application.Infrastructure.Services ;
using Sonny.Application.UseCases.AutoColumnDimension.Interfaces ;
using Sonny.Application.UseCases.AutoColumnDimension.Services ;
using ICadLinkSelector = Sonny.Application.Domain.InputPorts.ColumnFromCad.ICadLinkSelector ;
using IColumnFamilyLoader = Sonny.Application.Domain.InputPorts.ColumnFromCad.IColumnFamilyLoader ;
using ICircularColumnExtractor = Sonny.Application.Domain.InputPorts.ColumnFromCad.ICircularColumnExtractor ;
using IColumnFromCadContext = Sonny.Application.Domain.InputPorts.ColumnFromCad.IColumnFromCadContext ;
using IRectangularColumnExtractor = Sonny.Application.Domain.InputPorts.ColumnFromCad.IRectangularColumnExtractor ;

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

        // Register transaction manager factory
        services.AddSingleton<ITransactionManagerFactory, TransactionManagerFactory>() ;

        // Register Domain interfaces implementations
        services.AddSingleton<IFamilySymbolProvider, FamilySymbolProvider>() ;
        services.AddSingleton<ICadLayerProvider, CadLayerProvider>() ;
        services.AddSingleton<IGeometryHelper, GeometryHelper>() ;
        services.AddSingleton<IColumnModelFactory, ColumnModelFactory>() ;
        services.AddSingleton<IFailurePreprocessorFactory, FailurePreprocessorFactory>() ;
        services.AddSingleton<IPoint3DConverter, Point3DConverter>() ;

        // Register Use Cases interfaces implementations
        // Note: Infrastructure implements Use Cases Input Ports (acceptable exception for Revit add-in)
        services.AddSingleton<ICadLinkSelector, CadLinkSelector>() ;
        services.AddSingleton<IColumnFamilyLoader, ColumnFamilyLoader>() ;
        services.AddSingleton<IRectangularColumnExtractor, RectangularColumnExtractor>() ;
        services.AddSingleton<ICircularColumnExtractor, CircularColumnExtractor>() ;
        services.AddTransient<IColumnFromCadContext, ColumnFromCadContext>() ;


        // AutoColumnDimension services
        services.AddSingleton<IGridFinder, GridFinder>() ;
        services.AddSingleton<IDimensionCreator, DimensionCreator>() ;
        services.AddSingleton<IAutoColumnDimensionService, AutoColumnDimensionService>() ;
        services.AddSingleton<IAutoColumnDimensionHandler, AutoColumnDimensionHandler>() ;
    }
}


