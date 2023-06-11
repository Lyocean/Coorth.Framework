using System;
using System.Threading.Tasks;

namespace Coorth.Framework; 

public static class ReactionExtension {
    
    public static Reaction<T> Subscribe<T>(this IReactionContainer container, ActionI1<T> action) where T : notnull {
        var reaction = new ReactionAction<T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<T> Subscribe<T>(this IReactionContainer container, Action<T> action) where T : notnull {
        var reaction = new ReactionSystemAction<T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<T> Subscribe<T>(this IReactionContainer container, FuncI1<T, ValueTask> action) where T : notnull {
        var reaction = new ReactionFunction<T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<T> Subscribe<T>(this IReactionContainer container, FuncI1<T, Task> action) where T : notnull {
        var reaction = new ReactionTaskFunc<T>(container, action);
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
    
    public static Reaction<TContext, T> Subscribe<TContext, T>(this IReactionContainer container, ActionI2<TContext, T> action) where T : notnull {
        var reaction = new ReactionAction<TContext, T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<TContext,T> Subscribe<TContext,T>(this IReactionContainer container, Action<TContext, T> action) where T : notnull {
        var reaction = new ReactionSystemAction<TContext, T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<TContext,T> Subscribe<TContext,T>(this IReactionContainer container, FuncI2<TContext, T, ValueTask> action) where T : notnull {
        var reaction = new ReactionEventFunction<TContext, T>(container, action);
        container.Add(reaction);
        return reaction;
    }
    
    public static Reaction<TContext,T> Subscribe<TContext,T>(this IReactionContainer container, FuncI2<TContext, T, Task> action) where T : notnull {
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