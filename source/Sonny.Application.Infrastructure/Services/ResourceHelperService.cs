using Sonny.Application.UseCases.Interfaces ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Infrastructure.Services ;

/// <summary>
///     Infrastructure implementation of IResourceHelper wrapping static ResourceHelper
/// </summary>
public class ResourceHelperService : IResourceHelper
{
    /// <summary>
    ///     Get localized string from Application.Resources
    /// </summary>
    /// <param name="key">Resource key</param>
    /// <returns>Localized string or key if not found</returns>
    public string GetString(string key)
    {
        return ResourceHelper.GetString(key) ;
    }

    /// <summary>
    ///     Get localized string with format arguments
    /// </summary>
    /// <param name="key">Resource key</param>
    /// <param name="args">Format arguments</param>
    /// <returns>Formatted localized string</returns>
    public string GetString(string key,
        params object[] args)
    {
        return ResourceHelper.GetString(key,
            args) ;
    }
}

