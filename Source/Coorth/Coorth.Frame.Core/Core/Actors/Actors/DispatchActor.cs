using System;
using System.Collections.Generic;

namespace Coorth.Actors {
    public class DispatchActor<TContext> : Actor {
        
        private readonly Dictionary<Type, List<MessageReaction<TContext>>> reactions = new Dictionary<Type, List<MessageReaction<TContext>>>();

        private TContext context;

        public void Setup(TContext ctx) {
            this.context = ctx;
        }
        
        public override void Execute(in ActorMail e) {
            if (DispatchMessage(e.Type, e.message) == 0) {
                UnHandledMessage(e.Type, e.message);
            }
        }

        public void Subscribe<TMessage>(Action<TContext, TMessage> action) where TMessage: IMessage {
            var type = typeof(TMessage);
            var reaction = new MessageReaction<TContext, TMessage>(action);
            if (!reactions.TryGetValue(type, out var list)) {
                list = new List<MessageReaction<TContext>>();
                reactions[type] = list;
            }
            list.Add(reaction);
        }
        
        private int DispatchMessage(Type type, object message) {
            if (!reactions.TryGetValue(type, out var list)) {
                return 0;
            }
            foreach (var reaction in list) {
                reaction.Execute(context, message);
            }
            return list.Count;
        }

        private void UnHandledMessage(Type type, object message) {
            LogUtil.Error($"Actor UnHandledMessage: type:{type}, message:{message}");
        }

        protected override void Dispose(bool dispose) {
            base.Dispose(dispose);
            reactions.Clear();
        }
    }
}