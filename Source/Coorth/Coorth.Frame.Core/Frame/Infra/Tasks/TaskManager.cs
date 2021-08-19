using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
    using Coorth.Tasks;
    
    public class ConcurrentEventNode : Disposable, IEventNode {

        public EventId ProcessId { get; } = new EventId();

        public IEventNode Parent { get; set; }

        public void Execute<T>(in T e) where T : IEvent {
            
        }
    }

    public sealed class TaskManager : ManagerBase {

        public int MainThreadId;

        private readonly ConcurrentEventNode eventNode = new ConcurrentEventNode();

        public void Setup() {
            MainThreadId = Thread.CurrentThread.ManagedThreadId;
            Dispatcher.AddChild(eventNode);
        }

        public Task RunAsync(Func<Task> function) => Task.Factory.StartNew(function);

        public Task RunAsync(Func<Task> function, CancellationToken cancellationToken) =>
            Task.Factory.StartNew(function, cancellationToken);

        public async ValueTask RunAsync(Action action, bool configureAwait = true) {
            if (configureAwait) {
                var currentContext = SynchronizationContext.Current;
                await ToThreadPool();
                try {
                    action();
                }
                finally {
                    if (currentContext != null) {
                        await ToSynchronizationContext(currentContext);
                    }
                }
            }
            else {
                await ToThreadPool();
                action();
            }
        }
        
        public Task ToMainThread() {
            if (Thread.CurrentThread.ManagedThreadId == MainThreadId) {
                return Task.CompletedTask;
            }
            return Dispatcher.DelayFrame<EventTickUpdate>();
        }

        public ThreadPoolAwaitable ToThreadPool() => new ThreadPoolAwaitable();

        public TaskPoolAwaitable ToTaskPool() => new TaskPoolAwaitable();

        public SynchronizationContextAwaitable ToSynchronizationContext(SynchronizationContext context, CancellationToken cancellation = default) => new SynchronizationContextAwaitable(context, cancellation);

    }

}