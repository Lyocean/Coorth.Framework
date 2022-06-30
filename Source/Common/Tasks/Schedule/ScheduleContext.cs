using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Logs;

namespace Coorth.Tasks; 

public class ScheduleContext : SynchronizationContext {

    public readonly Thread MainThread;

    public readonly CancellationTokenSource Cts = new();
    
    public CancellationToken Cancellation => Cts.Token;
    
    private readonly ConcurrentQueue<(SendOrPostCallback, object?)> callbacks = new();

    public bool IsMain => Thread.CurrentThread.ManagedThreadId == MainThread.ManagedThreadId;
    
    public ScheduleContext(Thread mainThread) {
        MainThread = mainThread;
    }
    
    public Task ToTick() {
        var tcs = new TaskCompletionSource<bool>();
        Post(state => (state as TaskCompletionSource<bool>)?.SetResult(true), tcs);
        return tcs.Task;
    }

    public Task ToPool() {
        var tcs = new TaskCompletionSource<bool>();
        ThreadPool.QueueUserWorkItem(state => (state as TaskCompletionSource<bool>)?.SetResult(true), tcs);
        return tcs.Task;
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


    public override SynchronizationContext CreateCopy() => new ScheduleContext(MainThread);

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