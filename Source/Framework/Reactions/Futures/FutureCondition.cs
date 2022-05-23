using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Coorth.Framework;

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