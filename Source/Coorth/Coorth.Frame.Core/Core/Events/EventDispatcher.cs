using System;
using System.Threading.Tasks;

namespace Coorth {
    using Coorth.Tasks;
    public class EventDispatcher : EventNode {

        public Task<T> ReceiveEvent<T>(int count = 1) where T : IEvent {
            var future = new EventFuture<T>(count);
            Subscribe(future);
            return future.Task;
        }
        
        public Task<T> DelayFrame<T>(int count = 1) where T : ITickEvent {
            return ReceiveEvent<T>(count);
        }

        public Task DelayFrame(int count = 1) => DelayFrame<EventTickUpdate>(count);

        public Task<T> DelayTime<T>(TimeSpan duration) where T : ITickEvent {
            var future = new TimeFuture<T>(duration);
            Subscribe(future);
            return future.Task;
        }
        
        public Task DelayTime(TimeSpan duration) => DelayTime<EventTickUpdate>(duration);

        public Task<T> UntilCondition<T>(Func<T, bool> condition, int times = 1)where T : ITickEvent {
            var future = new ConditionFuture<T>(condition,times);
            Subscribe(future);
            return future.Task;
        }

        public Task UntilCondition(Func<bool> condition, int matchTimes = 1) => UntilCondition<EventTickUpdate>(_ => condition(), matchTimes);

    }
}