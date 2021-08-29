using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Coorth {
    public interface IEventNode : IDisposable {
        EventId ProcessId { get; }
        IEventNode Parent { get; set; }
        void Execute<T>(in T e) where T : IEvent;
        Task ExecuteAsync<T>(T e) where T : IEvent;
    }
    
    public abstract class EventNode : Disposable, IEventNode {

        public EventId ProcessId { get; } = EventId.New();

        public IEventNode Parent { get; set; }

        private readonly DictList<EventId, IEventNode> children = new DictList<EventId, IEventNode>(4);

        private readonly Dictionary<Type, EventChannel> channels = new Dictionary<Type, EventChannel>();
        
        protected override void Dispose(bool dispose) {
            foreach (var node in children.List) {
                node.Dispose();
            }            
            children.Clear();
        }
        
        public void AddChild(IEventNode node) {
            node.Parent = this;
            children.Add(node.ProcessId, node);
        }

        public void RemoveChild(IEventNode node) {
            node.Parent = null;
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

        public IEventReaction<T> Subscribe<T>(Action<T> action)  where T: IEvent {
            return OfferChannel<T>().Subscribe(action);
        }
        
        public IEventReaction<T> Subscribe<T>(Func<T, Task> action)  where T: IEvent {
            return OfferChannel<T>().Subscribe(action);
        }

        
        public IEventReaction<T> Subscribe<T>(IEventReaction<T> reaction) where T: IEvent {
            return OfferChannel<T>().Subscribe(reaction);
        }

#if NET5_0_OR_GREATER
         [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#else
         [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Execute<T>(in T e) where T: IEvent {
            if (channels.TryGetValue(typeof(T), out var channel)) {
                ((EventChannel<T>)channel).Execute(e);
            }

            var list = children.List;
            for (var i = 0; i < list.Count; i++) {
                var node = list[i];
                node.Execute<T>(e);
            }

        }
        
        public async Task ExecuteAsync<T>(T e) where T: IEvent {
            if (channels.TryGetValue(typeof(T), out var channel)) {
                await ((EventChannel<T>)channel).ExecuteAsync(e);
            }

            var list = children.List;
            for (var i = 0; i < list.Count; i++) {
                var node = list[i];
                await node.ExecuteAsync<T>(e);
            }
        }

    }
}