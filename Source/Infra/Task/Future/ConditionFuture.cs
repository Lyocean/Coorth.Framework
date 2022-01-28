using System;

namespace Coorth.Tasks {
    public class ConditionFuture<T>: EventFuture<T> where T: ITickEvent {

        private readonly Func<T, bool> condition;
        
        public ConditionFuture(Func<T, bool> cond, int times) : base(times) {
            this.condition = cond;
        }
        
        public override void Execute(in T e) {
            if (condition.Invoke(e)) {
                base.Execute(in e);
            }
        }
    }
}