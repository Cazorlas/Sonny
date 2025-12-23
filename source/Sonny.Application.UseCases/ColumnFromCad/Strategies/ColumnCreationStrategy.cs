// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Autodesk.Revit.DB.Structure ;
using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.Entities.ColumnFromCad ;
using Sonny.Application.Entities.ColumnFromCad.Contexts ;

namespace Sonny.Application.UseCases.ColumnFromCad.Strategies ;

public abstract class ColumnCreationStrategy(ColumnModel columnModel, ColumnCreationContext columnCreationContext, IPoint3DConverter point3DConverter)
{
    protected const double Tolerance = 0.001 ;
    protected readonly ColumnCreationContext ColumnCreationContext = columnCreationContext ;
    protected readonly ColumnModel ColumnModel = columnModel ;
    protected readonly IPoint3DConverter Point3DConverter = point3DConverter ;

    public static ColumnCreationStrategy? CreateInstance(ColumnModel columnModel,
        ColumnCreationContext columnCreationContext,
        IFamilySymbolProvider familySymbolProvider,
        IGeometryHelper geometryHelper,
        IPoint3DConverter point3DConverter)
    {
        if (columnModel is CircularColumnModel circularColumnModel) {
            return new CircularColumnCreationStrategy(circularColumnModel,
                columnCreationContext,
                familySymbolProvider,
                geometryHelper,
                point3DConverter) ;
        }

        if (columnModel is RectangularColumnModel rectangularColumnModel) {
            return new RectangularColumnCreationStrategy(rectangularColumnModel,
                columnCreationContext,
                familySymbolProvider,
                geometryHelper,
                point3DConverter) ;
        }

        return null ;
    }

    public Element? Execute()
    {
        if (GetOrCreateFamilySymbol() is not { } familySymbol) {
            return null ;
        }

        if (! familySymbol.IsActive) {
            familySymbol.Activate() ;
        }

        var document = ColumnCreationContext.Document ;
        var centerXyz = Point3DConverter.ToXyz(ColumnModel.Center) ;
        var instance = document.Create.NewFamilyInstance(centerXyz,
            familySymbol,
            ColumnCreationContext.BaseLevel,
            StructuralType.Column) ;

        instance.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM)
            .Set(ColumnCreationContext.BaseLevel.Id) ;
        instance.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM)
            .Set(ColumnCreationContext.TopLevel.Id) ;
        instance.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM)
            .Set(ColumnCreationContext.BaseOffset) ;
        instance.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM)
            .Set(ColumnCreationContext.TopOffset) ;

        RotateElement(instance) ;

        return instance ;
    }

    protected abstract FamilySymbol? GetOrCreateFamilySymbol() ;

    protected virtual void RotateElement(Element element)
    {
    }

    protected static double GetDoubleValue(Parameter parameter) =>
        parameter.StorageType switch
        {
            StorageType.Double => parameter.AsDouble(),
            StorageType.Integer => parameter.AsInteger(),
            _ => 0
        } ;
}
