using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coorth {
    public class EventChannel_Collection<T> : EventReaction<T> {
        
        public Dictionary<T, List<IEventReaction<T>>> Reactions = new Dictionary<T, List<IEventReaction<T>>>();
        
        public EventChannel_Collection(EventChannel<T> channel) : base(channel) { }
        
        public override void Execute(in T e) {
            if (!Reactions.TryGetValue(e, out var list)) {
                return;
            }
            foreach (IEventReaction<T> reaction in list) {
                reaction.Execute(in e);
            }
        }

        public override async ValueTask ExecuteAsync(T e) {
            if (!Reactions.TryGetValue(e, out var list)) {
                return;
            }
            foreach (IEventReaction<T> reaction in list) {
                await reaction.ExecuteAsync(in e);
            }
        }
        
    }
}