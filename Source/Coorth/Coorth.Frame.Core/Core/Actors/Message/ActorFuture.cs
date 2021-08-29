using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
 
    
    public abstract class ActorFuture {
    }
    
    public class ActorFuture<T> : ActorFuture {
        public readonly Task<T> Task;
        public object Result;
        private readonly CancellationTokenSource cts;
        private readonly TaskCompletionSource<T> tcs;

        public ActorFuture(CancellationTokenSource cts) {
            this.tcs = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
            this.cts = cts;
            this.cts?.Token.Register(() => {
                if (this.tcs.Task.IsCompleted) {
                    return;
                }
                this.tcs.TrySetCanceled();
            });
            this.Task = this.tcs.Task;
        }
        
        public void Response(T message) {
            tcs.TrySetResult(message);
        }
    }
    
}
