using System;
using System.Threading.Tasks;

namespace Coorth.Tasks; 

public partial struct TaskJob {
    
    public static async ValueTask RunAsync(Action action, bool configureAwait = true) {
        if (!configureAwait) {
            await ToPool();
            action();
            return;
        }
        await using var scope = await EnterPool();
        action();
    }
    
    public static async ValueTask<T> RunAsync<T>(Func<T> action, bool configureAwait = true) {
        if (!configureAwait) {
            await ToPool();
            return action();
        }
        await using var scope = await EnterPool();
        return action();
    }
    
    public static async ValueTask<TReturn> RunAsync<TState, TReturn>(Func<TState, TReturn> action, TState state, bool configureAwait = true) {
        if (!configureAwait) {
            await ToPool();
            return action(state);
        }
        await using var scope = await EnterPool();
        return action(state);
    }
}