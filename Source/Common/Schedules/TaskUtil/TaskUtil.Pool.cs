using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Tasks; 

public static partial class TaskUtil {
    
    private static readonly SendOrPostCallback sendOrPostCallback = state => (state as Action)?.Invoke();

    private static readonly WaitCallback waitCallback = state => (state as Action)?.Invoke();

    public static PoolAwaitable ToPool() => new();
    
    public readonly struct PoolAwaitable : ICriticalNotifyCompletion {
        
        public PoolAwaitable GetAwaiter() => this;
        
        public bool IsCompleted => false;
        
        public void GetResult() { }
        
        public void OnCompleted(Action continuation) => ThreadPool.QueueUserWorkItem(waitCallback, continuation);

        public void UnsafeOnCompleted(Action continuation) {
#if NET6_0_OR_GREATER
            ThreadPool.UnsafeQueueUserWorkItem(TaskJob.Create(continuation), false);
#else
            ThreadPool.UnsafeQueueUserWorkItem(waitCallback, continuation);
#endif
        }
    }

    public static TaskAwaitable ToTask(TaskScheduler? scheduler = null) => new(scheduler);

    public readonly struct TaskAwaitable  : ICriticalNotifyCompletion {

        private readonly TaskScheduler scheduler;
        public TaskAwaitable(TaskScheduler? s) => scheduler = s ?? TaskScheduler.Default;
        
        public TaskAwaitable GetAwaiter() => this;
        
        public bool IsCompleted => false;
        
        public void GetResult() { }

        public void OnCompleted(Action continuation) => UnsafeOnCompleted(continuation);
        
        public void UnsafeOnCompleted(Action continuation) => Task.Factory.StartNew(continuation, CancellationToken.None, TaskCreationOptions.DenyChildAttach, scheduler);
    }
    
    public static ContextAwaitable ToContext(SynchronizationContext context, CancellationToken cancellation = default) => new(context, cancellation);

    public readonly struct ContextAwaitable : ICriticalNotifyCompletion {
        
        private readonly SynchronizationContext context;
        
        private readonly CancellationToken cancellation;

        public ContextAwaitable(SynchronizationContext ctx, CancellationToken cancel) {
            context = ctx;
            cancellation = cancel;
        }
        
        public ContextAwaitable GetAwaiter() => this;

        public bool IsCompleted => false;
        
        public void GetResult() => cancellation.ThrowIfCancellationRequested();

        public void OnCompleted(Action continuation) => UnsafeOnCompleted(continuation);

        public void UnsafeOnCompleted(Action continuation) => context.Post(sendOrPostCallback, continuation);
    }

    public static async ValueTask<SyncContextScope> EnterPool(SynchronizationContext? context = null) {
        var scope = new SyncContextScope(context ?? SynchronizationContext.Current);
        await ToPool();
        return scope;
    }
    
    public static async ValueTask<SyncContextScope> EnterTask(TaskScheduler? scheduler = null, SynchronizationContext? context = null) {
        var scope = new SyncContextScope(context ?? SynchronizationContext.Current);
        await ToTask(scheduler);
        return scope;
    }
    
    public readonly struct SyncContextScope : IAsyncDisposable {
        
        private readonly SynchronizationContext? context;
        
        public SyncContextScope(SynchronizationContext? ctx) {
            context = ctx ?? SynchronizationContext.Current;
        }
        
        public async ValueTask DisposeAsync() {
            if (context != null) {
                await ToContext(context);
            }
        }
    }
}