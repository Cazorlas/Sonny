// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Sonny.Application.Domain.Interfaces ;
using Sonny.Application.Entities.ColumnFromCad ;
using Sonny.Application.Entities.ColumnFromCad.Contexts ;

namespace Sonny.Application.UseCases.ColumnFromCad.Strategies ;

public class CircularColumnCreationStrategy(
    CircularColumnModel circularColumnModel,
    ColumnCreationContext columnCreationContext,
    IFamilySymbolProvider familySymbolProvider,
    IGeometryHelper geometryHelper,
    IPoint3DConverter point3DConverter) : ColumnCreationStrategy(circularColumnModel,
    columnCreationContext,
    point3DConverter)
{
    private readonly IFamilySymbolProvider _familySymbolProvider = familySymbolProvider ;
    private readonly IGeometryHelper _geometryHelper = geometryHelper ;

    protected override FamilySymbol? GetOrCreateFamilySymbol() =>
        GetOrCreateCircularFamilySymbol(ColumnCreationContext.SelectedCircularColumnFamily,
            circularColumnModel.Diameter,
            ColumnCreationContext.DiameterParameter) ;

    /// <summary>
    ///     Gets or creates a family symbol for circular column with specified diameter
    /// </summary>
    private FamilySymbol? GetOrCreateCircularFamilySymbol(Family family,
        double diameter,
        string diameterParameter)
    {
        var allFamilySymbols = _familySymbolProvider.GetFamilySymbols(family)
            .ToList() ;

        // Try to find existing symbol with matching diameter
        foreach (var familySymbol in allFamilySymbols) {
            var diameterParam = familySymbol.LookupParameter(diameterParameter) ;
            if (diameterParam == null) {
                continue ;
            }

            var diameterValue = GetDoubleValue(diameterParam) ;

            if (Math.Abs(diameterValue - diameter) < Tolerance) {
                return familySymbol ;
            }
        }

        // Create new symbol if not found
        if (allFamilySymbols.Count == 0) {
            return null ;
        }

        var diameterMm = Math.Round(_geometryHelper.ToMillimeters(diameter),
            0) ;

        if (Math.Abs(diameterMm) < _geometryHelper.ToMillimeters(1.0)) {
            return null ;
        }

        var name = $"D{diameterMm}" ;

        // Check if symbol with this name already exists
        var existingSymbol = allFamilySymbols.FirstOrDefault(f => f.Name.Equals(name)) ;
        if (existingSymbol != null) {
            return existingSymbol ;
        }

        var newSymbol = allFamilySymbols[0]
            .Duplicate(name) as FamilySymbol ;
        newSymbol?.LookupParameter(diameterParameter)
            ?.Set(diameter) ;

        return newSymbol ;
    }
}
