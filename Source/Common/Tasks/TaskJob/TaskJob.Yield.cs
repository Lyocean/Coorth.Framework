﻿using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Tasks; 

public partial struct TaskJob {
    
    public static YieldAwaitable Yield() => new();

    public readonly struct YieldAwaitable : ICriticalNotifyCompletion {
        
        public bool IsCompleted => false;

        public void GetResult() { }
        
        public YieldAwaitable GetAwaiter() => this;
        
        public void OnCompleted(Action continuation) => UnsafeOnCompleted(continuation);

        public void UnsafeOnCompleted(Action continuation) {
            var context = SynchronizationContext.Current;
            if (context != null) {
                context.Post(sendOrPostCallback, continuation);
            }
            else {
#if NET5_0_OR_GREATER
                ThreadPool.UnsafeQueueUserWorkItem(TaskWorkItem.Create(continuation), false);
#else
                ThreadPool.UnsafeQueueUserWorkItem(waitCallback, continuation);
#endif
            }

        }
    }

    public static ValueTask<T> Yield<T>(Dispatcher dispatcher) where T : ITickEvent => dispatcher.Delay<T>();

    
}