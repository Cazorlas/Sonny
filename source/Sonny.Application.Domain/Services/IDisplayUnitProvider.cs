using Sonny.Application.Domain.Entities.Settings ;

namespace Sonny.Application.Domain.Services ;

/// <summary>
///     Provides default display unit based on document system
/// </summary>
public interface IDisplayUnitProvider
{
    /// <summary>
    ///     Gets default display unit (metric or imperial)
    /// </summary>
    /// <returns>Default display unit</returns>
    AppDisplayUnit GetDefaultDisplayUnit() ;
}
