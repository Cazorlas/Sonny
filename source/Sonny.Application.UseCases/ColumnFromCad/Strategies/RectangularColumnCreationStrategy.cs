// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Sonny.Application.Domain.Entites.ColumnFromCad.Contexts ;
using Sonny.Application.Domain.Entites.ColumnFromCad.Models ;
using Sonny.Application.Domain.Interfaces ;

namespace Sonny.Application.UseCases.ColumnFromCad.Strategies ;

public class RectangularColumnCreationStrategy(
    RectangularColumnModel rectangularColumnModel,
    ColumnCreationContext columnCreationContext,
    IFamilySymbolProvider familySymbolProvider,
    IGeometryHelper geometryHelper,
    IPoint3DConverter point3DConverter) : ColumnCreationStrategy(rectangularColumnModel,
    columnCreationContext,
    point3DConverter)
{
    private readonly IFamilySymbolProvider _familySymbolProvider = familySymbolProvider ;
    private readonly IGeometryHelper _geometryHelper = geometryHelper ;

    protected override FamilySymbol? GetOrCreateFamilySymbol()
    {
        if (Math.Abs(rectangularColumnModel.ShortSide) < Tolerance
            || Math.Abs(rectangularColumnModel.LongSide) < Tolerance) {
            return null ;
        }

        var familySymbol = GetOrCreateRectangularFamilySymbol(ColumnCreationContext.SelectedRectangularColumnFamily,
            rectangularColumnModel.ShortSide,
            rectangularColumnModel.LongSide,
            ColumnCreationContext.WidthParameter,
            ColumnCreationContext.HeightParameter) ;


        return familySymbol ;
    }

    protected override void RotateElement(Element element)
    {
        // Rotate column if needed
        var centerXyz = Point3DConverter.ToXyz(ColumnModel.Center) ;
        if (rectangularColumnModel.RotationAngle >= 0) {
            ElementTransformUtils.RotateElement(element.Document,
                element.Id,
                Line.CreateBound(centerXyz,
                    centerXyz.Add(XYZ.BasisZ)),
                rectangularColumnModel.RotationAngle) ;
        }
        else {
            ElementTransformUtils.RotateElement(element.Document,
                element.Id,
                Line.CreateBound(centerXyz,
                    centerXyz.Add(XYZ.BasisZ)),
                -rectangularColumnModel.RotationAngle + Math.PI / 2) ;
        }
    }

    private FamilySymbol? GetOrCreateRectangularFamilySymbol(Family family,
        double width,
        double height,
        string widthParameter,
        string heightParameter)
    {
        var allFamilySymbols = _familySymbolProvider.GetFamilySymbols(family)
            .ToList() ;

        // Try to find existing symbol with matching dimensions
        foreach (var familySymbol in allFamilySymbols) {
            var widthParam = familySymbol.LookupParameter(widthParameter) ;
            var heightParam = familySymbol.LookupParameter(heightParameter) ;

            if (widthParam == null
                || heightParam == null) {
                continue ;
            }

            var widthValue = GetDoubleValue(widthParam) ;
            var heightValue = GetDoubleValue(heightParam) ;

            if (Math.Abs(widthValue - width) < Tolerance
                && Math.Abs(heightValue - height) < Tolerance) {
                return familySymbol ;
            }
        }

        // Create new symbol if not found
        if (allFamilySymbols.Count == 0) {
            return null ;
        }

        var widthMm = Math.Round(_geometryHelper.ToMillimeters(width),
            0) ;
        var heightMm = Math.Round(_geometryHelper.ToMillimeters(height),
            0) ;

        if (Math.Abs(widthMm) < Tolerance
            || Math.Abs(heightMm) < Tolerance) {
            return null ;
        }

        var name = $"{widthMm}x{heightMm}" ;

        var newSymbol = allFamilySymbols[0]
            .Duplicate(name) as FamilySymbol ;
        newSymbol?.LookupParameter(widthParameter)
            ?.Set(width) ;
        newSymbol?.LookupParameter(heightParameter)
            ?.Set(height) ;

        return newSymbol ;
    }
}
