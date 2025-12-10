// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Autodesk.Revit.UI ;
using Serilog ;
using Sonny.Application.Core.Interfaces ;
using Sonny.Application.Core.Services ;

namespace Sonny.Application.Utils ;

public static class ServiceUtils
{
    public static ICommonServices CreateCommonServices(UIDocument uiDocument)
    {
        var revitDocumentService = new RevitDocumentService(uiDocument) ;
        var messageService = Host.GetService<IMessageService>() ;
        var logger = Host.GetService<ILogger>() ;
        var unitConverter = Host.GetService<IUnitConverter>() ;

        return new CommonServices(revitDocumentService,
            messageService,
            logger,
            unitConverter) ;
    }
}
