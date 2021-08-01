using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Coorth.Tasks {
    public readonly struct ThreadPoolAwaitable {
        public Awaiter GetAwaiter() => new Awaiter();
        public readonly struct Awaiter : ICriticalNotifyCompletion {
            private static readonly WaitCallback switchToCallback = Callback;
            private static void Callback(object state) => ((Action)state)();
            
            public bool IsCompleted => false;
            public void GetResult() { }
            public void OnCompleted(Action continuation) => ThreadPool.UnsafeQueueUserWorkItem(switchToCallback, continuation);
            public void UnsafeOnCompleted(Action continuation) => ThreadPool.UnsafeQueueUserWorkItem(switchToCallback, continuation);
        }
    }
}