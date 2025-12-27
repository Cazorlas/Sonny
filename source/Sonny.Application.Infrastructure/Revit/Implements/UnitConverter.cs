using Sonny.Application.Domain.Entities.Settings ;
using Sonny.Application.Domain.Services ;

namespace Sonny.Application.Infrastructure.Revit.Implements ;

public class UnitConverter : IUnitConverter
{
    private static readonly ForgeTypeId s_internalUnit = UnitTypeId.Feet ;

    public double ToInternalUnit(double value,
        AppDisplayUnit displayUnit)
    {
        var forgeTypeId = ConvertToForgeTypeId(displayUnit) ;

        if (forgeTypeId == s_internalUnit) {
            return value ;
        }

        return UnitUtils.Convert(value,
            forgeTypeId,
            s_internalUnit) ;
    }

    public double FromInternalUnit(double value,
        AppDisplayUnit displayUnit)
    {
        var forgeTypeId = ConvertToForgeTypeId(displayUnit) ;

        if (forgeTypeId == s_internalUnit) {
            return value ;
        }

        return UnitUtils.Convert(value,
            s_internalUnit,
            forgeTypeId) ;
    }

    public string FormatWithUnit(double value,
        AppDisplayUnit displayUnit)
    {
        var unitName = GetUnitDisplayName(displayUnit) ;
        return $"{value:F2} {unitName}" ;
    }

    public string GetUnitDisplayName(AppDisplayUnit displayUnit)
    {
        var forgeTypeId = ConvertToForgeTypeId(displayUnit) ;
        var typeId = forgeTypeId.TypeId ;

        return typeId switch
        {
            "autodesk.unit.unit:millimeters-1.0.1" => "mm",
            "autodesk.unit.unit:centimeters-1.0.1" => "cm",
            "autodesk.unit.unit:meters-1.0.0" => "m",
            "autodesk.unit.unit:feet-1.0.1" => "ft",
            "autodesk.unit.unit:inches-1.0.1" => "in",
            _ => "ft"
        } ;
    }

    private static ForgeTypeId ConvertToForgeTypeId(AppDisplayUnit unit) =>
        unit switch
        {
            AppDisplayUnit.Millimeters => UnitTypeId.Millimeters,
            AppDisplayUnit.Centimeters => UnitTypeId.Centimeters,
            AppDisplayUnit.Meters => UnitTypeId.Meters,
            AppDisplayUnit.Feet => UnitTypeId.Feet,
            AppDisplayUnit.Inches => UnitTypeId.Inches,
            _ => UnitTypeId.Feet
        } ;
}
