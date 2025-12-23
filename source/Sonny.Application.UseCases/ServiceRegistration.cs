// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection ;
using Sonny.Application.Domain.InputPorts.ColumnFromCad ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.Entities.ColumnFromCad ;
using Sonny.Application.UseCases.ColumnFromCad.Services ;

namespace Sonny.Application.UseCases ;

public static class ServiceRegistration
{
    public static void AddUseCaseServices(this IServiceCollection services)
    {
        // ColumnFromCad services
        // Note: All implementations (ICadLinkSelector, IColumnFamilyLoader, IRectangularColumnExtractor,
        // ICircularColumnExtractor, IColumnFromCadContext) are registered in Infrastructure layer

        // ViewModel settings service for ColumnFromCad
        services.AddTransient<IViewModelSettingsService<ColumnFromCadSettings>>(_ =>
            new ViewModelSettingsService<ColumnFromCadSettings>()) ;

        services.AddTransient<IColumnFromCadInteractor, ColumnFromCadInteractor>() ;
    }
}
