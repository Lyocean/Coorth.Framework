using System;
using System.Collections.Concurrent;
using System.Threading;
using Coorth.Logs;

namespace Coorth;

public class AppSynchronization : SynchronizationContext {
    
    private readonly Thread thread;

    private readonly ILogger logger;
    
    private readonly ConcurrentQueue<(SendOrPostCallback, object?)> callbacks = new();

    public AppSynchronization(Thread thread, ILogger logger) {
        this.thread = thread;
        this.logger = logger;
    }

    public override void Post(SendOrPostCallback callback, object? state) {
        callbacks.Enqueue((callback, state));
    }

    public override SynchronizationContext CreateCopy() => new AppSynchronization(thread, logger);

    public override void Send(SendOrPostCallback d, object? state) {
        if (Environment.CurrentManagedThreadId == thread.ManagedThreadId) {
            d(state);
            return;
        }
        Post(d, state);
    }
    
    public void Send(Action action) {
        if (Thread.CurrentThread.ManagedThreadId == thread.ManagedThreadId) {
            action();
            return;
        }
        callbacks.Enqueue((_ => action(), this));
    }

    
    public void Invoke() {
        while (callbacks.TryDequeue(out var value)) {
            var (callback, state) = value;
            try {
                callback(state);
            }
            catch (Exception e) {
                logger.Error(e);
            }
        }
    }
}