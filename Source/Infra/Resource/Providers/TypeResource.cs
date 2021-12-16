using System;

namespace Coorth {
    public class TypeResource : IResourceProvider<Type> {

        public Type GetResource(string name) {
            return Type.GetType(name);
        }

        public Type GetResource(Guid guid) {
            return TypeBinding.GetType(guid);
        }
        
        public string GetName(Type resource) {
            return resource.FullName;
        }
        
        public Guid GetGuid(Type resource) {
            return TypeBinding.GetGuid(resource);
        }
    }
}