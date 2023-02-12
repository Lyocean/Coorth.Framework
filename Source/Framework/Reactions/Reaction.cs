using System;
using System.Threading.Tasks;

namespace Coorth.Framework;

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