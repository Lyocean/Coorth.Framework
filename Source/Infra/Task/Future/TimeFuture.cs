using System;

namespace Coorth.Tasks {
    public class TimeFuture<T> : EventFuture<T> where T: ITickEvent {

        public readonly TimeSpan Duration;

        private TimeSpan elapse;

        public TimeFuture(TimeSpan time) : base(0) {
            this.Duration = time;
        }
        
        public override void Execute(in T e) {
            elapse += e.GetDeltaTime(); 
            if (elapse >= Duration) {
                base.Execute(in e);
            }
        }
    }
}