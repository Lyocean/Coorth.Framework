using System;
using System.Collections.Generic;

namespace Coorth {
    public class SystemActions : SystemBase {

        private readonly Dictionary<EventId, ISystemReaction> id2Reactions = new Dictionary<EventId, ISystemReaction>();
        
        public EventId Add<TEvent>(Action<TEvent> action) where TEvent: IEvent {
            var reaction = CreateReaction<TEvent>();
            reaction.OnEvent((in TEvent e)=> action(e));
            id2Reactions.Add(reaction.ProcessId, reaction);
            return reaction.ProcessId;
        }
                
        public bool Remove(EventId id) {
            if (id2Reactions.TryGetValue(id, out var reaction)) {
                reaction.Dispose();
                return true;
            }
            return false;
        }
    }
}