// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.Core.Config.Logging ;
using Sonny.Application.Core.Interfaces ;
using Sonny.Application.Core.Services ;

namespace Sonny.Application.Core ;

public static class CoreService
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        // Logging
        services.AddSerilogConfiguration() ;

        // Common services
        services.AddSingleton<IMessageService, MessageService>() ;
        services.AddSingleton<IUnitConverter, RevitUnitConverter>() ;

        // UIDocument Provider (Singleton - stores current UIDocument)
        services.AddSingleton<IUIDocumentProvider, UIDocumentProvider>() ;

        // RevitDocumentService (Transient - creates new instance each time, gets UIDocument from provider)
        services.AddTransient<IRevitDocument, RevitDocumentService>() ;
    }
}
