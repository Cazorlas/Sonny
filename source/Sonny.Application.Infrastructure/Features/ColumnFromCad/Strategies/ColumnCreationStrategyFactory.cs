// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Sonny.Application.Domain.Entities.ColumnFromCad.Contexts ;
using Sonny.Application.Domain.Entities.ColumnFromCad.Models ;
using Sonny.Application.Domain.Entities.ColumnFromCad.Services ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.Infrastructure.Revit.Services ;

namespace Sonny.Application.Infrastructure.Features.ColumnFromCad.Strategies ;

public class ColumnCreationStrategyFactory(IRevitDocument revitDocument,
    IPoint3DConverter point3DConverter,
    IDisplayUnitProvider displayUnitProvider,
    IUnitConverter unitConverter,
    ISettingsService settingsService) : IColumnCreationStrategyFactory
{
    public IColumnCreationStrategy? CreateStrategy(ColumnModel columnModel,
        ColumnCreationContext columnCreationContext)
    {
        if (columnModel is CircularColumnModel circularColumnModel) {
            return new CircularColumnCreationStrategy(circularColumnModel,
                columnCreationContext,
                revitDocument,
                point3DConverter,
                displayUnitProvider,
                unitConverter,
                settingsService) ;
        }

        if (columnModel is RectangularColumnModel rectangularColumnModel) {
            return new RectangularColumnCreationStrategy(rectangularColumnModel,
                columnCreationContext,
                revitDocument,
                point3DConverter,
                displayUnitProvider,
                unitConverter,
                settingsService) ;
        }

        return null ;
    }
}
