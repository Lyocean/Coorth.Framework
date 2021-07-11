using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coorth {
    public abstract class EventChannel {

        public EventId ProcessId { get; } = EventId.New();
        
        public readonly EventNode Node;

        public EventChannel(EventNode node) {
            this.Node = node;
        }
    }
    
    public class EventChannel<T> : EventChannel,  IEventProcess<T> where T : IEvent {

        private readonly List<IEventReaction<T>> reactions = new List<IEventReaction<T>>();
                
        public EventChannel(EventNode node) : base(node) {
            
        }
        
        internal IEventReaction<T> Subscribe(IEventReaction<T> reaction) {
            reactions.Add(reaction);
            // int position = reactions.Count - 1;
            // for (var i = reactions.Count - 1; i >= 0; i--) {
            //     if (reaction.Priority > reactions[i].Priority) {
            //         continue;
            //     }
            //     position = i;
            //     break;
            // }
            // reactions.Insert(position, reaction);
            return reaction;
        }
        
        public IEventReaction<T> Subscribe(Action<T> action) {
            var reaction = new EventReaction<T>();
            reaction.Setup(this);
            reaction.Action = action;
            Subscribe(reaction);
            return reaction;
        }
        
        public IEventReaction<T> Subscribe(Func<T, ValueTask> action) {
            var reaction = new EventReaction<T>();
            reaction.Setup(this);
            reaction.Action = e => action(e).ConfigureAwait(true);
            Subscribe(reaction);
            return reaction;
        }

        public bool Remove(EventId id) {
            for (var i = 0; i < reactions.Count; i++) {
                if (reactions[i].ProcessId == id) {
                    reactions.RemoveAt(i);
                    break;
                }
            }
            return false;
        }

        public void Execute(in T e) {
            for (var i = 0; i < reactions.Count; i++) { 
                reactions[i].Execute(e);
            }
        }
    }
}