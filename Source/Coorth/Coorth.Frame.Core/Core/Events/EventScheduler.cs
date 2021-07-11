using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coorth {
    public class EventScheduler {

        public int MainThreadId = 0;
        
        public static readonly EventScheduler Default = new EventScheduler();
        
        public static readonly EventScheduler Synchronous = new SynchronousScheduler();
        
        public static readonly EventScheduler Asynchronous = new AsynchronousScheduler();

        public virtual void Schedule<T>(T e, Action<T> action) where T : IEvent {
            action.Invoke(e);
        }

        public virtual ValueTask Schedule<T>(T e, Func<T, ValueTask> action) where T : IEvent {
            return action.Invoke(e);
        }
        
        public virtual void Schedule<T>(T e, IEventProcess<T> process) where T : IEvent {
            process.Execute(e);
        }

        public virtual ValueTask Schedule<T>(T e, IEventProcessAsync<T> process) where T : IEvent {
            return process.ExecuteAsync(e);
        }

        public virtual Task Schedule<T>(T e, IEnumerable<IEventProcess> processes) where T : IEvent {
            return ScheduleSync(e, processes);
        }
        
        public void ScheduleSync<T>(T e, Action<T> action) where T : IEvent {
            action.Invoke(e);
        }

        public void ScheduleSync<T>(T e, Func<T, ValueTask> action) where T : IEvent {
            action.Invoke(e);
        }

        public void ScheduleSync<T>(T e, IEventProcess<T> process) where T : IEvent {
            process.Execute(e);
        }

        public ValueTask ScheduleSync<T>(T e, IEventProcessAsync<T> process) where T : IEvent {
            return process.ExecuteAsync(e);
        }

        public async Task ScheduleSync<T>(T e, IEnumerable<IEventProcess> processes) where T : IEvent {
            foreach (var process in processes) {
                switch (process) {
                    case IEventProcess<T> sync:
                        sync.Execute(e);
                        break;
                    case IEventProcessAsync<T> async:
                        await async.ExecuteAsync(e);
                        break;
                    default:
                        throw new ArgumentException($"process type error: {process.GetType()} - {process.ProcessId}");
                }
            }
        }

        public Task ScheduleAsync<T>(T e, Action<T> action) where T : IEvent {
            return Task.Factory.StartNew(() => { action.Invoke(e); });
        }
        
        public Task ScheduleAsync<T>(T e, Func<T, ValueTask> action) where T : IEvent {
            return Task.Factory.StartNew(() => action.Invoke(e));
        }

        public Task ScheduleAsync<T>(T e, IEventProcess<T> process) where T : IEvent {
            return Task.Factory.StartNew(() => process.Execute(e));
        }

        public Task ScheduleAsync<T>(T e, IEventProcessAsync<T> process) where T : IEvent {
            return Task.Factory.StartNew(() => process.ExecuteAsync(e));
        }

        public Task ScheduleAsync<T>(T e, IEnumerable<IEventProcess> processes) where T : IEvent {
            var tasks = new List<Task>();
            foreach (var process in processes) {
                switch (process) {
                    case IEventProcess<T> sync:
                        tasks.Add(Task.Factory.StartNew(() => sync.Execute(e)));
                        break;
                    case IEventProcessAsync<T> async:
                        tasks.Add(Task.Factory.StartNew(() => async.ExecuteAsync(e)));
                        break;
                    default:
                        throw new ArgumentException($"process type error: {process.GetType()} - {process.ProcessId}");
                }
            }
            return Task.WhenAll(tasks);
        }

        private class SynchronousScheduler : EventScheduler { }

        private class AsynchronousScheduler : EventScheduler {
            public override void Schedule<T>(T e, Action<T> action) => ScheduleAsync(e, action);

            public override async ValueTask Schedule<T>(T e, Func<T, ValueTask> action) => await ScheduleAsync(e, action);

            public override void Schedule<T>(T e, IEventProcess<T> process) => ScheduleAsync(e, process);
            
            public override async ValueTask Schedule<T>(T e, IEventProcessAsync<T> process) => await ScheduleAsync(e, process);
            
            public override Task Schedule<T>(T e, IEnumerable<IEventProcess> processes)  => ScheduleAsync(e, processes);
        }
    }
    
}
