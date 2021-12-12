using System;
using System.Collections.Generic;

namespace Coorth {

    public interface INodeContext {
    
        Blackboard<int> Blackboard { get; }

        void OnGraphEnter(NodeGraph graph);

        void OnGraphExit(NodeGraph graph);

        ref T RefContext<T>(NodeBase node);
    }
    
    
    
    public class NodeValue<T>  {
        public T Value;
    }
    
    public class NodeContext : INodeContext {

        public Blackboard<int> Blackboard { get; } = new Blackboard<int>();

        public ref T RefContext<T>(NodeBase node) {
            return ref Blackboard.Ref<T>(node.Id);
        }
        
        public T GetContext<T>(NodeBase node) {
            return Blackboard.Get<T>(node.Id);
        }

        public void SetContext<T>(NodeBase node, T context) {
            Blackboard.Set<T>(node.Id, context);
        }

        public void RemoveContext<T>(NodeBase node) {
            Blackboard.Remove<T>(node.Id);
        }

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

        public ref TData RefData<TData>(object key) {
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
