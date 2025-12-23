// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.UseCases.Interfaces ;
using Sonny.Application.UseCases.Services ;
using Sonny.Application.Features.AutoColumnDimension.Interfaces ;
using Sonny.Application.Features.AutoColumnDimension.Services ;
using Sonny.Application.Features.ColumnFromCad.Interfaces ;
using Sonny.Application.Features.ColumnFromCad.Models ;
using Sonny.Application.Features.ColumnFromCad.Services ;

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


        // ColumnFromCad services
        services.AddSingleton<IColumnFamilyLoader, ColumnFamilyLoader>() ;
        services.AddSingleton<ICadLinkSelector, CadLinkSelector>() ;
        services.AddSingleton<IRectangularColumnExtractor, RectangularColumnExtractor>() ;
        services.AddSingleton<ICircularColumnExtractor, CircularColumnExtractor>() ;

        services.AddTransient<IColumnFromCadContext, ColumnFromCadContext>() ;

        // ViewModel settings service for ColumnFromCad
        services.AddTransient<IViewModelSettingsService<ColumnFromCadSettings>>(_ =>
            new ViewModelSettingsService<ColumnFromCadSettings>()) ;

        services.AddTransient<IColumnFromCadOrchestrator, ColumnFromCadOrchestrator>() ;
    }
}
