namespace Sonny.Application.Core.Interfaces ;

/// <summary>
///     Interface for unit conversion operations
/// </summary>
public interface IUnitConverter
{
    /// <summary>
    ///     Convert value from display unit to internal unit (feet)
    /// </summary>
    /// <param name="value">Value in display unit</param>
    /// <param name="displayUnit">Display unit type</param>
    /// <returns>Value in feet (Revit internal unit)</returns>
    double ToInternalUnit(double value,
        ForgeTypeId displayUnit) ;

    /// <summary>
    ///     Convert value from internal unit (feet) to display unit
    /// </summary>
    /// <param name="value">Value in feet</param>
    /// <param name="displayUnit">Display unit type</param>
    /// <returns>Value in display unit</returns>
    double FromInternalUnit(double value,
        ForgeTypeId displayUnit) ;

    /// <summary>
    ///     Get default display unit based on document settings or user preference
    /// </summary>
    /// <param name="document">Revit document</param>
    /// <returns>Default display unit type</returns>
    ForgeTypeId GetDefaultDisplayUnit(Document document) ;

    /// <summary>
    ///     Format value with unit suffix for display
    /// </summary>
    /// <param name="value">Value in display unit</param>
    /// <param name="displayUnit">Display unit type</param>
    /// <returns>Formatted string with unit suffix</returns>
    string FormatWithUnit(double value,
        ForgeTypeId displayUnit) ;

    /// <summary>
    ///     Get display name for unit type (e.g., "mm", "cm", "ft")
    /// </summary>
    /// <param name="displayUnit">Display unit type</param>
    /// <returns>Unit display name</returns>
    string GetUnitDisplayName(ForgeTypeId displayUnit) ;
}
