using System;

namespace Coorth {
    public interface IResourceNode {
        T GetResource<T>(string path) where T : class;
    }
    
    public interface IResourceProvider<out T> {
        T GetResource(string name);
        Type GetResource(Guid guid);
        string GetName(Type resource);
        Guid GetGuid(Type resource);
    }
}