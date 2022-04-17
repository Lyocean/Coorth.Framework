using System;
using System.Collections.Generic;
using System.Threading;

namespace Coorth {
    public abstract class Disposable : IDisposable {
        
        private volatile int disposed;
        public bool IsDisposed => disposed != 0;
        
        public void Dispose() {
            if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
                GC.SuppressFinalize(this);
                OnDispose(true);
            }
        }

        ~Disposable() {
            if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
                OnDispose(false);
            }
        }
        
        protected virtual void OnDispose(bool dispose) {
        }
    }

    
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
}