using System.IO ;
using Microsoft.Extensions.DependencyInjection ;
using Serilog ;
using Serilog.Core ;
using Serilog.Events ;

namespace Sonny.Application.Config.Logging ;

/// <summary>
///     Application logging configuration
/// </summary>
/// <example>
///     <code lang="csharp">
/// public class Class(ILogger logger)
/// {
///     private void Execute()
///     {
///         logger.Information("Message");
///     }
/// }
/// </code>
/// </example>
public static class LoggerConfiguration
{
    private const string LogTemplate =
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}]: {Message:lj}{NewLine}{Exception}" ;

    public static void AddSerilogConfiguration(this IServiceCollection services)
    {
        var logger = CreateDefaultLogger() ;
        services.AddSingleton<ILogger>(logger) ;

        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException ;
    }

    private static Logger CreateDefaultLogger()
    {
        var logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Sonny",
            "Logs") ;

        Directory.CreateDirectory(logDirectory) ;

        var logFilePath = Path.Combine(logDirectory,
            "sonny-.log") ;

        return new Serilog.LoggerConfiguration().WriteTo
            .Debug(LogEventLevel.Debug,
                LogTemplate)
            .WriteTo
            .Console(LogEventLevel.Debug,
                LogTemplate)
            .WriteTo
            .File(logFilePath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                outputTemplate: LogTemplate,
                restrictedToMinimumLevel: LogEventLevel.Debug)
            .MinimumLevel
            .Debug()
            .CreateLogger() ;
    }

    private static void OnUnhandledException(object sender,
        UnhandledExceptionEventArgs args)
    {
        var exception = (Exception)args.ExceptionObject ;
        var logger = Host.GetService<ILogger>() ;
        logger.Fatal(exception,
            "Domain unhandled exception") ;
    }
}
