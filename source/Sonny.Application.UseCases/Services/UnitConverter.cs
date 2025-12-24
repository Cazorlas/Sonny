using Sonny.Application.Domain.Interfaces ;

namespace Sonny.Application.UseCases.Services ;

public class UnitConverter : IUnitConverter
{
    private static readonly ForgeTypeId s_internalUnit = UnitTypeId.Feet ;

    public double ToInternalUnit(double value,
        ForgeTypeId displayUnit)
    {
        if (displayUnit == s_internalUnit) {
            return value ;
        }

        return UnitUtils.Convert(value,
            displayUnit,
            s_internalUnit) ;
    }

    public double FromInternalUnit(double value,
        ForgeTypeId displayUnit)
    {
        if (displayUnit == s_internalUnit) {
            return value ;
        }

        return UnitUtils.Convert(value,
            s_internalUnit,
            displayUnit) ;
    }

    public ForgeTypeId GetDefaultDisplayUnit(Document document)
    {
        // If document uses metric, default to millimeters
        // Otherwise use feet
        var isMetric = document.DisplayUnitSystem == DisplayUnit.METRIC ;
        return isMetric ? UnitTypeId.Millimeters : UnitTypeId.Feet ;
    }

    public string FormatWithUnit(double value,
        ForgeTypeId displayUnit)
    {
        var unitName = GetUnitDisplayName(displayUnit) ;
        return $"{value:F2} {unitName}" ;
    }

    public string GetUnitDisplayName(ForgeTypeId unitTypeId) =>
        unitTypeId.TypeId switch
        {
            "autodesk.unit.unit:millimeters-1.0.1" => "mm",
            "autodesk.unit.unit:centimeters-1.0.1" => "cm",
            "autodesk.unit.unit:meters-1.0.0" => "m",
            "autodesk.unit.unit:feet-1.0.1" => "ft",
            "autodesk.unit.unit:inches-1.0.1" => "in",
            _ => unitTypeId.ToString()!
        } ;
}
