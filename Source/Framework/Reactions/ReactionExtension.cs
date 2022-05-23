using System;
using System.Threading.Tasks;

namespace Coorth.Framework; 

public static class ReactionExtension {
    
    public static Reaction<T> Subscribe<T>(this IReactionContainer container, EventAction<T> action) where T : notnull {
        var reaction = new ReactionEventAction<T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<T> Subscribe<T>(this IReactionContainer container, Action<T> action) where T : notnull {
        var reaction = new ReactionSystemAction<T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<T> Subscribe<T>(this IReactionContainer container, EventFunc<T, ValueTask> action) where T : notnull {
        var reaction = new ReactionEventFunction<T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<T> Subscribe<T>(this IReactionContainer container, EventFunc<T, Task> action) where T : notnull {
        var reaction = new ReactionEventTaskFunc<T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<T> Subscribe<T>(this IReactionContainer container, Func<T, ValueTask> action) where T : notnull {
        var reaction = new ReactionSystemFunction<T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<T> Subscribe<T>(this IReactionContainer container, Func<T, Task> action) where T : notnull {
        var reaction = new ReactionSystemTaskFunc<T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<TContext, T> Subscribe<TContext, T>(this IReactionContainer container, EventAction<TContext, T> action) where T : notnull {
        var reaction = new ReactionEventAction<TContext, T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<TContext,T> Subscribe<TContext,T>(this IReactionContainer container, Action<TContext, T> action) where T : notnull {
        var reaction = new ReactionSystemAction<TContext, T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<TContext,T> Subscribe<TContext,T>(this IReactionContainer container, EventFunc<TContext, T, ValueTask> action) where T : notnull {
        var reaction = new ReactionEventFunction<TContext, T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<TContext,T> Subscribe<TContext,T>(this IReactionContainer container, EventFunc<TContext, T, Task> action) where T : notnull {
        var reaction = new ReactionEventTaskFunc<TContext, T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<TContext,T> Subscribe<TContext,T>(this IReactionContainer container, Func<TContext, T, ValueTask> action) where T : notnull {
        var reaction = new ReactionSystemFunction<TContext, T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<TContext,T> Subscribe<TContext, T>(this IReactionContainer container, Func<TContext, T, Task> action) where T : notnull {
        var reaction = new ReactionSystemTaskFunc<TContext, T>(container, action);
        container.Add(reaction);
        return reaction;
    }
}