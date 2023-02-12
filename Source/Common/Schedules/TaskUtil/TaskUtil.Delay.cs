// using System;
// using System.Runtime.CompilerServices;
// using System.Threading;
// using System.Threading.Tasks;
// using Coorth.Framework;
//
// namespace Coorth.Tasks; 
//
// public static partial class TaskUtil {
//     
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<T> NextFrame<T>(Router router) where T : ITickEvent => router.Delay<T>(1, CancellationToken.None);
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<T> NextFrame<T>() where T : ITickEvent => Router.Root.Delay<T>(1, CancellationToken.None);
//     
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<TickUpdateEvent> NextFrame(Router router) => router.Delay<TickUpdateEvent>(TimeSpan.Zero, CancellationToken.None);
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<TickUpdateEvent> NextFrame() => Router.Root.Delay<TickUpdateEvent>(TimeSpan.Zero, CancellationToken.None);
//     
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<T> Delay<T>(Router router, TimeSpan time, CancellationToken cancellation = default) where T : ITickEvent => router.Delay<T>(time, cancellation);
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<T> Delay<T>(TimeSpan time, CancellationToken cancellation = default) where T : ITickEvent => Router.Root.Delay<T>(time, cancellation);
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<TickUpdateEvent> Delay(Router router, TimeSpan time, CancellationToken cancellation = default) => router.Delay<TickUpdateEvent>(time, cancellation);
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<TickUpdateEvent> Delay(TimeSpan time, CancellationToken cancellation = default) => Router.Root.Delay<TickUpdateEvent>(time, cancellation);
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<T> Delay<T>(Router router, int frame, CancellationToken cancellation = default) where T : ITickEvent => router.Delay<T>(frame, cancellation);
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<T> Delay<T>(int frame, CancellationToken cancellation = default) where T : ITickEvent => Router.Root.Delay<T>(frame, cancellation);
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<TickUpdateEvent> Delay(Router router, int frame, CancellationToken cancellation = default) => router.Delay<TickUpdateEvent>(frame, cancellation);
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<TickUpdateEvent> Delay(int frame, CancellationToken cancellation = default) => Router.Root.Delay<TickUpdateEvent>(frame, cancellation);
//
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<T> Until<T>(Router router, Func<T, bool> condition, int times, CancellationToken cancellation = default) where T : ITickEvent => router.Until(condition, times, cancellation);
//     
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<T> Until<T>(Func<T, bool> condition, int times, CancellationToken cancellation = default) where T : ITickEvent => Router.Root.Until(condition, times, cancellation);
//     
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<TickUpdateEvent> Until(Router router, Func<TickUpdateEvent, bool> condition, int times, CancellationToken cancellation = default) => router.Until(condition, times, cancellation);
//     
//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static ValueTask<TickUpdateEvent> Until(Func<TickUpdateEvent, bool> condition, int times, CancellationToken cancellation = default) => Router.Root.Until(condition, times, cancellation);
// }