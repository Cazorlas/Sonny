namespace Sonny.Application.Domain.Entities.Settings.Models ;

/// <summary>
///     Represents a unit option for display in settings
/// </summary>
public class UnitOption
{
    /// <summary>
    ///     Initializes a new instance of UnitOption
    /// </summary>
    /// <param name="displayName">Display name (e.g., "Millimeters", "Centimeters")</param>
    /// <param name="displayUnit">Display unit enum</param>
    public UnitOption(string displayName,
        AppDisplayUnit displayUnit)
    {
        DisplayName = displayName ;
        DisplayUnit = displayUnit ;
    }

    /// <summary>
    ///     Display name for the unit
    /// </summary>
    public string DisplayName { get ; }

    /// <summary>
    ///     Display unit enum
    /// </summary>
    public AppDisplayUnit DisplayUnit { get ; }

    public override string ToString() => DisplayName ;
}
