using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Tasks; 

public static class TaskUtil {
    
    private static Dispatcher Dispatcher => Dispatcher.Root;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> Delay<T>(CancellationToken cancellation = default) where T : ITickEvent => Dispatcher.Delay<T>(cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> Delay(CancellationToken cancellation = default) => Dispatcher.Delay<EventTickUpdate>(cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> NextFrame() => Delay(CancellationToken.None);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> Delay<T>(TimeSpan time, CancellationToken cancellation = default) where T : ITickEvent => Dispatcher.Delay<T>(time, cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> Delay(TimeSpan time, CancellationToken cancellation = default) => Dispatcher.Delay<EventTickUpdate>(time, cancellation);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> Delay<T>(int frame, CancellationToken cancellation = default) where T : ITickEvent => Dispatcher.Delay<T>(frame, cancellation);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> Delay(int frame, CancellationToken cancellation = default) => Dispatcher.Delay<EventTickUpdate>(frame, cancellation);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> Until<T>(Func<T, bool> condition, int times, CancellationToken cancellation = default) where T : ITickEvent => Dispatcher.Until(condition, times, cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> Until(Func<EventTickUpdate, bool> condition, int times, CancellationToken cancellation = default) => Dispatcher.Until<EventTickUpdate>(condition, times, cancellation);

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