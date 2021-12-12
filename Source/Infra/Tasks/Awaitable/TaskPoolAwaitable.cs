using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Tasks {
    public readonly struct TaskPoolAwaitable {
        public Awaiter GetAwaiter() => new Awaiter();
        public readonly struct Awaiter : ICriticalNotifyCompletion {
            private static readonly Action<object> switchToCallback = Callback;
            private static void Callback(object state) => ((Action)state)();
            
            public bool IsCompleted => false;
            public void GetResult() { }
            public void OnCompleted(Action continuation) => Task.Factory.StartNew(switchToCallback, continuation, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
            public void UnsafeOnCompleted(Action continuation) => Task.Factory.StartNew(switchToCallback, continuation, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }
    }
}