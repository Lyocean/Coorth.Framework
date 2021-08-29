using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
   public interface IMailbox {
        void Post<T>(ActorRef target, ActorRef sender, T message);
        void Request<T>(ActorRef target, ActorRef sender, T message, ActorFuture future);
        void Execute(ActorScheduler scheduler);
    }
    
    public class ActorMailbox : IMailbox  {
        
        private readonly ConcurrentQueue<ActorMail> mails = new ConcurrentQueue<ActorMail>();

        private readonly int throughput;

        private int state;

        private const int IDLE_STATE = 0;
        private const int BUSY_STATE = 1;

        private class MailEvent : IEvent {
            public static readonly MailEvent Instance = new MailEvent();
        }
        
        public ActorMailbox(IActorConfig config) {
            throughput = config.ActorThroughput;
        }
        
        public void Post<T>(ActorRef target, ActorRef sender, T message) {
            if (typeof(T).IsValueType) {
                var box = ActorBox<T>.Create(message);
                var mail = new ActorMail(box, target, sender, null);
                mails.Enqueue(mail);
            }
            else {
                var mail = new ActorMail(message, target, sender, null);
                mails.Enqueue(mail);
            }
        }

        public void Request<T>(ActorRef target, ActorRef sender, T message, ActorFuture future) {
            if (typeof(T).IsValueType) {
                var box = ActorBox<T>.Create(message);
                var mail = new ActorMail(box, target, sender, future);
                mails.Enqueue(mail);
            }
            else {
                var mail = new ActorMail(message, target, sender, future);
                mails.Enqueue(mail);
            }
        }

        public void Execute(ActorScheduler scheduler) {
            if (Interlocked.CompareExchange(ref state, BUSY_STATE, IDLE_STATE) == IDLE_STATE) {
                // scheduler.Schedule(MailEvent.Instance, ExecuteAsync);
            }
        }

        private async Task ExecuteAsync(MailEvent instance) {
            for (var i = 0; i < throughput; i++) {
                if (mails.TryDequeue(out var mail)) {
                    await mail.Target.Context.Receive(mail);
                } else {
                    break;
                }
            }
            if (mails.IsEmpty) {
                Interlocked.Exchange(ref state, IDLE_STATE);
                return;
            }
        }
    }
    
    
    public readonly struct ActorMail {
        public readonly object message;
        private readonly IActorBox box;
        public readonly ActorRef Sender;
        public readonly ActorRef Target;
        private readonly ActorFuture future;

        public Type Type => box == null ? message.GetType() : box.Type;

        public object Message => box == null ? message : box.Value;

        public T Get<T>() => box == null ? (T)message : ((ActorBox<T>) box).Value;

        public ActorMail(object msg, ActorRef target, ActorRef sender, ActorFuture future) {
            this.box = null;
            this.message = msg;
            this.Target = target;
            this.Sender = sender;
            this.future = future;
        }
        
        public ActorMail(IActorBox box, ActorRef target, ActorRef sender, ActorFuture future) {
            this.message = null;
            this.box = box;
            this.Target = target;
            this.Sender = sender;
            this.future = future;
        }

        public bool Response<T>(T msg) {
            if (future != null) {
                (future as ActorFuture<T>)?.Response(msg);
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

        public void Forward(ActorRef target) {
            if (future != null) {
                target.Context.Forward(Sender, Message, future);
            }
            else {
                target.Send(Message, Sender);
            }
        }

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

    public interface IActorBox {
        object Value { get; }
        Type Type { get; }
    }
    
    internal sealed class ActorBox<T> : IActorBox, IDisposable {
        public ObjectPool<ActorBox<T>> Pool;
        public T Value;
        object IActorBox.Value => Value;
        public Type Type => Value.GetType();

        public static ActorBox<T> Create(T value) {
            var pool = ObjectPool<ActorBox<T>>.Instance;
            var box = pool.Get() ?? new ActorBox<T>();
            box.Value = value;
            box.Pool = pool;
            return box;
        }
        
        public void Dispose() {
            Pool?.Put(this);
        }
    }
    
    public class ObjectPool<T> where T: class {
        public static readonly ObjectPool<T> Instance = new ObjectPool<T>();
        
        private readonly ConcurrentQueue<T> items = new ConcurrentQueue<T>();

        public int Capacity;
        
        public T Get() {
            return items.TryDequeue(out var item) ? item : default;
        }

        public bool Put(T item) {
            if (items.Count < Capacity) {
                items.Enqueue(item);
                return true;
            }
            return false;
        }
    }
}