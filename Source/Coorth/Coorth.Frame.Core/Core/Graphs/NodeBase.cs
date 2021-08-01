using System;

namespace Coorth {
    [Serializable, StoreContract("408d7b8f-f157-4a55-af8d-9c61d08a5b9d")]
    public abstract class NodeBase {
        
        public int Id;
        
        public int Index => Id;
        
        public NodeGraph Graph { get; private set; }

        public INodeAction Action;

        public void Bind(INodeAction action) {
            this.Action = action;
        }
        
        public virtual void OnAttach(NodeGraph graph) {
            this.Graph = graph;
        }

        public virtual void OnDetach(NodeGraph graph) {
            this.Graph = null;
        }
        
        public virtual void Awake() {
            Action?.Awake(this);
        }
        
        public virtual void Destroy() {
            
        }
    }



    public abstract class NodeConnect {

        public NodeGraph Graph { get; private set; }

        public readonly int Source;
        
        public readonly int Target;
        
        public NodeConnect(int source, int target) {
            this.Source = source;
            this.Target = target;
        }
        
        public virtual void OnAttach(NodeGraph graph) {
            this.Graph = graph;
        }

        public virtual void OnDetach(NodeGraph graph) {
            this.Graph = null;
        }
    }
}