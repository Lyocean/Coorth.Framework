using System.Threading.Tasks;

namespace Coorth {

    public interface INodeAction {

        void Awake(NodeBase node);

        void Destroy();

        void Build(NodeContext context);
        
        void Clear(NodeContext context);

        
        /// <returns>IsRunning</returns>
        bool Enter(NodeContext context);
        
        NodeStatus Execute(NodeContext context, EventNodeTick e);

        void Exit(NodeContext context);

    }

    public abstract class NodeAction : INodeAction {

        protected NodeBase Node { get; private set; }

        protected NodeGraph Graph => Node?.Graph;


        public virtual void Awake(NodeBase node) {
            this.Node = node;
        }

        public virtual void Destroy() {
        }
        
        public virtual void Build(NodeContext context) {
            
        }
        
        public virtual void Clear(NodeContext context) {
            
        }
        
        public virtual bool Enter(NodeContext context) {
            return true;
        }
        
        public virtual NodeStatus Execute(NodeContext context, EventNodeTick e) {
            return NodeStatus.Success;
        }

        public virtual void Exit(NodeContext context) {
            
        }


    }


    public class AsyncActionData {
        public NodeStatus Status;
        public Task Task;
        public EventNodeTick Tick;
    }

    public abstract class NodeActionAsync : NodeAction {
        
        public override bool Enter(NodeContext context) {
            var data = context.GetData<AsyncActionData>(this);
            data.Status = NodeStatus.Success;
            return true;
        }
        
        public override NodeStatus Execute(NodeContext context, EventNodeTick e) {
            var data = context.GetData<AsyncActionData>(this);
            if (data.Task == null) {
                data.Tick = e;
                data.Task = OnExecute(context, data);
            }
            else {
                data.Tick = e;
            }
            if (data.Task.IsCompleted) {
                data.Status = data.Task.IsFaulted ? NodeStatus.Failure : NodeStatus.Success;
            }
            else {
                data.Status = NodeStatus.Running;
            }
            return data.Status;
        }
        
        protected virtual Task OnExecute(NodeContext context, AsyncActionData data) {
            return Task.CompletedTask;
        }
    }
    
    public interface INodeCondition {
        bool Check<TEvent>(NodeContext context, in TEvent evt);
    }
}