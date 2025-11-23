using Microsoft.Extensions.DependencyInjection ;
using SonnyApplication.Config.Logging ;
using SonnyApplication.Features.AutoColumnDimension.Interfaces ;
using SonnyApplication.Features.AutoColumnDimension.Services ;
using SonnyApplication.Interfaces ;
using SonnyApplication.Services ;

namespace SonnyApplication ;

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
        if (s_serviceProvider != null)
        {
            return ; // Already initialized
        }

        lock (s_lock)
        {
            if (s_serviceProvider != null)
            {
                return ; // Double-check locking
            }

            var services = new ServiceCollection() ;

            //Logging
            services.AddSerilogConfiguration() ;

            // Common services
            services.AddSingleton<IMessageService, MessageService>() ;
            services.AddSingleton<IUnitConverter, RevitUnitConverter>() ;

            // AutoColumnDimension services
            services.AddSingleton<IGridFinder, GridFinder>() ;
            services.AddSingleton<IDimensionCreator, DimensionCreator>() ;
            services.AddSingleton<IAutoColumnDimensionService, AutoColumnDimensionService>() ;
            services.AddSingleton<IAutoColumnDimensionHandler, AutoColumnDimensionHandler>() ;

            s_serviceProvider = services.BuildServiceProvider() ;
        }
    }

    /// <summary>
    ///     Ensures the host is initialized (lazy initialization for Addin Manager debugging)
    /// </summary>
    private static void EnsureInitialized()
    {
        if (s_serviceProvider == null)
        {
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
}
