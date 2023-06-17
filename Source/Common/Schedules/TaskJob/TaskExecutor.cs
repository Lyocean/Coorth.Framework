using System;
using System.Buffers;
using System.Threading;
using System.Threading.Tasks;


namespace Coorth.Tasks;

public sealed class TaskJobBox<T> : ITaskJob where T: ITaskJob {

    public T Inner = default!;

    public void Execute() {
        Inner.Execute();
        ClassPool.Return(this);
    }

    public static TaskJobBox<T> Create() {
        return ClassPool.Create<TaskJobBox<T>>() ?? new TaskJobBox<T>();
    }
}

public abstract class TaskExecutor {

    public static readonly SequenceExecutor Sequence = new();
    
    public static readonly ParallelExecutor Parallel = new();
    
    public abstract void Run<TState>(in TState v, Action<TState> action);
    
    public abstract void For(int from_include, int to_exclude, Action<int> action, int batch_size = 0);
    
    public abstract void For<TState>(TState state, int from_include, int to_exclude, Action<TState, int> action, int batch_size = 0);
    
    public abstract void For<T>(ReadOnlySequence<T> sequence, Action<T> action);

    public abstract void Queue(ITaskJob job);
    
    public abstract void Queue<T>(T job) where T : ITaskJob;

    public class ParallelExecutor : TaskExecutor {

        public override void Run<TState>(in TState v, Action<TState> action) {
            Task.Factory.StartNew(static o => {
                using var box = (Box<(TState, Action<TState>)>)o!;
                var (state, action) = box.Value;
                action(state);
            }, Box<(TState, Action<TState>)>.Create((v, action)));
        }

        public override void For(int from_include, int to_exclude, Action<int> action, int batch_size = 0) {
            if (batch_size <= 1) {
                System.Threading.Tasks.Parallel.For(from_include, to_exclude, action);
                return;
            }
            var totalSize = to_exclude - from_include;
            var batchCount = totalSize / batch_size + (totalSize % batch_size == 0 ? 0 : 1);
            System.Threading.Tasks.Parallel.For(0, batchCount, batchIndex => {
                for (var i = 0; i < batch_size; i++) {
                    var index = batchIndex * batch_size + i;
                    action(index);
                }
            });
        }

        public override void For<TState>(TState state, int from_include, int to_exclude, Action<TState, int> action, int batch_size = 0) {
            batch_size = batch_size <= 1 ? 1 : batch_size;
            var totalSize = to_exclude - from_include;
            var batchCount = totalSize / batch_size + (totalSize % batch_size == 0 ? 0 : 1);
            System.Threading.Tasks.Parallel.For(0, batchCount, batchIndex => {
                for (var i = 0; i < batch_size; i++) {
                    var index = batchIndex * batch_size + i;
                    action(state, index);
                }
            });
        }

        public override void For<T>(ReadOnlySequence<T> sequence, Action<T> action) {
            foreach (var memory in sequence) {
                ThreadPool.QueueUserWorkItem(o => {
                    using var box = (Box<(ReadOnlyMemory<T>, Action<T>)>)o!;
                    var (m, a) = box.Value;
                    foreach (var item in m.Span) {
                        a(item);
                    }
                }, Box<(ReadOnlyMemory<T>, Action<T>)>.Create((memory, action)));
            }
        }

        public override void Queue(ITaskJob job) {
#if NET6_0_OR_GREATER
            ThreadPool.UnsafeQueueUserWorkItem(job, true);
#else
            ThreadPool.UnsafeQueueUserWorkItem(static o => ((ITaskJob)o).Execute(), job);
#endif
        }

        public override void Queue<T>(T job) {
            var boxed = TaskJobBox<T>.Create();
            boxed.Inner = job;
#if NET6_0_OR_GREATER
            ThreadPool.UnsafeQueueUserWorkItem(boxed, true);
#else
            ThreadPool.UnsafeQueueUserWorkItem(static o => ((ITaskJob)o).Execute(), boxed);
#endif        
        }
    }

    public class SequenceExecutor : TaskExecutor {
        
        public override void Run<TState>(in TState v, Action<TState> action) => action(v);

        public override void For(int from_include, int to_exclude, Action<int> action, int batch_size = 0) {
            for (var i = from_include; i < to_exclude; i++) {
                action(i);
            }
        }

        public override void For<TState>(TState state, int from_include, int to_exclude, Action<TState, int> action,
            int batch_size = 0) {
            for (var i = from_include; i < to_exclude; i++) {
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

        public override void Queue(ITaskJob job) {
            job.Execute();
        }

        public override void Queue<T>(T job) {
            job.Execute();
        }
    }
}