using System;
using System.Threading.Tasks;

namespace Coorth {
    public interface IEventNode : IDisposable {
        EventId ProcessId { get; }
        IEventNode Parent { get; set; }
        void Dispatch<T>(in T e);
        ValueTask DispatchAsync<T>(T e);
    }
    
    public abstract class EventNode : Disposable, IEventNode {

        public EventId ProcessId { get; } = EventId.New();

        public IEventNode Parent { get; set; }

        private readonly DictList<EventId, IEventNode> children = new DictList<EventId, IEventNode>(4);

        protected override void OnDispose(bool dispose) {
            foreach (var node in children.List) {
                node.Dispose();
            }
            children.Clear();
            if (Parent != null) {
                
            }
        }
        
        public void AddChild(IEventNode node) {
            node.Parent = this;
            children.Add(node.ProcessId, node);
        }

        public void RemoveChild(IEventNode node) {
            node.Parent = null;
            children.Remove(node.ProcessId);
        }

        public virtual void Dispatch<T>(in T e) {
            var list = children.List;
            for (var i = 0; i < list.Count; i++) {
                var node = list[i];
                node.Dispatch<T>(in e);
            }
        }

        public virtual async ValueTask DispatchAsync<T>(T e) {
            var list = children.List;
            for (var i = 0; i < list.Count; i++) {
                var node = list[i];
                await node.DispatchAsync<T>(e);
            }
        }
    }
}