// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.Features.AutoColumnDimension.Interfaces ;
using Sonny.Application.Features.AutoColumnDimension.Services ;
using Sonny.Application.Features.AutoColumnDimension.ViewModels ;
using Sonny.Application.Features.AutoColumnDimension.Views ;
using Sonny.Application.Features.Settings.ViewModels ;
using Sonny.Application.Features.Settings.Views ;

namespace Sonny.Application.Features ;

public static class ServiceRegistration
{
    public static void AddFeatureServices(this IServiceCollection services)
    {
        // AutoColumnDimension services
        services.AddSingleton<IGridFinder, GridFinder>() ;
        services.AddSingleton<IDimensionCreator, DimensionCreator>() ;
        services.AddSingleton<IAutoColumnDimensionService, AutoColumnDimensionService>() ;
        services.AddSingleton<IAutoColumnDimensionHandler, AutoColumnDimensionHandler>() ;

        services.AddTransient<AutoColumnDimensionViewModel>() ;
        services.AddTransient<AutoColumnDimensionView>() ;

        // Settings services
        services.AddTransient<SettingsViewModel>() ;
        services.AddTransient<SettingsView>() ;
    }
}
