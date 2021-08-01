using System;

namespace Coorth {
    [Serializable]
    public struct StoreType {
        
        public string Guid;
        
        private Type type;
        public Type Type => type != null ? type : (type = Resolve());

        public Type Resolve() => TypeUtil.TryGetTypeByGuid(Guid, out type) ? type : null;
        
        public StoreType(string guid) {
            this.Guid = guid;
            this.type = null;
        }
    }
}