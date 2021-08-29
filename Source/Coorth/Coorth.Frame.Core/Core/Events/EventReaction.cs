﻿using System;
using System.Threading.Tasks;

namespace Coorth {

    public interface IEventReaction : IDisposable {
        EventId ProcessId { get; }
        float Priority { get; }
        Type EventType { get; }
    }

    public interface IEventReaction<T> : IEventReaction where T : IEvent {
        void Setup(EventChannel<T> channel);
        void Execute(in T e);
        Task ExecuteAsync(in T e);
    }

    public abstract class EventReaction<T> : Disposable, IEventReaction<T> where T : IEvent {
        private EventChannel<T> channel;

        public EventId ProcessId { get; } = EventId.New();

        public float Priority { get; } = 0;
        
        public Type EventType => typeof(T);
        
        public void Setup(EventChannel<T> eventChannel) {
            this.channel = eventChannel;
        }

        public abstract void Execute(in T e);

        public abstract Task ExecuteAsync(in T e);
        
        protected override void Dispose(bool dispose) {
            channel.Remove(this.ProcessId);
        }
    }
    
    public class EventReactionSync<T> : EventReaction<T> where T : IEvent {

        public Action<T> Action;

        public override void Execute(in T e) => Action(e);

        public override Task ExecuteAsync(in T e) {
            Action(e);
            return Task.CompletedTask;
        }

    }
    
    public class EventReactionAsync<T> : EventReaction<T> where T : IEvent {

        public Func<T, Task> Action;
        
        public override void Execute(in T e) => Action(e);

        public override Task ExecuteAsync(in T e) => Action(e);
    }
}