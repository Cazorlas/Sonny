using System.Globalization ;

namespace Sonny.ResourceManager ;

/// <summary>
///     Event arguments for culture changed event
/// </summary>
public class CultureChangedEventArgs : EventArgs
{
    /// <summary>
    ///     Initializes a new instance of CultureChangedEventArgs
    /// </summary>
    /// <param name="culture">The new culture</param>
    public CultureChangedEventArgs(CultureInfo culture) => Culture = culture ;

    /// <summary>
    ///     Gets the new culture
    /// </summary>
    public CultureInfo Culture { get ; }
}
