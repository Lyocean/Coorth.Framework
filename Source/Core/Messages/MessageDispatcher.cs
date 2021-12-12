using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coorth {
    public class MessageDispatcher<TContext> : Disposable {

        private readonly Dictionary<Type, List<IMessageReaction<TContext>>> channels = new Dictionary<Type, List<IMessageReaction<TContext>>>();
        
        public IMessageReaction<TContext> Receive<T>(Action<TContext, T> action)  where T: IMessage {
            var key = typeof(T);
            var reaction = new MessageReaction<TContext, T>(action);
            return Receive(key, reaction);
        }
        
        public IMessageReaction<TContext> Receive(Type key, IMessageReaction<TContext> reaction) {
            if (!channels.TryGetValue(key, out var channel)) {
                channel = new List<IMessageReaction<TContext>>();
                channels[key] = channel;
            }
            channel.Add(reaction);
            reaction.Setup(key, this);
            return reaction;
        }

        public bool Remove(IMessageReaction<TContext> reaction) {
            if (!channels.TryGetValue(reaction.Key, out var channel)) {
                return false;
            }
            return channel.Remove(reaction);
        }

        public void Execute(TContext context, in object e) {
            var key = e.GetType();
            if (!channels.TryGetValue(key, out var channel)) {
                return;
            }
            for (var i = 0; i < channel.Count; i++) {
                channel[i].Execute(context, e);
            }
        }

    }
}