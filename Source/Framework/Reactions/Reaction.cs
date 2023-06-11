using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Coorth.Tasks;

namespace Coorth.Framework;

#region Abstract

public abstract class Reaction : IDisposable {
    
    public readonly ReactionId Id = ReactionId.New();

    public readonly IReactionContainer Container;

    protected Reaction(IReactionContainer container) {
        Container = container;
    }
    
    public void Dispose() {
        Container.Remove(Id);
    }
}

public abstract class Reaction<T> : Reaction, IProcessor where T : notnull {
    
    protected Reaction(IReactionContainer container) : base(container) {
    }

    public abstract void Execute(scoped in T e);

    public abstract ValueTask ExecuteAsync(in T e);

    public void Execute(Type key, object e) => Execute((T)e);

    public ValueTask ExecuteAsync(Type key, object e) => ExecuteAsync((T)e);
}

public abstract class Reaction<TContext, T> : Reaction, IProcessor<TContext> where T : notnull {
    
    protected Reaction(IReactionContainer container) : base(container) {
    }
    
    public void Execute(TContext context, Type key, object e) => Execute(context, (T)e);

    public abstract void Execute(TContext context, in T e);
    
    public ValueTask ExecuteAsync(TContext context, Type key, object e) => ExecuteAsync(context, (T)e);

    public abstract ValueTask ExecuteAsync(TContext context, in T e);
}

#endregion


#region Action

public sealed class ReactionAction<T> : Reaction<T> where T : notnull {
    
    private readonly ActionI1<T> action;

    public ReactionAction(IReactionContainer container, ActionI1<T> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(in T e) => action(in e);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ValueTask ExecuteAsync(in T e) {
        action(e);
#if NET5_0_OR_GREATER
        return ValueTask.CompletedTask;
#else
         return Task.CompletedTask.ToValueTask();
#endif
    }
}

public sealed class ReactionAction<TContext, T> : Reaction<TContext, T> where T : notnull {
    
    private readonly ActionI2<TContext, T> action;

    public ReactionAction(IReactionContainer container, ActionI2<TContext, T> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(TContext context, in T e) => action(context, in e);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ValueTask ExecuteAsync(TContext context, in T e) {
        action(context, in e);
#if NET5_0_OR_GREATER
        return ValueTask.CompletedTask;
#else
         return Task.CompletedTask.ToValueTask();
#endif
    }
}

public sealed class ReactionSystemAction<T> : Reaction<T> where T : notnull {
    private readonly Action<T> action;

    public ReactionSystemAction(IReactionContainer container, Action<T> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(in T e) => action(e);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ValueTask ExecuteAsync(in T e) {
        action(e);
#if NET5_0_OR_GREATER
        return ValueTask.CompletedTask;
#else
        return Task.CompletedTask.ToValueTask();
#endif
    }
}

public sealed class ReactionSystemAction<TContext, T> : Reaction<TContext, T> where T : notnull {
    private readonly Action<TContext, T> action;

    public ReactionSystemAction(IReactionContainer container, Action<TContext, T> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(TContext context, in T e) => action(context, e);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ValueTask ExecuteAsync(TContext context, in T e) {
        action(context, e);
#if NET5_0_OR_GREATER
        return ValueTask.CompletedTask;
#else
        return Task.CompletedTask.ToValueTask();
#endif
    }
}

#endregion


#region Function

public sealed class ReactionFunction<T> : Reaction<T> where T : notnull {
    
    private readonly FuncI1<T, ValueTask> action;

    public ReactionFunction(IReactionContainer container, FuncI1<T, ValueTask> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(in T e) => action(e).Forget();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ValueTask ExecuteAsync(in T e) => action(e);
}

public sealed class ReactionEventFunction<TContext, T> : Reaction<TContext, T> where T : notnull {
    
    private readonly FuncI2<TContext, T, ValueTask> action;

    public ReactionEventFunction(IReactionContainer container, FuncI2<TContext, T, ValueTask> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(TContext context, in T e) => action(context, e).Forget();

    public override ValueTask ExecuteAsync(TContext context, in T e) => action(context, e);
}


public sealed class ReactionSystemFunction<T> : Reaction<T> where T : notnull {
    
    private readonly Func<T, ValueTask> action;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReactionSystemFunction(IReactionContainer container, Func<T, ValueTask> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(in T e) => action(e).Forget();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ValueTask ExecuteAsync(in T e) => action(e);
}

public sealed class ReactionSystemFunction<TContext, T> : Reaction<TContext, T> where T : notnull {
    
    private readonly Func<TContext, T, ValueTask> action;

    public ReactionSystemFunction(IReactionContainer container, Func<TContext, T, ValueTask> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(TContext context, in T e) => action(context, e).Forget();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ValueTask ExecuteAsync(TContext context, in T e) => action(context, e);
}

#endregion


#region TaskFunc

public sealed class ReactionTaskFunc<T> : Reaction<T> where T : notnull {
    
    private readonly FuncI1<T, Task> action;

    public ReactionTaskFunc(IReactionContainer container, FuncI1<T, Task> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(in T e) => action(e).Forget();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ValueTask ExecuteAsync(in T e) => action(e).ToValueTask();
}

public sealed class ReactionEventTaskFunc<TContext, T> : Reaction<TContext, T> where T : notnull {
    
    private readonly FuncI2<TContext, T, Task> action;

    public ReactionEventTaskFunc(IReactionContainer container, FuncI2<TContext, T, Task> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(TContext context, in T e) => action(context, e).Forget();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ValueTask ExecuteAsync(TContext context, in T e) => action(context, e).ToValueTask();
}

public sealed class ReactionSystemTaskFunc<T> : Reaction<T> where T : notnull {
    
    private readonly Func<T, Task> action;

    public ReactionSystemTaskFunc(IReactionContainer container, Func<T, Task> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(in T e) => action(e).Forget();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ValueTask ExecuteAsync(in T e) => action(e).ToValueTask();
}

public sealed class ReactionSystemTaskFunc<TContext, T> : Reaction<TContext, T> where T : notnull {
    
    private readonly Func<TContext, T, Task> action;

    public ReactionSystemTaskFunc(IReactionContainer container, Func<TContext, T, Task> action) : base(container) {
        this.action = action;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Execute(TContext context, in T e) => action(context, e).Forget();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ValueTask ExecuteAsync(TContext context, in T e) => action(context, e).ToValueTask();
}

#endregion
