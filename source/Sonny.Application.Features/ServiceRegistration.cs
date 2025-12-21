// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.Core.Interfaces ;
using Sonny.Application.Core.Services ;
using Sonny.Application.Features.AutoColumnDimension.Interfaces ;
using Sonny.Application.Features.AutoColumnDimension.Services ;
using Sonny.Application.Features.AutoColumnDimension.ViewModels ;
using Sonny.Application.Features.AutoColumnDimension.Views ;
using Sonny.Application.Features.ColumnFromCad.Interfaces ;
using Sonny.Application.Features.ColumnFromCad.Models ;
using Sonny.Application.Features.ColumnFromCad.Services ;
using Sonny.Application.Features.ColumnFromCad.ViewModels ;
using Sonny.Application.Features.ColumnFromCad.Views ;
using Sonny.Application.Features.Settings.ViewModels ;
using Sonny.Application.Features.Settings.Views ;
using Sonny.Application.UI.Views ;

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

        // ColumnFromCad services
        services.AddSingleton<IColumnFamilyLoader, ColumnFamilyLoader>() ;
        services.AddSingleton<ICadLinkSelector, CadLinkSelector>() ;
        services.AddSingleton<IRectangularColumnExtractor, RectangularColumnExtractor>() ;
        services.AddSingleton<ICircularColumnExtractor, CircularColumnExtractor>() ;

        services.AddTransient<IColumnFromCadContext, ColumnFromCadContext>() ;

        // ViewModel settings service for ColumnFromCad
        services.AddTransient<IViewModelSettingsService<ColumnFromCadSettings>>(_ =>
            new ViewModelSettingsService<ColumnFromCadSettings>("ColumnFromCadSettings.json")) ;

        services.AddTransient<ColumnFromCadViewModel>() ;
        services.AddTransient<ColumnFromCadView>() ;
        services.AddTransient<IColumnFromCadOrchestrator, ColumnFromCadOrchestrator>() ;
        services.AddTransient<ProgressView>() ;

        // Settings services
        services.AddTransient<SettingsViewModel>() ;
        services.AddTransient<SettingsView>() ;
    }
}
