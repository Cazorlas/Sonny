namespace Sonny.Application.Domain.Services ;

/// <summary>
///     Interface for reporting progress of long-running operations
/// </summary>
public interface IProgressReporter
{
    /// <summary>
    ///     Shows the progress window with the specified title
    /// </summary>
    /// <param name="title">Title to display on progress window</param>
    void Show(string title) ;

    /// <summary>
    ///     Updates the progress indicator
    /// </summary>
    /// <param name="current">Current progress value</param>
    /// <param name="total">Total progress value</param>
    void Update(int current,
        int total) ;

    /// <summary>
    ///     Closes the progress window
    /// </summary>
    void Close() ;
}
