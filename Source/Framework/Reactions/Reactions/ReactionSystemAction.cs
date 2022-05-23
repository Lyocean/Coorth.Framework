using System;
using System.Threading.Tasks;
using Coorth.Tasks;

namespace Coorth.Framework;

public sealed class ReactionSystemAction<T> : Reaction<T> where T : notnull {
    private readonly Action<T> action;

    public ReactionSystemAction(IReactionContainer container, Action<T> action) : base(container) {
        this.action = action;
    }

    public override void Execute(in T e) => action(e);

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

    public override void Execute(TContext context, in T e) => action(context, e);

    public override ValueTask ExecuteAsync(TContext context, in T e) {
        action(context, e);
#if NET5_0_OR_GREATER
        return ValueTask.CompletedTask;
#else
         return Task.CompletedTask.ToValueTask();
#endif
    }
}