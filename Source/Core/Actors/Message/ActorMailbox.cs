using System;
using System.Collections.Concurrent;

namespace Coorth {
    
    public class ActorMailbox {
        
        private readonly ConcurrentQueue<ActorMail> mails = new ConcurrentQueue<ActorMail>();

        public int Count => mails.Count;
        
        public bool TryDequeue(out ActorMail mail) {
            return mails.TryDequeue(out mail);
        }
        
        public void Enqueue(in ActorMail mail) {
            mails.Enqueue(mail);
        }
        
        
        
        
        
        
        // private readonly int throughput;

        // private int state;
        //
        // private const int IDLE_STATE = 0;
        // private const int BUSY_STATE = 1;
        //
        // private class MailEvent : IEvent {
        //     public static readonly MailEvent Instance = new MailEvent();
        // }
        //
        // public ActorMailbox(IActorConfig config) {
        //     throughput = config.ActorThroughput;
        // }
        // //
        // // public void Post<T>(ActorRef target, ActorRef sender, T message) {
        // //     if (typeof(T).IsValueType) {
        // //         var box = ActorBox<T>.Create(message);
        // //         var mail = new ActorMail(box, target, sender, null);
        // //         mails.Enqueue(mail);
        // //     }
        // //     else {
        // //         var mail = new ActorMail(message, target, sender, null);
        // //         mails.Enqueue(mail);
        // //     }
        // // }
        //
        // public void Request<T>(ActorRef target, ActorRef sender, T message, ActorFuture future) {
        //     if (typeof(T).IsValueType) {
        //         var box = ActorBox<T>.Create(message);
        //         var mail = new ActorMail(box, target, sender, future);
        //         mails.Enqueue(mail);
        //     }
        //     else {
        //         var mail = new ActorMail(message, target, sender, future);
        //         mails.Enqueue(mail);
        //     }
        // }
        //
        // public void Execute(ActorScheduler scheduler) {
        //     if (Interlocked.CompareExchange(ref state, BUSY_STATE, IDLE_STATE) == IDLE_STATE) {
        //         // scheduler.Schedule(MailEvent.Instance, ExecuteAsync);
        //     }
        // }
        //
        // private async Task ExecuteAsync(MailEvent instance) {
        //     for (var i = 0; i < throughput; i++) {
        //         if (mails.TryDequeue(out var mail)) {
        //             await mail.Target.Context.Receive(mail);
        //         } else {
        //             break;
        //         }
        //     }
        //     if (mails.IsEmpty) {
        //         Interlocked.Exchange(ref state, IDLE_STATE);
        //         return;
        //     }
        // }
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