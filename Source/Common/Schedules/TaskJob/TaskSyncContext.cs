using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Logs;

namespace Coorth.Tasks; 

public class TaskSyncContext : SynchronizationContext {

    public readonly Thread MainThread;

    public readonly CancellationTokenSource Cts = new();
    
    public CancellationToken Cancellation => Cts.Token;
    
    private readonly ConcurrentQueue<(SendOrPostCallback, object?)> callbacks = new();

    public bool IsMain => Thread.CurrentThread.ManagedThreadId == MainThread.ManagedThreadId;
    
    public TaskSyncContext(Thread mainThread) {
        MainThread = mainThread;
    }
    
    public override void Post(SendOrPostCallback callback, object? state) {
        if (Environment.CurrentManagedThreadId == MainThread.ManagedThreadId) {
            try {
                callback(state);
            } catch (Exception e) {
                LogUtil.Exception(e);
            }
            return;
        }
        callbacks.Enqueue((callback, state));
    }


    public override SynchronizationContext CreateCopy() => new TaskSyncContext(MainThread);

    public void Execute() {
        while (callbacks.TryDequeue(out var value)) {
            var (callback, state) = value;
            try {
                callback(state);
            } catch (Exception e) {
                LogUtil.Exception(e);
            }
        }
    }

    public void Cancel() => Cts.Cancel();

}