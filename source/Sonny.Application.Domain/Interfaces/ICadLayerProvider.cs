namespace Sonny.Application.Domain.Interfaces ;

/// <summary>
///     Interface for getting layer names from CAD link
/// </summary>
public interface ICadLayerProvider
{
    /// <summary>
    ///     Gets all layer names from CAD link
    /// </summary>
    /// <param name="cadLink">CAD link import instance</param>
    /// <param name="includeHidden">Whether to include hidden layers</param>
    /// <returns>Set of layer names</returns>
    HashSet<string> GetAllLayerNames(ImportInstance cadLink,
        bool includeHidden = false) ;
}

