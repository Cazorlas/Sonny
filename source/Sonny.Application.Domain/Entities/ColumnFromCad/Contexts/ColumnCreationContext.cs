using Sonny.Application.Domain.Entities.ColumnFromCad.Models ;

namespace Sonny.Application.Domain.Entities.ColumnFromCad.Contexts ;

public class ColumnCreationContext
{
    public ColumnFromCadSettings Settings { get ; set ; } = null! ;

    /// <summary>
    ///     Base offset in feet (internal unit)
    /// </summary>
    public double BaseOffset { get ; set ; }

    /// <summary>
    ///     Top offset in feet (internal unit)
    /// </summary>
    public double TopOffset { get ; set ; }
}
