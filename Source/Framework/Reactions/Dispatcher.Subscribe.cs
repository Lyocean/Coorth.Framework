using System;
using System.Threading.Tasks;

namespace Coorth.Framework; 

public partial class Dispatcher {

    public Reaction<T> Subscribe<T>(EventAction<T> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
    
    public Reaction<T> Subscribe<T>(Action<T> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
    
    public Reaction<T> Subscribe<T>(EventFunc<T, ValueTask> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
    
    public Reaction<T> Subscribe<T>(Func<T, ValueTask> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
    
    public Reaction<T> Subscribe<T>(EventFunc<T, Task> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
    
    public Reaction<T> Subscribe<T>(Func<T, Task> action) where T : notnull {
        var channel = OfferChannel(typeof(T));
        return channel.Subscribe(action);
    }
}

public partial class Dispatcher<TContext> {
    
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
