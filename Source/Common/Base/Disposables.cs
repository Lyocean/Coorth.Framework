using System;
using System.Collections.Generic;

namespace Coorth;

public struct Disposables<T> : IDisposable where T: IDisposable {
        
    private List<T> disposables;
        
    public void Add(T disposable) {
        disposables ??= new List<T>();
        disposables.Add(disposable);
    }

    private void Clear() {
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