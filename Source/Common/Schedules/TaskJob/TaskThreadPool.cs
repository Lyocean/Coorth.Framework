using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Coorth.Tasks;

//TODO: [Task]: Replace internal thread pool
public static class TaskThreadPool {
#if NET6_0_OR_GREATER
    public static int ThreadCount => ThreadPool.ThreadCount;

    public static long CompletedWorkItemCount => ThreadPool.CompletedWorkItemCount;

    public static long PendingWorkItemCount => ThreadPool.PendingWorkItemCount;

#endif
    public static void GetAvailableThreads(out int workerThreads, out int completionPortThreads) => ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);

    public static void GetMaxThreads(out int workerThreads, out int completionPortThreads) => ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
    
    public static void GetMinThreads(out int workerThreads, out int completionPortThreads) => ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
    
    public static void SetMaxThreads(int workerThreads, int completionPortThreads) => ThreadPool.SetMaxThreads(workerThreads, completionPortThreads);
    
    public static void GetMaxThreads(int workerThreads, int completionPortThreads) => ThreadPool.SetMinThreads(workerThreads, completionPortThreads);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void QueueUserWorkItem(WaitCallback callBack, object? state) {
        ThreadPool.QueueUserWorkItem(callBack, state);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void UnsafeQueueUserWorkItem(WaitCallback callBack, object? state) {
        ThreadPool.UnsafeQueueUserWorkItem(callBack, state);
    }
    
#if NET6_0_OR_GREATER

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void UnsafeQueueUserWorkItem(IThreadPoolWorkItem callBack, bool preferLocal) {
        ThreadPool.UnsafeQueueUserWorkItem(callBack, preferLocal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void UnsafeQueueUserWorkItem<TState>(Action<TState> callBack, TState state, bool preferLocal) {
        ThreadPool.UnsafeQueueUserWorkItem(callBack, state, preferLocal);
    }
    
#endif
    
}