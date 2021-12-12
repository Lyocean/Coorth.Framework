using System;
using System.Threading.Tasks;

namespace Coorth {

    public readonly struct ActorMail {
        public readonly object message;
        private readonly IActorBox box;
        public readonly ActorRef Sender;
        public readonly ActorRef Target;
        private readonly MessageFuture future;

        public Type Type => box == null ? message.GetType() : box.Type;

        public object Message => box == null ? message : box.Value;

        public T Get<T>() => box == null ? (T)message : ((ActorBox<T>) box).Value;

        public ActorMail(object msg, ActorRef target, ActorRef sender, MessageFuture future) {
            this.box = null;
            this.message = msg;
            this.Target = target;
            this.Sender = sender;
            this.future = future;
        }
        
        public ActorMail(IActorBox box, ActorRef target, ActorRef sender, MessageFuture future) {
            this.message = null;
            this.box = box;
            this.Target = target;
            this.Sender = sender;
            this.future = future;
        }

        public static ActorMail Create<T>(T message, ActorRef target, ActorRef sender, MessageFuture future) {
            if (typeof(T).IsValueType) {
                var box = ActorBox<T>.Create(message);
                var mail = new ActorMail(box, target, sender, null);
                return mail;
            }
            else {
                var mail = new ActorMail(message, target, sender, null);
                return mail;
            }
        }

        public bool Response<T>(T msg) {
            if (future != null) {
                (future as MessageFuture<T>)?.SetResult(msg);
                return true;
            }
            else if(!Sender.IsNull) {
                Sender.Send(msg, Target);
                return true;
            }
            else {
                return false;
            }
        }

        // public void Forward(ActorRef target) {
        //     if (future != null) {
        //         target.Context.Forward(Sender, Message, future);
        //     }
        //     else {
        //         target.Send(Message, Sender);
        //     }
        // }

        public void Case<T>(Action<T> action) {
            if (typeof(T).IsAssignableFrom(Type)) {
                action(Get<T>());
            }
        }
        
        public Task Case<T>(Func<T, Task> action) {
            if (typeof(T).IsAssignableFrom(Type)) {
                return action(Get<T>());
            }
            return Task.CompletedTask;
        }
    }
}