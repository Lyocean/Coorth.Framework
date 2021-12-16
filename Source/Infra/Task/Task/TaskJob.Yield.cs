using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Coorth.Tasks;
using System.Threading.Tasks;

namespace Coorth {
    public partial struct TaskJob {

        public static YieldAwaitable Yield() => Task.Yield();

        public static YieldAwaitable<T> Yield<T>() => new YieldAwaitable<T>();

        public static YieldAwaitable<T> Yield<T>(CancellationToken cancellationToken) => new YieldAwaitable<T>();

        //new YieldAwaitable();

        // public static YieldAwaitable2<T> Yield<T>() => new YieldAwaitable2<T>();

        // public static YieldAwaitable Yield<T>() 
    }
}

namespace Coorth.Tasks {
    public struct YieldAwaitable<T> {

        public Awaiter GetAwaiter() => new Awaiter();
        
        public readonly struct Awaiter : ICriticalNotifyCompletion {
            
            private static readonly SendOrPostCallback sendOrPostCallbackDelegate = Continuation;
            private static readonly WaitCallback waitCallbackDelegate = Continuation;
            
            private static void Continuation(object state) => ((Action)state).Invoke();

            public void OnCompleted(Action continuation) => UnsafeOnCompleted(continuation);

            public void UnsafeOnCompleted(Action continuation) {
                var syncContext = SynchronizationContext.Current;
                if (syncContext != null) {
                    syncContext.Post(sendOrPostCallbackDelegate, continuation);
                } else {
                    ThreadPool.UnsafeQueueUserWorkItem(waitCallbackDelegate, continuation);
                }
            }
        }
    }

    // public class YieldPromise {
    //
    //
    //     public static YieldPromise Create<T>(CancellationToken cancellationToken) {
    //         
    //     }
    //     
    // }

    
    // public struct YieldAwaitable2<T> {
    //     
    // }
    
}