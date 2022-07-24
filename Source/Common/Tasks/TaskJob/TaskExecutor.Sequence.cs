using System;
using System.Buffers;
using System.Collections.Concurrent;

namespace Coorth.Tasks;

public abstract partial class TaskExecutor {
    
    public class SequenceExecutor : TaskExecutor {
        
        public override void Run<T>(T v, Action<T> action) => action(v);

        public override void For(int fromInclude, int toExclude, Action<int> action, int batchSize = 0) {
            for (var i = fromInclude; i < toExclude; i++) {
                action(i);
            }
        }

        public override void For<TState>(TState state, int fromInclude, int toExclude, Action<TState, int> action,
            int batchSize = 0) {
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
}