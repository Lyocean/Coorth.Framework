using System;
using System.Collections.Generic;

namespace Coorth; 

public abstract class Disposable : IDisposable {
        
    private bool disposed;
    public bool IsDisposed => disposed;
        
    public void Dispose() {
        if (disposed) {
            return;
        }
        disposed = true;
        GC.SuppressFinalize(this);
        OnDispose();
    }

    ~Disposable() {
        if (disposed) {
            return;
        }
        disposed = true;
        OnDispose();
    }
        
    protected virtual void OnDispose() {
    }
}


public struct Disposables: IDisposable {
        
    private List<IDisposable> disposables;

    public void Add(IDisposable disposable) {
        disposables ??= new List<IDisposable>();
        disposables.Add(disposable);
    }

    public void Clear() {
        if (disposables == null) {
            return;
        }
        foreach (var disposable in disposables) {
            disposable.Dispose();
        }
        disposables.Clear();
    }

    public void Dispose() {
        Clear();
    }
}
    
public static class DisposableExtension {

    public static void ManageBy(this IDisposable disposable, ref Disposables disposables) {
        disposables.Add(disposable);
    }
        
    public static T ManageBy<T>(this T disposable, ref Disposables disposables) where T : IDisposable {
        disposables.Add(disposable);
        return disposable;
    }
        
    public static T ManageBy<T>(this T disposable, ref Disposables<T> disposables) where T : IDisposable {
        disposables.Add(disposable);
        return disposable;
    }
}

public class DisposeAction : IDisposable {
    private readonly Action action;
    public DisposeAction(Action action) => this.action = action;
    public void Dispose() => action.Invoke();
}