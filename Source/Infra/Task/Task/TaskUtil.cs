using System;
using System.Threading;
using System.Threading.Tasks;


namespace Coorth {
     public static partial class TaskUtil {

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
         
         public static Task<T> DelayFrame<T>(int count = 1) where T : ITickEvent => Manager.Dispatcher.DelayFrame<T>(count);
         
         public static Task DelayFrame(int count = 1) => Manager.Dispatcher.DelayFrame(count);

         public static Task<T> DelayTime<T>(TimeSpan duration) where T : ITickEvent => Manager.Dispatcher.DelayTime<T>(duration);
         
         public static Task DelayTime(TimeSpan duration) => Manager.Dispatcher.DelayTime(duration);
             
         public static Task<T> UntilCondition<T>(Func<T, bool> condition, int times = 1) where T : ITickEvent => Manager.Dispatcher.UntilCondition<T>(condition, times);
         
         public static Task UntilCondition(Func<bool> condition, int matchTimes = 1) => Manager.Dispatcher.UntilCondition(condition, matchTimes);
         
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