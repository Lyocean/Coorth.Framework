using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Framework; 

public sealed partial class Dispatcher {

    private readonly ConcurrentDictionary<Type, object> futures = new();

    private ReactFutures<T> GetFutures<T>() where T : ITickEvent {
        var key = typeof(T);
        var channel = OfferChannel(key);
        var reaction = (ReactFutures<T>)futures.GetOrAdd(key, () => new ReactFutures<T>(channel));
        return reaction;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<T> Delay<T>(CancellationToken cancellationToken = default) where T : ITickEvent => GetFutures<T>().Delay(TimeSpan.Zero, 0, cancellationToken);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<T> Delay<T>(TimeSpan time, CancellationToken cancellationToken = default) where T : ITickEvent => GetFutures<T>().Delay(time, 0, cancellationToken);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<T> Delay<T>(int frame, CancellationToken cancellationToken = default) where T : ITickEvent => GetFutures<T>().Delay(TimeSpan.Zero, frame, cancellationToken);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<T> Until<T>(Func<T, bool> condition, int times, CancellationToken cancellationToken = default) where T : ITickEvent => GetFutures<T>().Until(condition, times, cancellationToken);

}
