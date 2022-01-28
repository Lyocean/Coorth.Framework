using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Coorth {
    public interface IEventReaction : IDisposable {
        EventId Id { get; }
        Type EventType { get; }
    }

    public interface IEventReaction<T> : IEventReaction, ISetup<EventChannel<T>> {
        void Execute(in T e);
        ValueTask ExecuteAsync(T e);
        ValueTask ExecuteAsync(in T e);
    }
    
    public abstract class EventReaction<T> : Disposable, IEventReaction<T> {
        
        private EventChannel<T> channel;

        public EventId Id { get; } = EventId.New();
        
        public Type EventType => typeof(T);

        protected EventReaction(EventChannel<T> eventChannel) {
            this.channel = eventChannel;
        }
        
        protected EventReaction() {
            this.channel = null;
        }
        
        public void OnSetup(EventChannel<T> eventChannel) {
            this.channel?.Remove(Id);
            this.channel = eventChannel;
        }

        public abstract void Execute(in T e);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ValueTask ExecuteAsync(in T e) => ExecuteAsync(e);

        public abstract ValueTask ExecuteAsync(T e);

        protected override void OnDispose(bool dispose) {
            channel.Remove(this.Id);
            channel = null;
        }


    }
}