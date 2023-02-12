using System;
using System.Buffers;
using System.Threading.Tasks;

namespace Coorth.Tasks;

public abstract partial class TaskExecutor {

    public static readonly SequenceExecutor Sequence = new();
    
    public static readonly ParallelExecutor Parallel = new();

    public abstract void Run<T>(T v, Action<T> action);
    
    public abstract void For(int fromInclude, int toExclude, Action<int> action, int batchSize = 0);
    
    public abstract void For<TState>(TState state, int fromInclude, int toExclude, Action<TState, int> action, int batchSize = 0);
    
    public abstract void For<T>(ReadOnlySequence<T> sequence, Action<T> action);
}