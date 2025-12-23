namespace Sonny.Application.UseCases.ColumnFromCad.Models ;

public abstract class ColumnModel
{
    /// <summary>
    ///     Center point of the column
    /// </summary>
    public XYZ Center { get ; protected set ; }
}
