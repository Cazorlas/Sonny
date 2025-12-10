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
    }
}
