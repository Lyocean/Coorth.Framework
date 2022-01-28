using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coorth {
    public abstract class EventChannel {
        
        public EventId ChannelId { get; } = EventId.New();

        public readonly EventNode Node;

        protected EventChannel(EventNode node) {
            this.Node = node;
        }
    }

    public class EventChannel<T> : EventChannel {
        
        private readonly List<IEventReaction<T>> reactions = new List<IEventReaction<T>>();
        public IReadOnlyList<IEventReaction<T>> Reactions => reactions;
        
        public EventChannel(EventNode node) : base(node) {
        }

        private IEventReaction<T> _Subscribe(IEventReaction<T> reaction) {
            reactions.Add(reaction);
            return reaction;
        }
        
        public IEventReaction<T> Subscribe(IEventReaction<T> reaction) {
            reaction.OnSetup(this);
            return _Subscribe(reaction);
        }
  
        public IEventReaction<T> Subscribe(Action<T> action) {
            var reaction = new EventReaction_EventAction<T>(this, action);
            return _Subscribe(reaction);
        }
        
        public IEventReaction<T> Subscribe(EventAction<T> action) {
            var reaction = new EventReaction_SystemAction<T>(this, action);
            return _Subscribe(reaction);
        }
        
        public IEventReaction<T> Subscribe(Func<T, Task> action) {
            var reaction = new EventReaction_SystemFunc<T>(this, action);
            return _Subscribe(reaction);
        }

        public IEventReaction<T> Subscribe(Func<T, ValueTask> action) {
            var reaction = new EventReaction_SystemFunc2<T>(this, action);
            return _Subscribe(reaction);
        }
        
        public IEventReaction<T> Subscribe(EventFunc<T, Task> action) {
            var reaction = new EventReaction_EventFunc<T>(this, action);
            return _Subscribe(reaction);
        }
        
        public IEventReaction<T> Subscribe(EventFunc<T, ValueTask> action) {
            var reaction = new EventReaction_EventFunc2<T>(this, action);
            return _Subscribe(reaction);
        }
        
        public bool Remove(EventId id) {
            for (var i = 0; i < reactions.Count; i++) {
                if (reactions[i].Id == id) {
                    reactions.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}