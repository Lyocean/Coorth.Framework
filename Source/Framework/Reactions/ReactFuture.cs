using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Framework;

public class ReactFutures<T> : Reaction<T>  where T: ITickEvent {

    private readonly ConcurrentQueue<FutureTimePoint<T>> timeFutures = new();
    
    private readonly ConcurrentQueue<FutureCondition<T>> condFutures = new();

    public ReactFutures(IReactionContainer container) : base(container) {
    }

    public async ValueTask<T> Delay(TimeSpan time, int count, CancellationToken cancellationToken) {
        var future = ClassPool.Create<FutureTimePoint<T>>() ?? new FutureTimePoint<T>();
        future.Setup(time, count, cancellationToken);
        timeFutures.Enqueue(future);
        var e = await future;
        future.Clear();
        ClassPool.Return(future);
        return e;
    }
    
    public async ValueTask<T> Until(Func<T, bool> function, int count, CancellationToken cancellationToken) {
        var future = ClassPool.Create<FutureCondition<T>>() ?? new FutureCondition<T>();
        future.Setup(function, count, cancellationToken);
        condFutures.Enqueue(future);
        var e = await future;
        future.Clear();
        ClassPool.Return(future);
        return e;
    }
    
    public override void Execute(in T e) {
        var deltaTime = e.GetDeltaTime();
        var timeArray = ArrayPool<FutureTimePoint<T>>.Shared.Rent(timeFutures.Count);
        timeFutures.CopyTo(timeArray, 0);
        timeFutures.Clear();
        foreach (var future in timeArray) {
            future.Duration -= deltaTime;
            future.Count--;
            if (future.Duration > TimeSpan.Zero || future.Count > 0) {
                timeFutures.Enqueue(future);
            }
            else {
                future.SetComplete(e);
            }
        }
        
        ArrayPool<FutureTimePoint<T>>.Shared.Return(timeArray, true);
        
        var condArray = ArrayPool<FutureCondition<T>>.Shared.Rent(condFutures.Count);
        condFutures.CopyTo(condArray, 0);
        condFutures.Clear();
        foreach (var future in condArray) {
            if (future.Condition == null) {
                continue;
            }
            if (!future.Condition(e)) {
                condFutures.Enqueue(future);
            } else if (future.Count > 1) {
                future.Count--;
                condFutures.Enqueue(future);
            }
            else {
                future.SetComplete(e);
            }
        }
        ArrayPool<FutureCondition<T>>.Shared.Return(condArray, true);
    }

    public override ValueTask ExecuteAsync(in T e) {
        Execute(in e);
        return new ValueTask();
    }
}
