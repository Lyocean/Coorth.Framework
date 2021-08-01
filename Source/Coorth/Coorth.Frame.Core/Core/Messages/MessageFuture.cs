using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth {

    public abstract class MessageFuture {
        public abstract Type GetResponseType();

        public abstract void SetResult(object result);
    }
    
    public class MessageFuture<TResponse> : MessageFuture {
        
        private readonly TaskCompletionSource<TResponse> taskCompletionSource = new TaskCompletionSource<TResponse>();

        public Task<TResponse> Task => taskCompletionSource.Task;

        private readonly CancellationToken cancellationToken;
        
        public override Type GetResponseType() {
            return typeof(TResponse);
        }
        
        public MessageFuture(CancellationToken ct) {
            cancellationToken = ct;
        }
        
        public void SetResult(TResponse response) {
            taskCompletionSource.SetResult(response);
        }
        
        public void SetCancel() {
#if NET5_0_OR_GREATER
        taskCompletionSource.SetCanceled(cancellationToken);
#else
        taskCompletionSource.SetCanceled();
#endif
        }
        
        public override void SetResult(object result) {
            if (cancellationToken.IsCancellationRequested) {
                SetCancel();
                return;
            }
            SetResult((TResponse) result);
        }
    }
}