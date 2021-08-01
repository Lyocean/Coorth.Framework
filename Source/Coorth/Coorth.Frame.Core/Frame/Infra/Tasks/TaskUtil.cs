using System;
using System.Threading;
using System.Threading.Tasks;


namespace Coorth {
     public static class TaskUtil {

         private static TaskManager manager;

         public static TaskManager Manager {
             get {
                 if (manager != null) {
                     return manager;
                 }
                 manager = Infra.Get<TaskManager>();
                 return manager;
             }
         }

         public static int MainThreadId => Manager.MainThreadId;
         
         public static Task FromWaitHandle(WaitHandle waitHandle) {
             return FromWaitHandle(waitHandle, Timeout.InfiniteTimeSpan);
         }
         
         public static Task FromWaitHandle(WaitHandle waitHandle, TimeSpan timeout) {
             if (waitHandle.WaitOne(0)) {
                 return Task.CompletedTask;
             } 
             var tcs = new TaskCompletionSource<bool>();
             ThreadPool.RegisterWaitForSingleObject(waitHandle, (state, isTimeout) => {
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
}