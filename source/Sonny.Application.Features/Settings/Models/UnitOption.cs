namespace Sonny.Application.Features.Settings.Models ;

/// <summary>
///     Represents a unit option for display in settings
/// </summary>
public class UnitOption
{
    /// <summary>
    ///     Initializes a new instance of UnitOption
    /// </summary>
    /// <param name="displayName">Display name (e.g., "Millimeters", "Centimeters")</param>
    /// <param name="unitTypeId">ForgeTypeId for the unit</param>
    public UnitOption(string displayName,
        ForgeTypeId unitTypeId)
    {
        DisplayName = displayName ;
        UnitTypeId = unitTypeId ;
    }

    /// <summary>
    ///     Display name for the unit
    /// </summary>
    public string DisplayName { get ; }

    /// <summary>
    ///     ForgeTypeId for the unit
    /// </summary>
    public ForgeTypeId UnitTypeId { get ; }

    /// <summary>
    ///     String representation for display
    /// </summary>
    public override string ToString() => DisplayName ;
}
