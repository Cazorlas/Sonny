using SonnyApplication.Interfaces ;

namespace SonnyApplication.Services ;

/// <summary>
/// Unit converter implementation using Revit UnitUtils
/// </summary>
public class RevitUnitConverter : IUnitConverter
{
    private static readonly ForgeTypeId s_internalUnit = UnitTypeId.Feet ;

    /// <summary>
    /// Convert value from display unit to internal unit (feet)
    /// </summary>
    public double ToInternalUnit(double value,
        ForgeTypeId displayUnit)
    {
        if (displayUnit == s_internalUnit)
            return value ;

        return UnitUtils.Convert(value,
            displayUnit,
            s_internalUnit) ;
    }

    /// <summary>
    /// Convert value from internal unit (feet) to display unit
    /// </summary>
    public double FromInternalUnit(double value,
        ForgeTypeId displayUnit)
    {
        if (displayUnit == s_internalUnit)
            return value ;

        return UnitUtils.Convert(value,
            s_internalUnit,
            displayUnit) ;
    }

    /// <summary>
    /// Get default display unit based on document settings or user preference
    /// </summary>
    public ForgeTypeId GetDefaultDisplayUnit(Document document)
    {
        // If document uses metric, default to millimeters
        // Otherwise use feet
        var isMetric = document.DisplayUnitSystem == DisplayUnit.METRIC ;
        return isMetric ? UnitTypeId.Millimeters : UnitTypeId.Feet ;
    }

    /// <summary>
    /// Format value with unit suffix for display
    /// </summary>
    public string FormatWithUnit(double value,
        ForgeTypeId displayUnit)
    {
        var unitName = GetUnitDisplayName(displayUnit) ;
        return $"{value:F2} {unitName}" ;
    }

    /// <summary>
    /// Get display name for unit type
    /// </summary>
    public string GetUnitDisplayName(ForgeTypeId unitTypeId)
    {
        return unitTypeId.TypeId switch
        {
            "autodesk.unit.unit:millimeters-1.0.1" => "mm" ,
            "autodesk.unit.unit:centimeters-1.0.1" => "cm" ,
            "autodesk.unit.unit:meters-1.0.1" => "m" ,
            "autodesk.unit.unit:feet-1.0.1" => "ft" ,
            "autodesk.unit.unit:inches-1.0.1" => "in" ,
            _ => unitTypeId.ToString()!
        } ;
    }
}

