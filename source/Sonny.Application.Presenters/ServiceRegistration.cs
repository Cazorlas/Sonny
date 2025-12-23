// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.Presenters.AutoColumnDimension.ViewModels ;
using Sonny.Application.Presenters.AutoColumnDimension.Views ;
using Sonny.Application.Presenters.ColumnFromCad.ViewModels ;
using Sonny.Application.Presenters.ColumnFromCad.Views ;
using Sonny.Application.Presenters.Settings.ViewModels ;
using Sonny.Application.Presenters.Settings.Views ;
using Sonny.Application.Presenters.Views ;

namespace Sonny.Application.Presenters ;

/// <summary>
///     Service registration for Sonny.Application.Presenters Layer
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    ///     Adds Sonny.Application.Presenters Layer services to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void AddPresentersServices(this IServiceCollection services)
    {
        // Register Views
        services.AddTransient<AutoColumnDimensionViewModel>() ;
        services.AddTransient<AutoColumnDimensionView>() ;

        services.AddTransient<ColumnFromCadViewModel>() ;
        services.AddTransient<ColumnFromCadView>() ;

        // Settings services
        services.AddTransient<SettingsViewModel>() ;
        services.AddTransient<SettingsView>() ;

        services.AddTransient<ProgressView>() ;
    }
}

