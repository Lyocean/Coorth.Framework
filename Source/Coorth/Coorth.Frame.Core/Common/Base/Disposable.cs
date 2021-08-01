using System;
using System.Collections.Generic;
using System.Threading;

namespace Coorth {
    public abstract class Disposable : IDisposable {
        
        private volatile int disposed = 0;

        public bool IsDisposed => disposed != 0;
        
        public void Dispose() {
            if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        ~Disposable() {
            if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
                Dispose(false);
            }
        }
        
        protected virtual void Dispose(bool dispose) {
            
        }
    }

    public struct Disposables<T> : IDisposable where T: IDisposable {
        
        private List<T> disposables;
        
        public void Add(T disposable) {
            if (disposables == null) {
                disposables = new List<T>();
            }
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
    
    public struct Disposables: IDisposable {
        
        private List<IDisposable> disposables;

        public void Add(IDisposable disposable) {
            if (disposables == null) {
                disposables = new List<IDisposable>();
            }
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
}