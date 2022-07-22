using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Tasks; 

public partial struct TaskJob {
    
    public static Task FromWaitHandle(WaitHandle handle) => FromWaitHandle(handle, Timeout.InfiniteTimeSpan);

    public static Task FromWaitHandle(WaitHandle handle, TimeSpan timeout) {
        if (handle.WaitOne(0)) {
            return Task.CompletedTask;
        }
        var tcs = new TaskCompletionSource<bool>();
        ThreadPool.RegisterWaitForSingleObject(handle, (state, isTimeout) => {
            if (isTimeout) {
                tcs.TrySetCanceled();
            }
            else {
                tcs.TrySetResult(true);
            }
        }, tcs, timeout, true);
        return tcs.Task;
    }
}