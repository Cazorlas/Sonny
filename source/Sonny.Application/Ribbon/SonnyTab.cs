using Sonny.Application.Commands ;
using Sonny.EasyRibbon.UIAttributeBase ;

namespace Sonny.Application.Ribbon ;

/// <summary>
///     Main ribbon tab for Sonny application
/// </summary>
[Tab("Sonny")]
public class SonnyTab
{
    /// <summary>
    ///     Panel for application settings
    /// </summary>
    [Panel("Settings")]
    public class SettingsPanel
    {
        /// <summary>
        ///     Button for application settings
        /// </summary>
        [Button("Settings",
            typeof( SettingsCommand ),
            Image = "/Sonny.Application;component/Resources/Icons/Setting16.png",
            LargeImage = "/Sonny.Application;component/Resources/Icons/Setting32.png",
            ToolTip = "Open application settings",
            LongDescription = "Configure application preferences including display units")]
        public class SettingsButton ;
    }

    /// <summary>
    ///     Panel for dimension tools
    /// </summary>
    [Panel("Dimension Tools")]
    public class DimensionPanel
    {
        /// <summary>
        ///     Button for auto column dimension feature
        /// </summary>
        [Button("Auto Column Dimension",
            typeof( AutoColumnDimensionCommand ),
            Image = "/Sonny.Application;component/Resources/Icons/AutoColumnDimension16.png",
            LargeImage = "/Sonny.Application;component/Resources/Icons/AutoColumnDimension32.png",
            ToolTip = "Automatically create dimensions for columns",
            LongDescription = "This tool automatically creates dimensions for columns based on grids")]
        public class AutoColumnDimensionButton ;
    }
}
