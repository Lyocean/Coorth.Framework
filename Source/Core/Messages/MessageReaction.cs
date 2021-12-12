using System;

namespace Coorth {
    public interface IMessageReaction<TContext> : IDisposable {
        Type Key { get; }
        void Setup(Type type, MessageDispatcher<TContext> dispatcher);
        void Execute(TContext context, in object e);
    }
    
    public abstract class MessageReaction<TContext> : Disposable, IMessageReaction<TContext> {
        
        public Guid Id = Guid.NewGuid();
        
        protected MessageDispatcher<TContext> Dispatcher{ get; private set; }
        
        public Type Key { get; private set; }

        public void Setup(Type key, MessageDispatcher<TContext> dispatcher) {
            this.Key = key;
            this.Dispatcher = dispatcher;
        }

        public abstract void Execute(TContext context, in object e);

        protected override void OnDispose(bool dispose) {
            Dispatcher.Remove(this);
        }
    }
    
    public class MessageReaction<TContext, TMessage> : MessageReaction<TContext> where TMessage: IMessage {

        public readonly Action<TContext, TMessage> Action;

        public MessageReaction(Action<TContext, TMessage> action) {
            this.Action = action;
        }
        
        public override void Execute(TContext context, in object e) {
            Action?.Invoke(context, (TMessage)e);
        }
    }
}