using System;
using System.Threading.Tasks;

namespace Coorth {
    public class EventReaction_SystemAction<T> : EventReaction<T> {
        
        public readonly EventAction<T> Action;

        public EventReaction_SystemAction(EventChannel<T> channel, EventAction<T> action) : base(channel) {
            this.Action = action;
        }

        public override void Execute(in T e) {
            Action(in e);
        }

        public override ValueTask ExecuteAsync(T e) {
            Action(e);
            return Task.CompletedTask.ToValueTask();
        }
    }
    
    public class EventReaction_EventAction<T> : EventReaction<T> {

        public readonly Action<T> Action;

        public EventReaction_EventAction(EventChannel<T> channel, Action<T> action) : base(channel) {
            this.Action = action;
        }

        public override void Execute(in T e) {
            Action(e);
        }

        public override ValueTask ExecuteAsync(T e) {
            Action(e);
            return Task.CompletedTask.ToValueTask();
        }
    }
    
    public class EventReaction_SystemFunc<T> : EventReaction<T> {

        public readonly Func<T, Task> Action;
        
        public EventReaction_SystemFunc(EventChannel<T> channel, Func<T, Task> action) : base(channel) {
            this.Action = action;
        }
        
        public override void Execute(in T e) {
            Action(e);
        }

        public override ValueTask ExecuteAsync(T e) {
            return Action(e).ToValueTask();
        }
    }

    public class EventReaction_SystemFunc2<T> : EventReaction<T> {
        
        public readonly Func<T, ValueTask> Action;
        
        public EventReaction_SystemFunc2(EventChannel<T> channel, Func<T, ValueTask> action) : base(channel) {
            this.Action = action;
        }
        
        public override void Execute(in T e) {
            Action(e).Forget();
        }

        public override ValueTask ExecuteAsync(T e) {
            return Action(e);
        }
    }
    
    public class EventReaction_EventFunc<T> : EventReaction<T> {
        
        public readonly EventFunc<T, Task> Action;
        
        public EventReaction_EventFunc(EventChannel<T> channel, EventFunc<T, Task> action) : base(channel) {
            this.Action = action;
        }
        
        public override void Execute(in T e) {
            Action(e);
        }

        public override ValueTask ExecuteAsync(T e) {
            return Action(e).ToValueTask();
        }
    }
    
    public class EventReaction_EventFunc2<T> : EventReaction<T> {
        
        public readonly EventFunc<T, ValueTask> Action;
        
        public EventReaction_EventFunc2(EventChannel<T> channel, EventFunc<T, ValueTask> action) : base(channel) {
            this.Action = action;
        }
        
        public override void Execute(in T e) {
            Action(e).Forget();
        }

        public override ValueTask ExecuteAsync(T e) {
            return Action(e);
        }
    }
}