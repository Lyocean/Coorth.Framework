using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Framework;

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