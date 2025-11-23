using EasyRibbon.UIAttributeBase;
using SonnyApplication.Features.AutoColumnDimension.Commands;

namespace SonnyApplication.Ribbon;

/// <summary>
///     Main ribbon tab for Sonny application
/// </summary>
[Tab("Sonny")]
public class SonnyTab
{
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
            typeof(AutoColumnDimensionCommand),
            Image = "/SonnyApplication;component/Resources/Icons/AutoColumnDimension16.png",
            LargeImage = "/SonnyApplication;component/Resources/Icons/AutoColumnDimension32.png",
            ToolTip = "Automatically create dimensions for columns",
            LongDescription = "This tool automatically creates dimensions for columns based on grids")]
        public class AutoColumnDimensionButton;
    }
}

