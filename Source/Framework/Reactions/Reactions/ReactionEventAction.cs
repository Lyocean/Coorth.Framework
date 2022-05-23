using System;
using System.Threading.Tasks;
using Coorth.Tasks;

namespace Coorth.Framework;

public sealed class ReactionEventAction<T> : Reaction<T> where T : notnull {
    
    private readonly EventAction<T> action;

    public ReactionEventAction(IReactionContainer container, EventAction<T> action) : base(container) {
        this.action = action;
    }

    public override void Execute(in T e) => action(in e);

    public override ValueTask ExecuteAsync(in T e) {
        action(e);
#if NET5_0_OR_GREATER
        return ValueTask.CompletedTask;
#else
         return Task.CompletedTask.ToValueTask();
#endif
    }
}


public sealed class ReactionEventAction<TContext, T> : Reaction<TContext, T> where T : notnull {
    
    private readonly EventAction<TContext, T> action;

    public ReactionEventAction(IReactionContainer container, EventAction<TContext, T> action) : base(container) {
        this.action = action;
    }

    public override void Execute(TContext context, in T e) => action(context, in e);

    public override ValueTask ExecuteAsync(TContext context, in T e) {
        action(context, in e);
#if NET5_0_OR_GREATER
        return ValueTask.CompletedTask;
#else
         return Task.CompletedTask.ToValueTask();
#endif
    }
}

