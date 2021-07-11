using System;
using System.Threading.Tasks;

namespace Coorth {
    public interface IEventProcess {
        EventId ProcessId { get; }
    }

    public interface IEventProcess<T> : IEventProcess where T : IEvent {
        void Execute(in T e);
    }
    
    public interface IEventProcessAsync<in T> : IEventProcess where T : IEvent {
        ValueTask ExecuteAsync(T e);
    }

    public class EventProcess<T> : IEventProcess<T> where T : IEvent{
        public EventId ProcessId { get; } = EventId.New();

        private readonly Action<T> action;

        public EventProcess(Action<T> action) {
            this.action = action;
        }

        public void Execute(in T e) {
            this.action(e);
        }
    }
    
    public class EventProcessAsync<T> : IEventProcessAsync<T> where T : IEvent{
        public EventId ProcessId { get; } = EventId.New();
        private readonly Func<T, ValueTask> action;

        public EventProcessAsync(Func<T, ValueTask> action) {
            this.action = action;
        }
        
        public ValueTask ExecuteAsync(T e) {
            return this.action(e);
        }

    }
}