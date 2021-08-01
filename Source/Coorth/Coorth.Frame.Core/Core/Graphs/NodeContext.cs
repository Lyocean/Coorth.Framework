using System;
using System.Collections.Generic;

namespace Coorth {
    public class NodeValue<T> where T : struct {
        public T Value;
    }
    
    public class NodeContext {
        
        private readonly Dictionary<object, object> children = new Dictionary<object, object>();
        
        protected object owner;

        public T GetOwner<T>() where T: class => owner as T;

        public void Awake(object ownerObj, NodeGraph graph) {
            this.owner = ownerObj;
        }
        
        public TData GetData<TData>(object key)  {
            if (children.TryGetValue(key, out var data)) {
                return (TData) data;
            }
            var result = Activator.CreateInstance<TData>();
            children[key] = result;
            return result;
        }

        public ref TData RefData<TData>(object key) where TData : struct {
            if (!children.TryGetValue(key, out var data)) {
                data = new NodeValue<TData>() {Value = default};
                children[key] = data;
            }
            var value = (NodeValue<TData>)data;
            return ref value.Value;
        }
        
        public void OnGraphEnter(NodeGraph graph) { }

        public void OnGraphExit(NodeGraph graph) { }

        public TData GetData<TData>(NodeBase node) where TData: new() => GetData<TData>((object)node);

        public TData GetData<TData>(NodeGraph node) where TData: new() => GetData<TData>((object)node);
    }
}
