using System;

namespace Coorth {

    public interface IEventReaction : IDisposable {

        EventId ProcessId { get; }

        float Priority { get; }

        Type EventType { get; }
    }

    public interface IEventReaction<T> : IEventReaction where T : IEvent {
        void Setup(EventChannel<T> channel);
        void Execute(in T e);
    }

    public class EventReaction<T> : Disposable, IEventReaction<T> where T : IEvent {

        private EventChannel<T> channel;

        public EventId ProcessId { get; } = EventId.New();

        public float Priority { get; } = 0;
        
        public Type EventType => typeof(T);

        public Action<T> Action;

        public void Setup(EventChannel<T> eventChannel) {
            this.channel = eventChannel;
        }

        public void Execute(in T e) {
            Action?.Invoke(e);
        }

        protected override void Dispose(bool dispose) {
            channel.Remove(this.ProcessId);
        }
    }
}