using System;
using System.Collections.Generic;


namespace Coorth {
    
    public interface ISystemReaction : IEventReaction {

        IReadOnlyCollection<Type> IncludeComponents { get; }

        IReadOnlyCollection<Type> ExcludeComponents { get; }

    }
    
    public sealed class SystemReaction<TEvent> : Disposable, IEventReaction<TEvent>, ISystemReaction where TEvent : IEvent {
        
        public EventId ProcessId { get; } = EventId.New();

        private EventChannel<TEvent> channel;
        
        public float Priority { get; } = 0;
        
        public Type EventType => typeof(TEvent);

        private readonly SystemBase system;

        private Sandbox Sandbox => system.Sandbox;

        private EventDispatcher Dispatcher => Sandbox.Dispatcher;

        public Action<TEvent> Action { get; private set; }

        private HashSet<Type> includes;
        
        private HashSet<Type> excludes;

        public IReadOnlyCollection<Type> IncludeComponents => (IReadOnlyCollection<Type>)includes ?? Array.Empty<Type>();
        
        public IReadOnlyCollection<Type> ExcludeComponents => (IReadOnlyCollection<Type>)excludes ?? Array.Empty<Type>();
        
        public SystemReaction(SystemBase system) {
            this.system = system;
        }

        public void OnEvent(Action<TEvent> action) {
            this.Action = action;
        }

        public void Include(Type type) {
            if (includes == null) {
                includes = new HashSet<Type>();
            }
            includes.Add(type);
        }

        public void Include<T>() where T : IComponent => Include(typeof(T));
        
        public void Exclude(Type type) {
            if (excludes == null) {
                excludes = new HashSet<Type>();
            }
            excludes.Add(type);
        }
        
        public void Exclude<T>() where T : IComponent => Exclude(typeof(T));

        
        public void Setup(EventChannel<TEvent> eventChannel) {
            this.channel = channel;
        }

        public void Execute(in TEvent e) {
            Action?.Invoke(e);
        }

        protected override void Dispose(bool dispose) {
            channel.Remove(ProcessId);
            system.RemoveReaction(this);
        }
    }
}