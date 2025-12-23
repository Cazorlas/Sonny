namespace Sonny.Application.Domain.Interfaces ;

/// <summary>
///     Interface for accessing localized resources
/// </summary>
public interface IResourceHelper
{
    /// <summary>
    ///     Get localized string from Application.Resources
    /// </summary>
    /// <param name="key">Resource key</param>
    /// <returns>Localized string or key if not found</returns>
    string GetString(string key) ;

    /// <summary>
    ///     Get localized string with format arguments
    /// </summary>
    /// <param name="key">Resource key</param>
    /// <param name="args">Format arguments</param>
    /// <returns>Formatted localized string</returns>
    string GetString(string key,
        params object[] args) ;
}

