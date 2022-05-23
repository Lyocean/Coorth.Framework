using System.Threading.Tasks;
using Coorth.Tasks;

namespace Coorth.Framework;

public sealed class ReactionEventFunction<T> : Reaction<T> where T : notnull {
    
    private readonly EventFunc<T, ValueTask> action;

    public ReactionEventFunction(IReactionContainer container, EventFunc<T, ValueTask> action) : base(container) {
        this.action = action;
    }

    public override void Execute(in T e) => action(e).Forget();

    public override ValueTask ExecuteAsync(in T e) => action(e);
}

public sealed class ReactionEventFunction<TContext, T> : Reaction<TContext, T> where T : notnull {
    
    private readonly EventFunc<TContext, T, ValueTask> action;

    public ReactionEventFunction(IReactionContainer container, EventFunc<TContext, T, ValueTask> action) : base(container) {
        this.action = action;
    }

    public override void Execute(TContext context, in T e) => action(context, e).Forget();

    public override ValueTask ExecuteAsync(TContext context, in T e) => action(context, e);
}