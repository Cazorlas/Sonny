namespace Sonny.Application.Features.ColumnFromCad.Contexts ;

public class ColumnCreationContext
{
    public Document Document { get ; set ; }
    public Family SelectedRectangularColumnFamily { get ; set ; }
    public Family SelectedCircularColumnFamily { get ; set ; }
    public string WidthParameter { get ; set ; }
    public string HeightParameter { get ; set ; }
    public string DiameterParameter { get ; set ; }
    public Level BaseLevel { get ; set ; }
    public Level TopLevel { get ; set ; }

    /// <summary>
    ///     Base offset in feet
    /// </summary>
    public double BaseOffset { get ; set ; }

    /// <summary>
    ///     Top offset in feet
    /// </summary>
    public double TopOffset { get ; set ; }

    public Action<int, int>? ProgressCallback { get ; set ; }
}
