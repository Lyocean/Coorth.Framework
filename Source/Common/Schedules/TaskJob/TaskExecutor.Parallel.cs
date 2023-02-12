using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Tasks;

public abstract partial class TaskExecutor {
    
    public class ParallelExecutor : TaskExecutor {
        
        public override void Run<T>(T v, Action<T> action) {
            Task.Factory.StartNew(static o => {
                using var box = (Box<(T, Action<T>)>)o!;
                var (state, action) = box.Value;
                action(state);
            }, Box<(T, Action<T>)>.Create((v, action)));
        }

        public override void For(int fromInclude, int toExclude, Action<int> action, int batchSize = 0) {
            if (batchSize <= 1) {
                System.Threading.Tasks.Parallel.For(fromInclude, toExclude, action);
                return;
            }
            var totalSize = toExclude - fromInclude;
            var batchCount = totalSize / batchSize + (totalSize % batchSize == 0 ? 0 : 1);
            System.Threading.Tasks.Parallel.For(0, batchCount, batchIndex => {
                for (var i = 0; i < batchSize; i++) {
                    var index = batchIndex * batchSize + i;
                    action(index);
                }
            });
        }

        public override void For<TState>(TState state, int fromInclude, int toExclude, Action<TState, int> action, int batchSize = 0) {
            batchSize = batchSize <= 1 ? 1 : batchSize;
            var totalSize = toExclude - fromInclude;
            var batchCount = totalSize / batchSize + (totalSize % batchSize == 0 ? 0 : 1);
            System.Threading.Tasks.Parallel.For(0, batchCount, batchIndex => {
                for (var i = 0; i < batchSize; i++) {
                    var index = batchIndex * batchSize + i;
                    action(state, index);
                }
            });
        }

        public override void For<T>(ReadOnlySequence<T> sequence, Action<T> action) {
            foreach (var memory in sequence) {
                TaskThreadPool.QueueUserWorkItem(o => {
                    using var box = (Box<(ReadOnlyMemory<T>, Action<T>)>)o!;
                    var (m, a) = box.Value;
                    foreach (var item in m.Span) {
                        a(item);
                    }
                }, Box<(ReadOnlyMemory<T>, Action<T>)>.Create((memory, action)));
            }
        }
    }
}