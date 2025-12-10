// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.Features.AutoColumnDimension.Interfaces ;
using Sonny.Application.Features.AutoColumnDimension.Services ;

namespace Sonny.Application.Features ;

public static class FeatureService
{
    public static void AddFeatureServices(this IServiceCollection services)
    {
        // AutoColumnDimension services
        services.AddSingleton<IGridFinder, GridFinder>() ;
        services.AddSingleton<IDimensionCreator, DimensionCreator>() ;
        services.AddSingleton<IAutoColumnDimensionService, AutoColumnDimensionService>() ;
        services.AddSingleton<IAutoColumnDimensionHandler, AutoColumnDimensionHandler>() ;
    }
}
