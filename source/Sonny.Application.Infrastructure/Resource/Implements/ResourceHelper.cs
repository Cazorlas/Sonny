using Sonny.Application.Domain.Services ;

namespace Sonny.Application.Infrastructure.Resource.Implements ;

public class ResourceHelper : IResourceHelper
{
    public string GetString(string key) => ResourceManager.ResourceHelper.GetString(key) ;

    public string GetString(string key,
        params object[] args) =>
        ResourceManager.ResourceHelper.GetString(key,
            args) ;
}
