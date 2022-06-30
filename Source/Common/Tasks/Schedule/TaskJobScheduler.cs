using System;
using System.Buffers;
using System.Threading.Tasks;

namespace Coorth.Tasks;

//TODO:Custom task scheduler
public abstract class TaskJobScheduler {

    public static readonly SequenceScheduler Sequence = new();
    public static readonly ParallelScheduler Parallel = new();

    public abstract void For(int fromInclude, int toExclude, Action<int> action);
    
    public class SequenceScheduler : TaskJobScheduler {
        public override void For(int fromInclude, int toExclude, Action<int> action) {
            System.Threading.Tasks.Parallel.For(fromInclude, toExclude, action);
        }
        
        public void For<T>(ReadOnlySequence<T> sequence, Action<T> action) {
            foreach (var memory in sequence) {
                foreach (var value in memory.Span) {
                    action(value);
                }
            }
        }
    }
    
    public class ParallelScheduler : TaskJobScheduler {
        
        public override void For(int fromInclude, int toExclude, Action<int> action) {
            for (var i = fromInclude; i < toExclude; i++) {
                action(i);
            }
        }

        public void For<T>(ReadOnlySequence<T> sequence, Action<T> action) {
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
    



    
    // public void BatchFor<T>(ReadOnlySequence<T> sequence) {
    //
    //     foreach (var memory in sequence) {
    //         
    //     }
    //     
    //     SequenceReader<T> reader;
    //     
    // }

    // private static readonly ParameterizedThreadStart s_longRunningThreadWork = (s => ((Task) s).ExecuteEntryUnsafe((Thread) null));
    //
    // protected override IEnumerable<Task> GetScheduledTasks() {
    //     throw new System.NotImplementedException();
    // }
    //
    // protected override void QueueTask(Task task) {
    //     task.RunSynchronously();
    //     var options = task.CreationOptions;
    //     if ((options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None) {
    //         new Thread(s_longRunningThreadWork) {
    //             IsBackground = true,
    //             Name = ".NET Long Running Task"
    //         }.Start(task);
    //     }
    //     else {
    //         #if NET5_0_OR_GREATER
    //             ThreadPool.UnsafeQueueUserWorkItem((IThreadPoolWorkItem) task, (options & TaskCreationOptions.PreferFairness) == TaskCreationOptions.None );
    //         #else
    //             ThreadPool.QueueUserWorkItem<Task>(t => t.RunSynchronously(), task, (options & TaskCreationOptions.PreferFairness) == TaskCreationOptions.None);
    //             // ThreadPool.UnsafeQueueUserWorkItem<Task>(t => t.RunSynchronously(), task, (options & TaskCreationOptions.PreferFairness) == TaskCreationOptions.None);
    //             // ThreadPool.UnsafeQueueUserWorkItemInternal((object) task, (options & TaskCreationOptions.PreferFairness) == TaskCreationOptions.None);
    //         #endif
    //     }
    //
    //     
    // }

    // protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) {
    //
    //     // ThreadPool.
    //     // ThreadPool.TryPopCustomWorkItem()
    //     // if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem((object) task))
    //     //     return false;
    //     // try
    //     // {
    //     //     task.ExecuteEntryUnsafe((Thread) null);
    //     // }
    //     // finally
    //     // {
    //     //     if (taskWasPreviouslyQueued)
    //     //         this.NotifyWorkItemProgress();
    //     // }
    //     // return true;
    // }


}