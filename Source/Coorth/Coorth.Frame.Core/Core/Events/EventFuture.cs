using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coorth {
    public class EventEmitter<T> : Disposable where T: IEvent {
        
        private readonly ConcurrentDictionary<EventId, IEventProcess<T>> processes = new ConcurrentDictionary<EventId, IEventProcess<T>>();

        public void Trigger(T e) {
            foreach (var process in processes.Values) {
                process.Execute(e);
            }
        }

        public void Subscribe(IEventProcess<T> process) {
            processes.TryAdd(process.ProcessId, process);
        }

        public bool Remove(IEventProcess<T> process) {
            return processes.TryRemove(process.ProcessId, out var _);
        }
    }
    
    public class EventFuture<T> : IEventProcess<T> where T: IEvent {
        public EventId ProcessId { get; } = EventId.New();

        private readonly Func<T, bool> condition;
        private readonly TaskCompletionSource<T> source = new TaskCompletionSource<T>();

        public EventFuture(Func<T, bool> cond = null) {
            condition = cond;
        }
        
        void IEventProcess<T>.Execute(in T e) {
            if (condition != null && !condition.Invoke(e)) {
                return;
            }
            source.SetResult(e);
        }

        public Task<T> ReceiveAsync() {
            return source.Task;
        }

        public bool TryReceive(out T e) {
            if (source.Task.IsCompleted) {
                e = source.Task.Result;
                return true;
            }

            e = default;
            return false;
        }
    }

    public class EventReceiver<T> : IEventProcess<T> where T : IEvent {
        public EventId ProcessId { get; } = EventId.New();

        private readonly Func<T, bool> condition;
        
        private volatile TaskCompletionSource<T> source;
        private readonly ConcurrentQueue<T> events = new ConcurrentQueue<T>();
        
        public EventReceiver(Func<T, bool> cond = null) {
            condition = cond;
        }
        
        void IEventProcess<T>.Execute(in T e) {
            if (condition != null && !condition.Invoke(e)) {
                return;
            }

            if (source != null) {
                var taskSource = source;
                source = null;
                taskSource.SetResult(e);
            }
            events.Enqueue(e);
        }
           
        public Task<T> ReceiveAsync() {
            if (events.TryDequeue(out var e)) {
                return Task.FromResult(e);
            }

            if (source == null) {
                throw new InvalidOperationException("Can't receive twice");
            }
            source = new TaskCompletionSource<T>();
            return source.Task;
        }
        
        public int TryReceiveAll(out List<T> list) {
            var count = events.Count;
            list = new List<T>(count);
            while (events.TryDequeue(out var e) && count > 0) {
                count--;
                list.Add(e);
            }
            return list.Count;
        }
        
        public bool TryReceive(out T e) {
            return events.TryDequeue(out e);
        }
    }
}