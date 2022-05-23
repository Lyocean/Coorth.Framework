using System;
using System.Threading.Tasks;
using Coorth.Tasks;

namespace Coorth.Framework;

public sealed class ReactionSystemFunction<T> : Reaction<T> where T : notnull {
    
    private readonly Func<T, ValueTask> action;

    public ReactionSystemFunction(IReactionContainer container, Func<T, ValueTask> action) : base(container) {
        this.action = action;
    }

    public override void Execute(in T e) => action(e).Forget();

    public override ValueTask ExecuteAsync(in T e) => action(e);
}

public sealed class ReactionSystemFunction<TContext, T> : Reaction<TContext, T> where T : notnull {
    
    private readonly Func<TContext, T, ValueTask> action;

    public ReactionSystemFunction(IReactionContainer container, Func<TContext, T, ValueTask> action) : base(container) {
        this.action = action;
    }

    public override void Execute(TContext context, in T e) => action(context, e).Forget();

    public override ValueTask ExecuteAsync(TContext context, in T e) => action(context, e);
}