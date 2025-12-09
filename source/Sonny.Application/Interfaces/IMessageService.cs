namespace Sonny.Application.Interfaces ;

/// <summary>
///     Interface for displaying messages to users
/// </summary>
public interface IMessageService
{
    /// <summary>
    ///     Shows an error message with title
    /// </summary>
    /// <param name="title">The title of the error message</param>
    /// <param name="message">The error message content</param>
    void ShowError(string title,
        string message) ;

    /// <summary>
    ///     Shows an error message with default title
    /// </summary>
    /// <param name="message">The error message content</param>
    void ShowError(string message) ;

    /// <summary>
    ///     Shows an information message with title
    /// </summary>
    /// <param name="title">The title of the information message</param>
    /// <param name="message">The information message content</param>
    void ShowInfo(string title,
        string message) ;

    /// <summary>
    ///     Shows an information message with default title
    /// </summary>
    /// <param name="message">The information message content</param>
    void ShowInfo(string message) ;

    /// <summary>
    ///     Shows a warning message with title
    /// </summary>
    /// <param name="title">The title of the warning message</param>
    /// <param name="message">The warning message content</param>
    void ShowWarning(string title,
        string message) ;

    /// <summary>
    ///     Shows a warning message with default title
    /// </summary>
    /// <param name="message">The warning message content</param>
    void ShowWarning(string message) ;

    /// <summary>
    ///     Shows a question message with title
    /// </summary>
    /// <param name="title">The title of the question message</param>
    /// <param name="message">The question message content</param>
    /// <returns>True if user clicks Yes, False if user clicks No</returns>
    bool ShowQuestion(string title,
        string message) ;

    /// <summary>
    ///     Shows a question message with default title
    /// </summary>
    /// <param name="message">The question message content</param>
    /// <returns>True if user clicks Yes, False if user clicks No</returns>
    bool ShowQuestion(string message) ;
}
