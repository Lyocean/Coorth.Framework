using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Tasks; 

public partial struct TaskJob {
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> NextFrame<T>(Dispatcher dispatcher) where T : ITickEvent => dispatcher.Delay<T>(1, CancellationToken.None);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> NextFrame<T>() where T : ITickEvent => Dispatcher.Root.Delay<T>(1, CancellationToken.None);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> NextFrame(Dispatcher dispatcher) => dispatcher.Delay<EventTickUpdate>(TimeSpan.Zero, CancellationToken.None);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> NextFrame() => Dispatcher.Root.Delay<EventTickUpdate>(TimeSpan.Zero, CancellationToken.None);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> Delay<T>(Dispatcher dispatcher, TimeSpan time, CancellationToken cancellation = default) where T : ITickEvent => dispatcher.Delay<T>(time, cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> Delay<T>(TimeSpan time, CancellationToken cancellation = default) where T : ITickEvent => Dispatcher.Root.Delay<T>(time, cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> Delay(Dispatcher dispatcher, TimeSpan time, CancellationToken cancellation = default) => dispatcher.Delay<EventTickUpdate>(time, cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> Delay(TimeSpan time, CancellationToken cancellation = default) => Dispatcher.Root.Delay<EventTickUpdate>(time, cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> Delay<T>(Dispatcher dispatcher, int frame, CancellationToken cancellation = default) where T : ITickEvent => dispatcher.Delay<T>(frame, cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> Delay<T>(int frame, CancellationToken cancellation = default) where T : ITickEvent => Dispatcher.Root.Delay<T>(frame, cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> Delay(Dispatcher dispatcher, int frame, CancellationToken cancellation = default) => dispatcher.Delay<EventTickUpdate>(frame, cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> Delay(int frame, CancellationToken cancellation = default) => Dispatcher.Root.Delay<EventTickUpdate>(frame, cancellation);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> Until<T>(Dispatcher dispatcher, Func<T, bool> condition, int times, CancellationToken cancellation = default) where T : ITickEvent => dispatcher.Until(condition, times, cancellation);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<T> Until<T>(Func<T, bool> condition, int times, CancellationToken cancellation = default) where T : ITickEvent => Dispatcher.Root.Until(condition, times, cancellation);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> Until(Dispatcher dispatcher, Func<EventTickUpdate, bool> condition, int times, CancellationToken cancellation = default) => dispatcher.Until(condition, times, cancellation);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask<EventTickUpdate> Until(Func<EventTickUpdate, bool> condition, int times, CancellationToken cancellation = default) => Dispatcher.Root.Until(condition, times, cancellation);
}