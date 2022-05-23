using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Coorth.Framework; 

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