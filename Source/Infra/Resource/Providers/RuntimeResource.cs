using System;

namespace Coorth {
    public class RuntimeResource: IResourceProvider<object> {
        
        public object GetResource(string name) {
            throw new NotImplementedException();
        }

        public Type GetResource(Guid guid) {
            throw new NotImplementedException();
        }

        public string GetName(Type resource) {
            throw new NotImplementedException();
        }

        public Guid GetGuid(Type resource) {
            throw new NotImplementedException();
        }
    }
}