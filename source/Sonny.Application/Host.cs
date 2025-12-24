using Microsoft.Extensions.DependencyInjection ;
using Serilog ;
using Sonny.Application.Domain.Config.Logging ;
using Sonny.Application.Domain.Entites.ColumnFromCad.Models ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.Infrastructure.AutoColumnDimension.Services ;
using Sonny.Application.Infrastructure.Managers ;
using Sonny.Application.Infrastructure.Services ;
using Sonny.Application.Presentation ;
using Sonny.Application.UseCases.AutoColumnDimension.Services ;
using Sonny.Application.UseCases.ColumnFromCad.Implements ;
using Sonny.Application.UseCases.ColumnFromCad.Services ;
using Sonny.Application.UseCases.Services ;

namespace Sonny.Application ;

/// <summary>
///     Provides a host for the application's services and manages their lifetimes
/// </summary>
public static class Host
{
    private static IServiceProvider? s_serviceProvider ;
    private static readonly object s_lock = new() ;

    /// <summary>
    ///     Starts the host and configures the application's services
    /// </summary>
    public static void Start()
    {
        if (s_serviceProvider != null) {
            return ; // Already initialized
        }

        lock (s_lock) {
            if (s_serviceProvider != null) {
                return ; // Double-check locking
            }

            var services = new ServiceCollection() ;

            // Logging
            services.AddSerilogConfiguration() ;
            services.AddSingleton<IResourceHelper, ResourceHelper>() ;
            services.AddSingleton<ITransactionManagerFactory, TransactionManagerFactory>() ;
            services.AddSingleton<IFamilySymbolProvider, FamilySymbolProvider>() ;
            services.AddSingleton<ICadLayerProvider, CadLayerProvider>() ;
            services.AddSingleton<IGeometryHelper, GeometryHelper>() ;
            services.AddSingleton<IFailurePreprocessorFactory, FailurePreprocessorFactory>() ;
            services.AddSingleton<IPoint3DConverter, Point3DConverter>() ;

            // Common services
            services.AddSingleton<IMessageService, MessageService>() ;
            services.AddSingleton<IUnitConverter, UnitConverter>() ;
            services.AddSingleton<ISettingsService, SettingsService>() ;

            // UIDocument Provider (Singleton - stores current UIDocument)
            services.AddSingleton<IUIDocumentProvider, UIDocumentProvider>() ;
            services.AddScoped<IDocumentQuery, DocumentQuery>() ;


            // RevitDocument (Singleton - gets UIDocument from provider each time, no caching)
            services.AddTransient<IRevitDocument, RevitDocument>() ;

            // CommonServices (Singleton - gets fresh UIDocument from provider via IRevitDocument)
            services.AddTransient<ICommonServices, CommonServices>() ;

            // ColumnFromCadFromCad
            services.AddSingleton<ICadLinkSelector, CadLinkSelector>() ;
            services.AddSingleton<IColumnFamilyLoader, ColumnFamilyLoader>() ;
            services.AddSingleton<IColumnModelFactory, ColumnModelFactory>() ;

            services.AddSingleton<IRectangularColumnExtractor, RectangularColumnExtractor>() ;
            services.AddSingleton<ICircularColumnExtractor, CircularColumnExtractor>() ;
            services.AddTransient<IColumnFromCadContext, ColumnFromCadContext>() ;
            services.AddTransient<IColumnFromCadInteractor, ColumnFromCadInteractor>() ;

            // AutoColumnDimension
            services.AddSingleton<IGridFinder, GridFinder>() ;
            services.AddSingleton<IDimensionCreator, DimensionCreator>() ;
            services.AddSingleton<IAutoColumnDimension, AutoColumnDimension>() ;
            services.AddSingleton<IAutoColumnDimensionInteractor, AutoColumnDimensionInteractor>() ;
            services.AddTransient<IViewModelSettingsService<ColumnFromCadSettings>>(_ =>
                new ViewModelSettingsService<ColumnFromCadSettings>()) ;

            services.AddPresentationsServices() ; // Presentation services (Views)

            s_serviceProvider = services.BuildServiceProvider() ;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException ;
        }
    }

    /// <summary>
    ///     Ensures the host is initialized (lazy initialization for Addin Manager debugging)
    /// </summary>
    private static void EnsureInitialized()
    {
        if (s_serviceProvider == null) {
            Start() ;
        }
    }

    /// <summary>
    ///     Get service of type <typeparamref name="T" />
    /// </summary>
    /// <typeparam name="T">The type of service object to get</typeparam>
    /// <exception cref="System.InvalidOperationException">There is no service of type <typeparamref name="T" /></exception>
    public static T GetService<T>() where T : class
    {
        EnsureInitialized() ;
        return s_serviceProvider!.GetRequiredService<T>() ;
    }

    private static void OnUnhandledException(object sender,
        UnhandledExceptionEventArgs args)
    {
        var exception = (Exception)args.ExceptionObject ;
        var logger = GetService<ILogger>() ;
        logger.Fatal(exception,
            "Domain unhandled exception") ;
    }
}
