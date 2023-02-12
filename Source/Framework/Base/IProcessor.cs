using System;
using System.Threading.Tasks;

namespace Coorth.Framework;

public interface IProcessor {
    void Execute(Type key, object e);
    ValueTask ExecuteAsync(Type key, object e);
}

public interface IProcessor<in TContext> {
    void Execute(TContext context, Type key, object e);
    ValueTask ExecuteAsync(TContext context, Type key, object e);
}
