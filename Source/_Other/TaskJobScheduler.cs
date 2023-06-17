using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Tasks; 

//TODO: [Task]: Replace internal task scheduler
public class TaskJobScheduler : TaskScheduler {
    
    protected override IEnumerable<Task>? GetScheduledTasks() {
        throw new System.NotImplementedException();
    }

    protected override void QueueTask(Task task) {
        throw new System.NotImplementedException();
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) {
        throw new System.NotImplementedException();
    }
}