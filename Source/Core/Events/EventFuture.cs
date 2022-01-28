using System.Threading.Tasks;

namespace Coorth {
    public class EventFuture<T> : EventReaction<T> {

        private int count = 0;
        
        private readonly TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();

        public Task<T> Task => taskCompletionSource.Task;
        
        public EventFuture(int times) {
            count = times;
        }

        public override void Execute(in T e) {
            count--;
            if (count <= 0) {
                taskCompletionSource.SetResult(e);
                this.Dispose();
            }
        }

        public override ValueTask ExecuteAsync(T e) {
            count--;
            if (count <= 0) {
                taskCompletionSource.SetResult(e);
                this.Dispose();
            }
            return System.Threading.Tasks.Task.CompletedTask.ToValueTask();
        }
    }
}