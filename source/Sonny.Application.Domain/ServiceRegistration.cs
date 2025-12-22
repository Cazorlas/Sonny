// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.Domain.Config.Logging ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.Domain.Services ;

namespace Sonny.Application.Domain ;

public static class ServiceRegistration
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        // Logging
        services.AddSerilogConfiguration() ;

        // Common services
        services.AddSingleton<IMessageService, MessageService>() ;
        services.AddSingleton<IUnitConverter, RevitUnitConverter>() ;
        services.AddSingleton<ISettingsService, SettingsService>() ;

        // UIDocument Provider (Singleton - stores current UIDocument)
        services.AddSingleton<IUIDocumentProvider, UIDocumentProvider>() ;

        // RevitDocumentService (Singleton - gets UIDocument from provider each time, no caching)
        services.AddTransient<IRevitDocument, RevitDocumentService>() ;

        // CommonServices (Singleton - gets fresh UIDocument from provider via IRevitDocument)
        services.AddTransient<ICommonServices, CommonServices>() ;
    }
}
