namespace Sonny.Application.Features.ColumnFromCad.Models ;

public class ColumnFromCadSettings
{
    public string? SelectedLayer { get ; set ; }
    public bool IsModelByHatch { get ; set ; } = true ;
    public string? RectangularColumnFamilyId { get ; set ; }
    public string? CircularColumnFamilyId { get ; set ; }
    public string? WidthParameter { get ; set ; }
    public string? HeightParameter { get ; set ; }
    public string? DiameterParameter { get ; set ; }
    public string? BaseLevelId { get ; set ; }
    public string? TopLevelId { get ; set ; }
    public double BaseOffsetDisplay { get ; set ; }
    public double TopOffsetDisplay { get ; set ; }
}
