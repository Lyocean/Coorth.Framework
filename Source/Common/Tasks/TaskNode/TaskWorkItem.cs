using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Coorth.Tasks;

public interface ITaskWorkItem {
    void Execute();
}

#if NET5_0_OR_GREATER
public sealed class TaskWorkItem : IThreadPoolWorkItem, ITaskWorkItem {
#else
public sealed class TaskWorkItem : ITaskWorkItem {
#endif
    
    private static readonly ConcurrentStack<TaskWorkItem> pool = new();

    private static readonly Action empty = () => { };

    private Action continuation = empty;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TaskWorkItem Create(Action action) {
        if (!pool.TryPop(out var item)) {
            item = new TaskWorkItem();
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

