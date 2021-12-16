using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Coorth.Tasks {
    public readonly struct SynchronizationContextAwaitable {
        private readonly SynchronizationContext synchronizationContext;
        private readonly CancellationToken cancellationToken;

        public SynchronizationContextAwaitable(SynchronizationContext synchronizationContext, CancellationToken cancellationToken) {
            this.cancellationToken = cancellationToken;
            this.synchronizationContext = synchronizationContext;
        }

        public Awaiter GetAwaiter() => new Awaiter(synchronizationContext, cancellationToken);

        public readonly struct Awaiter : ICriticalNotifyCompletion {
            private static readonly SendOrPostCallback switchToCallback = Callback;
            private static void Callback(object state) => ((Action)state)();

            private readonly SynchronizationContext synchronizationContext;
            private readonly CancellationToken cancellationToken;
            
            public Awaiter(SynchronizationContext synchronizationContext, CancellationToken cancellationToken) {
                this.synchronizationContext = synchronizationContext;
                this.cancellationToken = cancellationToken;
            }
            
            public bool IsCompleted => false;
            public void GetResult() => cancellationToken.ThrowIfCancellationRequested();
            public void OnCompleted(Action continuation) => synchronizationContext.Post(switchToCallback, continuation);
            public void UnsafeOnCompleted(Action continuation) => synchronizationContext.Post(switchToCallback, continuation);
        }
    }
}