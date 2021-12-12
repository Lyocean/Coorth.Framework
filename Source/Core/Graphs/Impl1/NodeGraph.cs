using System.Collections.Generic;


namespace Coorth {
    public abstract class NodeGraph : NodeAction {
        
        #region Graph-Node

        public NodeGraph ParentGraph => Graph;

        public NodeGraph RootGraph => ParentGraph != null ? ParentGraph.RootGraph : this;

        private readonly List<NodeGraph> children = new List<NodeGraph>();
        public IReadOnlyList<NodeGraph> Children => children;

        protected Dictionary<int, NodeBase> nodes = new Dictionary<int, NodeBase>();
        
        public virtual void OnNodeAttach(NodeBase node) {
            nodes.Add(node.Id, node);
            if (node.Action is NodeGraph childGraph) {
                children.Remove(childGraph);
                children.Add(childGraph);
            }
            node.OnAttach(this);
        }
        
        public virtual void OnNodeDetach(NodeBase node) {
            node.OnDetach(this);
            if (node.Action is NodeGraph childGraph) {
                children.Remove(childGraph);
            }
            nodes.Remove(node.Id);
        }

        public virtual void OnConnectAttach(NodeConnect connect) {
            connect.OnAttach(this);
        }

        public virtual void OnConnectDetach(NodeConnect connect) {
            connect.OnDetach(this);
        }

        #endregion

        #region Behaviour
        
        public override void Awake(NodeBase node) {
            base.Awake(node);
            foreach (var pair in nodes) {
                pair.Value.Awake();
            }
        }

        public override bool Enter(NodeContext context) {
            context.OnGraphEnter(this);
            return true;
        }

        public override NodeStatus Execute(NodeContext context, EventNodeTick e) {
            return NodeStatus.Success;
        }

        public override void Exit(NodeContext context) {
            context.OnGraphExit(this);
        }

        public override void Destroy() {
            base.Destroy();
            foreach (var pair in nodes) {
                pair.Value.Destroy();
            }
        }

        #endregion
    }

    public abstract class NodeGraph<TData> : NodeGraph {
        public override bool Enter(NodeContext context) {
            context.OnGraphEnter(this);
            var data = context.GetData<TData>(this);
            return OnEnter(context, ref data);
        }

        protected virtual bool OnEnter(NodeContext context, ref TData self) {
            return true;
        }
        
        public override NodeStatus Execute(NodeContext context, EventNodeTick e) {
            var data = context.GetData<TData>(this);
            return OnExecute(context, e, ref data);
        }

        protected virtual NodeStatus OnExecute(NodeContext context, EventNodeTick frame, ref TData self) {
            return NodeStatus.Success;
        }

        public override void Exit(NodeContext context) {
            context.OnGraphExit(this);
            var data = context.GetData<TData>(this);
            OnExit(context, ref data);
        }
        
        protected virtual void OnExit(NodeContext context, ref TData self) { }

    }
    
}
