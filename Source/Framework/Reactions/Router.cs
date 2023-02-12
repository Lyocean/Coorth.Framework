
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Coorth.Logs;

namespace Coorth.Framework;

public partial class Router<TContext> : Disposable {
    
    private readonly Router<TContext>? parent;

    private readonly List<Router<TContext>> children = new();

    private readonly Dictionary<Type, ReactChannel> channels = new();
    
    public Router() {
        parent = null;
    }

    private Router(Router<TContext> parent) {
        this.parent = parent;
    }
    
    public Router<TContext> CreateChild() {
        var child = new Router<TContext>(this);
        children.Add(child);
        return child;
    }

    protected override void OnDispose(bool disposing) {
        foreach (var child in children) {
            child.Dispose();
        }
        children.Clear();
        foreach (var (_, channel) in channels) {
            channel.Dispose();
        }
        channels.Clear();
        parent?.children.Remove(this);
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
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public void Dispatch<T>(TContext context, in T e) where T: notnull {
        if (channels.TryGetValue(typeof(T), out var channel)) {
            foreach (var reaction in channel.Reactions) {
                try {
                    ((Reaction<TContext, T>)reaction).Execute(context, in e);
                }
                catch (Exception exception) {
                    LogUtil.Exception(exception);
                }
            }
        }
        foreach (var child in children) {
            child.Dispatch(context, in e);
        }
    }

#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public void Dispatch(TContext context, Type key, object e) {
        if (channels.TryGetValue(key, out var channel)) {
            foreach (var reaction in channel.Reactions) {
                try {
                    ((IProcessor<TContext>)reaction).Execute(context, key, e);
                }
                catch (Exception exception) {
                    LogUtil.Exception(exception);
                }
            }
        }
        foreach (var child in children) {
            child.Dispatch(context, key, e);
        }
    }
    
#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public async ValueTask DispatchAsync<T>(TContext context, T e) where T: notnull {
        if (channels.TryGetValue(typeof(T), out var channel)) {
            foreach (var reaction in channel.Reactions) {
                try {
                    await ((Reaction<TContext, T>)reaction).ExecuteAsync(context, e);
                }
                catch (Exception exception) {
                    LogUtil.Exception(exception);
                }
            }
            
        }
        foreach (var child in children) {
            await child.DispatchAsync(context, e);
        }
    }
    
#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public async ValueTask DispatchAsync(TContext context, Type key, object e) {
        if (channels.TryGetValue(key, out var channel)) {
            foreach (var reaction in channel.Reactions) {
                try {
                    await ((IProcessor<TContext>)reaction).ExecuteAsync(context,key, e);
                }
                catch (Exception exception) {
                    LogUtil.Exception(exception);
                }
            }
        }
        foreach (var child in children) {
            await child.DispatchAsync(context, key, e);
        }
    }
}


public partial class Router<TContext> {
    
    public Reaction<TContext, T> Subscribe<T>(EventAction<TContext, T> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
    
    public Reaction<TContext, T> Subscribe<T>(Action<TContext, T> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
    
    public Reaction<TContext, T> Subscribe<T>(EventFunc<TContext, T, ValueTask> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
    
    public Reaction<TContext, T> Subscribe<T>(Func<TContext, T, ValueTask> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
    
    public Reaction<TContext, T> Subscribe<T>(EventFunc<TContext, T, Task> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
    
    public Reaction<TContext, T> Subscribe<T>(Func<TContext, T, Task> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
}

public sealed class Router : Router<MessageContext> {
    
}