using Coorth.Logs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Framework;

public sealed partial class Dispatcher : Disposable {

    public static readonly Dispatcher Root = new(null!);

    private readonly Dispatcher parent;

    private readonly List<Dispatcher> children = new();

    private readonly Dictionary<Type, ReactChannel> channels = new();

    public Dispatcher(Dispatcher parent) {
        this.parent = parent;
    }

    public Dispatcher CreateChild() {
        var child = new Dispatcher(this);
        children.Add(child);
        return child;
    }

    protected override void OnDispose() {
        foreach (var child in children) {
            child.Dispose();
        }
        children.Clear();
        foreach (var (_, channel) in channels) {
            channel.Dispose();
        }
        channels.Clear();
        parent.children.Remove(this);
    }

    private ReactChannel OfferChannel(Type key) {
        if (channels.TryGetValue(key, out var channel)) {
            return channel;
        }
        channel = new ReactChannel(key);
        channels.Add(key, channel);
        return channel;
    }

#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Dispatch<T>(in T e) where T: notnull {
        if (channels.TryGetValue(typeof(T), out var channel)) {
            foreach (var reaction in CollectionsMarshal.AsSpan<Reaction>(channel.Reactions)) {
                try {
                    ((Reaction<T>)reaction).Execute(in e);
                }
                catch (AggregateException exception) {
                    if (exception.InnerException != null) {
                        LogUtil.Exception(exception.InnerException);
                    }
                    else {
                        LogUtil.Exception(exception);
                    }
                }
                catch (Exception exception) {
                    LogUtil.Exception(exception);
                }
            }
        }
        foreach (var child in CollectionsMarshal.AsSpan<Dispatcher>(children)) {
            child.Dispatch(in e);
        }
    }
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispatch<T>(in T e) where T : notnull {
        if (channels.TryGetValue(typeof(T), out var channel)) {
            foreach (var reaction in channel.Reactions) {
                try {
                    ((Reaction<T>)reaction).Execute(in e);
                } catch (Exception exception) {
                    LogUtil.Exception(exception);
                }
            }
        }
        foreach (var child in children) {
            child.Dispatch(in e);
        }
    }
#endif


#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public void Dispatch(Type key, object e) {
        if (channels.TryGetValue(key, out var channel)) {
            foreach (var reaction in channel.Reactions) {
                try {
                    ((IProcessor)reaction).Execute(key, e);
                } catch (Exception exception) {
                    LogUtil.Exception(exception);
                }
            }
        }
        foreach (var child in children) {
            child.Dispatch(key, e);
        }
    }

#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public async ValueTask DispatchAsync<T>(T e) where T : notnull {
        if (channels.TryGetValue(typeof(T), out var channel)) {
            foreach (var reaction in channel.Reactions) {
                try {
                    await ((Reaction<T>)reaction).ExecuteAsync(e);
                } catch (Exception exception) {
                    LogUtil.Exception(exception);
                }
            }
        }

        foreach (var child in children) {
            await child.DispatchAsync(e);
        }
    }

#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public async ValueTask DispatchAsync(Type key, object e) {
        if (channels.TryGetValue(key, out var channel)) {
            foreach (var reaction in channel.Reactions) {
                try {
                    await ((IProcessor)reaction).ExecuteAsync(key, e);
                } catch (Exception exception) {
                    LogUtil.Exception(exception);
                }
            }
        }
        foreach (var child in children) {
            await child.DispatchAsync(key, e);
        }
    }
}


public sealed class ReactChannel : IReactionContainer, IDisposable {

    public readonly Type Key;

    public readonly List<Reaction> Reactions = new();

    public ReactChannel(Type key) {
        Key = key;
    }

    public void Add(Reaction reaction) {
        Reactions.Add(reaction);
    }

    public void Remove(ReactionId id) {
        Reactions.RemoveAll(_ => _.Id == id);
    }

    public void Dispose() {
        foreach (var reaction in Reactions) {
            reaction.Dispose();
        }
    }
}

public interface IReactionContainer {
    void Add(Reaction reaction);
    void Remove(ReactionId id);
}


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

public partial class Dispatcher {

    public Reaction<T> Subscribe<T>(EventAction<T> action) where T : notnull {
        System.Diagnostics.Debug.Assert(action != null);
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }

    public Reaction<T> Subscribe<T>(Action<T> action) where T : notnull {
        System.Diagnostics.Debug.Assert(action != null);
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }

    public Reaction<T> Subscribe<T>(EventFunc<T, ValueTask> action) where T : notnull {
        System.Diagnostics.Debug.Assert(action != null);
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }

    public Reaction<T> Subscribe<T>(Func<T, ValueTask> action) where T : notnull {
        System.Diagnostics.Debug.Assert(action != null);
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }

    public Reaction<T> Subscribe<T>(EventFunc<T, Task> action) where T : notnull {
        System.Diagnostics.Debug.Assert(action != null);
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }

    public Reaction<T> Subscribe<T>(Func<T, Task> action) where T : notnull {
        System.Diagnostics.Debug.Assert(action != null);
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
}


