﻿using System;
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
    
    public class EventChannel<T> : EventChannel,  IEventProcess<T> {

        private readonly List<IEventReaction<T>> reactions = new List<IEventReaction<T>>();
        
        public EventChannel(EventNode node) : base(node) {
            
        }
        
        internal IEventReaction<T> Subscribe(IEventReaction<T> reaction) {
            reactions.Add(reaction);
            reaction.Setup(this);
            return reaction;
        }
        
        public IEventReaction<T> Subscribe(Action<T> action) {
            var reaction = new EventReactionSync<T> { Action = action };
            return Subscribe(reaction);
        }
        
        public IEventReaction<T> Subscribe(Func<T, Task> action) {
            var reaction = new EventReactionAsync<T> { Action = action };
            return Subscribe(reaction);
        }

        public bool Remove(EventId id) {
            for (var i = 0; i < reactions.Count; i++) {
                if (reactions[i].ProcessId == id) {
                    reactions.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        
        public void Dispatch(in T e) {
            for (var i = 0; i < reactions.Count; i++) {
                try {
                    reactions[i].Execute(e);
                }
                catch (Exception exception) {
                    LogUtil.Exception(exception);
                    throw;
                }
            }
        }
        
        public async Task DispatchAsync(T e) {
            for (var i = 0; i < reactions.Count; i++) { 
                try {
                    await reactions[i].ExecuteAsync(e);
                }
                catch (Exception exception) {
                    LogUtil.Exception(exception);
                    throw;
                }
            }
        }
    }
}