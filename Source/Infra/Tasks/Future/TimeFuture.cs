using System;

namespace Coorth.Tasks {
    public class TimeFuture<T> : EventFuture<T> where T: ITickEvent {

        public TimeSpan duration;

        private TimeSpan elapse;

        public TimeFuture(TimeSpan time) : base(0) {
            this.duration = time;
        }
        
        public override void Execute(in T e) {
            elapse += e.GetDeltaTime(); 
            if (elapse >= duration) {
                taskCompletionSource.SetResult(e);
                this.Dispose();
            }
        }
    }
}