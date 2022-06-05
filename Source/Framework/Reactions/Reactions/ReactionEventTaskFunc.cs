using System.Threading.Tasks;
using Coorth.Tasks;

namespace Coorth.Framework;

public sealed class ReactionEventTaskFunc<T> : Reaction<T> where T : notnull {
    
    private readonly EventFunc<T, Task> action;

    public ReactionEventTaskFunc(IReactionContainer container, EventFunc<T, Task> action) : base(container) {
        this.action = action;
    }

    public override void Execute(in T e) => action(e).Forget();

    public override ValueTask ExecuteAsync(in T e) => action(e).ToValueTask();
}

public sealed class ReactionEventTaskFunc<TContext, T> : Reaction<TContext, T> where T : notnull {
    
    private readonly EventFunc<TContext, T, Task> action;

    public ReactionEventTaskFunc(IReactionContainer container, EventFunc<TContext, T, Task> action) : base(container) {
        this.action = action;
    }

    public override void Execute(TContext context, in T e) => action(context, e).Forget();

    public override ValueTask ExecuteAsync(TContext context, in T e) => action(context, e).ToValueTask();
}