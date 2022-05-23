using System;
using System.Threading.Tasks;
using Coorth.Tasks;

namespace Coorth.Framework;

public sealed class ReactionSystemTaskFunc<T> : Reaction<T> where T : notnull {
    
    private readonly Func<T, Task> action;

    public ReactionSystemTaskFunc(IReactionContainer container, Func<T, Task> action) : base(container) {
        this.action = action;
    }

    public override void Execute(in T e) => action(e).Forget();

    public override ValueTask ExecuteAsync(in T e) => action(e).ToValueTask();
}

public sealed class ReactionSystemTaskFunc<TContext, T> : Reaction<TContext, T> where T : notnull {
    
    private readonly Func<TContext, T, Task> action;

    public ReactionSystemTaskFunc(IReactionContainer container, Func<TContext, T, Task> action) : base(container) {
        this.action = action;
    }

    public override void Execute(TContext context, in T e) => action(context, e).Forget();

    public override ValueTask ExecuteAsync(TContext context, in T e) => action(context, e).ToValueTask();
}