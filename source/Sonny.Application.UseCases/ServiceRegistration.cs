// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.UseCases.AutoColumnDimension.Interfaces ;
using Sonny.Application.UseCases.AutoColumnDimension.Services ;
using Sonny.Application.UseCases.ColumnFromCad.Interfaces ;
using Sonny.Application.UseCases.ColumnFromCad.Models ;
using Sonny.Application.UseCases.ColumnFromCad.Services ;

namespace Sonny.Application.UseCases ;

public static class ServiceRegistration
{
    public static void AddUseCaseServices(this IServiceCollection services)
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
