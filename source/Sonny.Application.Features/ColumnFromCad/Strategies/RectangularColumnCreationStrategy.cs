// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Sonny.Application.Features.ColumnFromCad.Contexts ;
using Sonny.Application.Features.ColumnFromCad.Models ;
using Sonny.RevitExtensions.Extensions.Families ;

namespace Sonny.Application.Features.ColumnFromCad.Strategies ;

public class RectangularColumnCreationStrategy(
    RectangularColumnModel rectangularColumnModel,
    ColumnCreationContext columnCreationContext) : ColumnCreationStrategy(rectangularColumnModel,
    columnCreationContext)
{
    protected override FamilySymbol? GetOrCreateFamilySymbol()
    {
        if (Math.Abs(rectangularColumnModel.ShortSide) < Tolerance
            || Math.Abs(rectangularColumnModel.LongSide) < Tolerance)
        {
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
        if (rectangularColumnModel.RotationAngle >= 0)
        {
            ElementTransformUtils.RotateElement(element.Document,
                element.Id,
                Line.CreateBound(ColumnModel.Center,
                    ColumnModel.Center.Add(XYZ.BasisZ)),
                rectangularColumnModel.RotationAngle) ;
        }
        else
        {
            ElementTransformUtils.RotateElement(element.Document,
                element.Id,
                Line.CreateBound(ColumnModel.Center,
                    ColumnModel.Center.Add(XYZ.BasisZ)),
                -rectangularColumnModel.RotationAngle + Math.PI / 2) ;
        }
    }

    private FamilySymbol? GetOrCreateRectangularFamilySymbol(Family family,
        double width,
        double height,
        string widthParameter,
        string heightParameter)
    {
        var allFamilySymbols = family.GetFamilySymbols()
            .ToList() ;

        // Try to find existing symbol with matching dimensions
        foreach (var familySymbol in allFamilySymbols)
        {
            var widthParam = familySymbol.LookupParameter(widthParameter) ;
            var heightParam = familySymbol.LookupParameter(heightParameter) ;

            if (widthParam == null
                || heightParam == null)
            {
                continue ;
            }

            var widthValue = GetDoubleValue(widthParam) ;
            var heightValue = GetDoubleValue(heightParam) ;

            if (Math.Abs(widthValue - width) < Tolerance
                && Math.Abs(heightValue - height) < Tolerance)
            {
                return familySymbol ;
            }
        }

        // Create new symbol if not found
        if (allFamilySymbols.Count == 0)
        {
            return null ;
        }

        var widthMm = Math.Round(width.ToMillimeters(),
            0) ;
        var heightMm = Math.Round(height.ToMillimeters(),
            0) ;

        if (Math.Abs(widthMm) < Tolerance
            || Math.Abs(heightMm) < Tolerance)
        {
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
