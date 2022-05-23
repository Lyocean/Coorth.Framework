using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Coorth.Framework; 

public sealed partial class Dispatcher : Disposable {

    public static readonly Dispatcher Root = new(null!);
    
    private readonly Dispatcher parent;

    private readonly List<Dispatcher> children = new();

    private readonly Dictionary<Type, ReactChannel> channels = new();

    private Dispatcher(Dispatcher parent) {
        this.parent = parent;
    }

    public Dispatcher CreateChild() {
        var child = new Dispatcher(this);
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
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public void Dispatch<T>(in T e) where T: notnull {
        if (channels.TryGetValue(typeof(T), out var channel)) {
            foreach (var reaction in channel.Reactions) {
                ((Reaction<T>)reaction).Execute(in e);
            }
        }
        foreach (var child in children) {
            child.Dispatch(in e);
        }
    }
    
#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public void Dispatch(Type key, object e) {
        if (channels.TryGetValue(key, out var channel)) {
            foreach (var reaction in channel.Reactions) {
                ((IProcessor)reaction).Execute(key, e);
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
    public async ValueTask DispatchAsync<T>(T e) where T: notnull {
        if (channels.TryGetValue(typeof(T), out var channel)) {
            foreach (var reaction in channel.Reactions) {
                await ((Reaction<T>)reaction).ExecuteAsync(e);
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
                await ((IProcessor)reaction).ExecuteAsync(key, e);
            }
        }
        foreach (var child in children) {
            await child.DispatchAsync(key, e);
        }
    }
}

public sealed partial class Dispatcher<TContext> : Disposable {
    
    private readonly Dispatcher<TContext>? parent;

    private readonly List<Dispatcher<TContext>> children = new();

    private readonly Dictionary<Type, ReactChannel> channels = new();
    
    public Dispatcher() {
        parent = null;
    }

    private Dispatcher(Dispatcher<TContext> parent) {
        this.parent = parent;
    }
    
    public Dispatcher<TContext> CreateChild() {
        var child = new Dispatcher<TContext>(this);
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
                ((Reaction<TContext, T>)reaction).Execute(context, in e);
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
                ((IProcessor<TContext>)reaction).Execute(context, key, e);
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
                await ((Reaction<TContext, T>)reaction).ExecuteAsync(context, e);
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
                await ((IProcessor<TContext>)reaction).ExecuteAsync(context,key, e);
            }
        }
        foreach (var child in children) {
            await child.DispatchAsync(context, key, e);
        }
    }
}