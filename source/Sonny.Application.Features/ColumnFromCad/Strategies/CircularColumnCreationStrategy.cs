// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Sonny.Application.Features.ColumnFromCad.Contexts ;
using Sonny.Application.Features.ColumnFromCad.Models ;
using Sonny.RevitExtensions.Extensions.Families ;

namespace Sonny.Application.Features.ColumnFromCad.Strategies ;

public class CircularColumnCreationStrategy(
    CircularColumnModel circularColumnModel,
    ColumnCreationContext columnCreationContext) : ColumnCreationStrategy(circularColumnModel,
    columnCreationContext)
{
    protected override FamilySymbol? GetOrCreateFamilySymbol()
    {
        return GetOrCreateCircularFamilySymbol(ColumnCreationContext.SelectedCircularColumnFamily,
            circularColumnModel.Diameter,
            ColumnCreationContext.DiameterParameter) ;
    }

    /// <summary>
    ///     Gets or creates a family symbol for circular column with specified diameter
    /// </summary>
    public static FamilySymbol? GetOrCreateCircularFamilySymbol(Family family,
        double diameter,
        string diameterParameter)
    {
        var allFamilySymbols = family.GetFamilySymbols()
            .ToList() ;

        // Try to find existing symbol with matching diameter
        foreach (var familySymbol in allFamilySymbols)
        {
            var diameterParam = familySymbol.LookupParameter(diameterParameter) ;
            if (diameterParam == null)
            {
                continue ;
            }

            var diameterValue = GetDoubleValue(diameterParam) ;

            if (Math.Abs(diameterValue - diameter) < Tolerance)
            {
                return familySymbol ;
            }
        }

        // Create new symbol if not found
        if (allFamilySymbols.Count == 0)
        {
            return null ;
        }

        var diameterMm = Math.Round(diameter.ToMillimeters(),
            0) ;

        if (Math.Abs(diameterMm) < 1.0.ToMillimeters())
        {
            return null ;
        }

        var name = $"D{diameterMm}" ;

        // Check if symbol with this name already exists
        var existingSymbol = allFamilySymbols.FirstOrDefault(f => f.Name.Equals(name)) ;
        if (existingSymbol != null)
        {
            return existingSymbol ;
        }

        var newSymbol = allFamilySymbols[0]
            .Duplicate(name) as FamilySymbol ;
        newSymbol?.LookupParameter(diameterParameter)
            ?.Set(diameter) ;

        return newSymbol ;
    }
}
