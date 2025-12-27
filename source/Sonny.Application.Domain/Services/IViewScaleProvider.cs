namespace Sonny.Application.Domain.Services ;

/// <summary>
///     Provides view scale information
/// </summary>
public interface IViewScaleProvider
{
    /// <summary>
    ///     Gets the scale of the active view
    /// </summary>
    /// <returns>View scale</returns>
    double GetActiveViewScale() ;
}
