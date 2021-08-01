using System;
using System.Threading.Tasks;

namespace Coorth {
    public class EventFuture<T>  : Disposable, IEventReaction<T> where T: IEvent{

        private EventChannel<T> channel;

        public EventId ProcessId { get; } = EventId.New();

        public float Priority { get; } = 0;
        
        public Type EventType => typeof(T);

        protected int count = 0;
        
        protected readonly TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();

        public Task<T> Task => taskCompletionSource.Task;
        
        public EventFuture(int times) {
            count = times;
        }
        
        public void Setup(EventChannel<T> eventChannel) {
            this.channel = eventChannel;
        }

        public virtual void Execute(in T e) {
            count--;
            if (count <= 0) {
                taskCompletionSource.SetResult(e);
                this.Dispose();
            }
        }
        
        protected override void Dispose(bool dispose) {
            channel.Remove(this.ProcessId);
        }
    }
}