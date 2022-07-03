using System;
using System.Buffers;
using System.Threading.Tasks;

namespace Coorth.Tasks;

public abstract class TaskExecutor {

    public static readonly SequenceExecutor Sequence = new();
    
    public static readonly ParallelExecutor Parallel = new();

    public abstract void Run<T>(T v, Action<T> action);
    
    public abstract void For(int fromInclude, int toExclude, Action<int> action, int batchSize = 0);
    
    public abstract void For<TState>(in TState state, int fromInclude, int toExclude, Action<TState, int> action, int batchSize = 0);
    
    public abstract void For<T>(ReadOnlySequence<T> sequence, Action<T> action);
    
    public class SequenceExecutor : TaskExecutor {
        public override void Run<T>(T v, Action<T> action) => action(v);
        
        public override void For(int fromInclude, int toExclude, Action<int> action, int batchSize = 0) {
            for (var i = fromInclude; i < toExclude; i++) {
                action(i);
            }
        }

        public override void For<TState>(in TState state, int fromInclude, int toExclude, Action<TState, int> action, int batchSize = 0) {
            for (var i = fromInclude; i < toExclude; i++) {
                action(state, i);
            }
        }
        
        public override void For<T>(ReadOnlySequence<T> sequence, Action<T> action) {
            foreach (var memory in sequence) {
                foreach (var value in memory.Span) {
                    action(value);
                }
            }
        }
    }
    
    public class ParallelExecutor : TaskExecutor {
        
        public override void Run<T>(T v, Action<T> action) {
            Task.Factory.StartNew(() => action(v));
        }

        
        //TODO: batch process
        public override void For(int fromInclude, int toExclude, Action<int> action, int batchSize = 0) {
            if (batchSize <= 0) {
                System.Threading.Tasks.Parallel.For(fromInclude, toExclude, action);
                return;
            }
            System.Threading.Tasks.Parallel.For(fromInclude, toExclude, action);

            // var size = toExclude - fromInclude;
            // var count = size / batchSize + (size % batchSize == 0 ? 0 : 1);
        }
        
        //TODO: batch process
        public override void For<TState>(in TState state, int fromInclude, int toExclude, Action<TState, int> action, int batchSize = 0) {
            if (batchSize <= 0) {
                for (var i = fromInclude; i < toExclude; i++) {
                    action(state, i);
                }
                return;
            }
            for (var i = fromInclude; i < toExclude; i++) {
                action(state, i);
            }
        }

        public override void For<T>(ReadOnlySequence<T> sequence, Action<T> action) {
            foreach (var memory in sequence) {
                foreach (var value in memory.Span) {
                    action(value);
                }
            }
        }

        public void For(int fromInclude, int toExclude, Action<int, ParallelLoopState> action) {
            System.Threading.Tasks.Parallel.For(fromInclude, toExclude, action);
        }
    }
    
}