﻿namespace Coorth.Framework; 

public interface ICondition {
    bool Check(object host, IGraphContext context);
}

public interface ICondition<TContext> : ICondition where TContext : INodeContext {
    bool Check(object host, in TContext context);
}