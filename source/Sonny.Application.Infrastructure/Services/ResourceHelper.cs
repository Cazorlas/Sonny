using Sonny.Application.Domain.Interfaces ;

namespace Sonny.Application.Infrastructure.Services ;

public class ResourceHelper : IResourceHelper
{
    public string GetString(string key)
    {
        return ResourceManager.ResourceHelper.GetString(key) ;
    }

    public string GetString(string key,
        params object[] args)
    {
        return ResourceManager.ResourceHelper.GetString(key,
            args) ;
    }
}

