using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
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


public record FutureCondition<T> : ICriticalNotifyCompletion {
    
    public Func<T, bool>? Condition;
    
    public CancellationToken CancellationToken;

    private Action? continuation;

    public int Count;

    private T? value;

    public FutureCondition<T> GetAwaiter() => this;
    
    public bool IsCompleted { get; private set; }
    
    public T GetResult() => value ?? throw new NullReferenceException();
    
    public void Setup(Func<T, bool> cond, int count, CancellationToken cancellationToken) {
        IsCompleted = false;
        Condition = cond;
        Count = count;
        CancellationToken = cancellationToken;
    }
    
    public void Clear() {
        Condition = null;
        CancellationToken = CancellationToken.None;
    }
    
    public void SetComplete(T v) {
        value = v;
        IsCompleted = true;
        if (CancellationToken.IsCancellationRequested) {
            return;
        }
        continuation?.Invoke();
    }

    public void OnCompleted(Action action) {
        continuation = action;
    }

    public void UnsafeOnCompleted(Action action) {
        continuation = action;
    }
}


public class FutureMessage : ICriticalNotifyCompletion {
    
    public bool IsCompleted { get; private set; }

    public FutureMessage GetAwaiter() => this;
   
    private Action? continuation;

    private IMessage? value;
    
    public CancellationToken CancellationToken;

    public IMessage GetResult() => value ?? throw new NullReferenceException();

    public void Clear() {
        CancellationToken = CancellationToken.None;
    }
    
    public void SetComplete(IMessage v) {
        value = v;
        IsCompleted = true;
        if (CancellationToken.IsCancellationRequested) {
            return;
        }
        continuation?.Invoke();
    }
    
    public void OnCompleted(Action action) {
        continuation = action;
    }

    public void UnsafeOnCompleted(Action action) {
        continuation = action;
    }
}


public record FutureTimePoint<T> : ICriticalNotifyCompletion {
    
    public TimeSpan Duration;
    
    public int Count;
    
    public CancellationToken CancellationToken;

    private Action? continuation;

    private T? value;

    public FutureTimePoint<T> GetAwaiter() => this;
    
    public bool IsCompleted { get; private set; }
    
    public void Setup(TimeSpan duration, int count, CancellationToken cancellationToken) {
        IsCompleted = false;
        Duration = duration;
        Count = count;
        CancellationToken = cancellationToken;
    }
    
    public void Clear() {
        Duration = TimeSpan.Zero;
        Count = 0;
        CancellationToken = CancellationToken.None;
    }

    public T GetResult() => value ?? throw new NullReferenceException();

    public void SetComplete(T v) {
        value = v;
        IsCompleted = true;
        continuation?.Invoke();
    }
    
    public void OnCompleted(Action action) {
        continuation = action;
    }

    public void UnsafeOnCompleted(Action action) {
        IsCompleted = true;
        continuation = action;
    }
}