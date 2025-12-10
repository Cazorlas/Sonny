// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Serilog ;
using Sonny.Application.Core.Interfaces ;
using Sonny.Application.Core.Services ;

namespace Sonny.Application.Utils ;

public static class ServiceUtils
{
    /// <summary>
    ///     Creates common services using dependency injection
    /// </summary>
    /// <returns>ICommonServices instance</returns>
    public static ICommonServices CreateCommonServices()
    {
        var revitDocumentService = Host.GetService<IRevitDocument>() ;
        var messageService = Host.GetService<IMessageService>() ;
        var logger = Host.GetService<ILogger>() ;
        var unitConverter = Host.GetService<IUnitConverter>() ;
        var settingsService = Host.GetService<ISettingsService>() ;

        return new CommonServices(revitDocumentService,
            messageService,
            logger,
            unitConverter,
            settingsService) ;
    }
}
