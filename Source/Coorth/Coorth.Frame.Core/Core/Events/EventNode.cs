using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coorth {
    
    public abstract class EventNode : Disposable {

        private static int currentNodeIndex = 0;
        
        public readonly int NodeIndex = currentNodeIndex++;

        public EventId ProcessId { get; } = EventId.New();

        private EventNode parent;

        private readonly DictList<EventId, EventNode> children = new DictList<EventId, EventNode>(4);

        private readonly Dictionary<Type, EventChannel> channels = new Dictionary<Type, EventChannel>();
        
        protected override void Dispose(bool dispose) {
            foreach (EventNode node in children.List) {
                node.Dispose();
            }            
            children.Clear();
        }
        
        public void AddChild(EventNode node) {
            node.parent = this;
            children.Add(node.ProcessId, node);
        }

        public void RemoveChild(EventNode node) {
            node.parent = null;
            children.Remove(node.ProcessId);
        }

        private EventChannel<T> OfferChannel<T>() where T: IEvent {
            var key = typeof(T);
            if (channels.TryGetValue(key, out var channel)) {
                return (EventChannel<T>)channel;
            }
            channel = new EventChannel<T>(this);
            channels.Add(key, channel);
            return (EventChannel<T>)channel;
        }

        public void Subscribe<T>(Action<T> action)  where T: IEvent {
            OfferChannel<T>().Subscribe(action);
        }
        
        public void Subscribe<T>(IEventReaction<T> reaction) where T: IEvent {
            OfferChannel<T>().Subscribe(reaction);
        }
        

        public void Execute<T>(in T e) where T: IEvent {
            if (channels.TryGetValue(typeof(T), out var channel)) {
                ((EventChannel<T>)channel).Execute(e);
            }
            
            foreach (EventNode node in children.List) {
                node.Execute<T>(e);
            }
        }
    }
}