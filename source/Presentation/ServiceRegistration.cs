// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.Features.AutoColumnDimension.Views ;
using Sonny.Application.Features.ColumnFromCad.Views ;
using Sonny.Application.Features.Settings.Views ;
using Sonny.Application.Features.Views ;

namespace Sonny.Application.Presentation ;

/// <summary>
///     Service registration for Presentation Layer
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    ///     Adds Presentation Layer services to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void AddPresentationServices(this IServiceCollection services)
    {
        // Register Views
        services.AddTransient<AutoColumnDimensionView>() ;
        services.AddTransient<ColumnFromCadView>() ;
        services.AddTransient<SettingsView>() ;
        services.AddTransient<ProgressView>() ;
    }
}
