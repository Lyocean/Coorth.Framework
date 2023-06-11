using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
#if NET6_0_OR_GREATER
using System.Threading;
#endif

namespace Coorth.Tasks;

#if NET6_0_OR_GREATER
public interface ITaskJob : IThreadPoolWorkItem {
#else
public interface ITaskJob {
    void Execute();
#endif
}

public sealed class TaskJob : ITaskJob {
    
    private static readonly ConcurrentStack<TaskJob> pool = new();

    private static readonly Action empty = () => { };

    private Action continuation = empty;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TaskJob Create(Action action) {
        if (!pool.TryPop(out var item)) {
            item = new TaskJob();
        }
        item.continuation = action;
        return item;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute() {
        var action = continuation;
        continuation = empty;
        pool.Push(this);
        action.Invoke();
    }
}