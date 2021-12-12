using System;
using System.Threading.Tasks;

namespace Coorth {
    public class EventSlot<T> {
        
        private TaskCompletionSource<T> source;
        
        private T value;
        
        private bool hasValue;

        public bool HasPromise => source != null;
        
        public Task<T> ReceiveAsync() {
            if (hasValue) {
                var task = Task.FromResult(value);
                hasValue = false;
                value = default;
                return task;
            }
            if (source != null) {
                throw new InvalidOperationException("Can't receive twice");
            }
            source = new TaskCompletionSource<T>();
            return source.Task;
        }

        public void Trigger(in T e) {
            if (source != null) {
                var taskSource = source;
                source = null;
                taskSource.SetResult(e);
            }
            else {
                this.value = e;
                this.hasValue = true;      
            }
        }
    }
}