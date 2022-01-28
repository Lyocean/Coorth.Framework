using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Coorth.Tasks;

namespace Coorth {
    public class EventDispatcher : EventNode {
        
        private readonly Dictionary<Type, EventChannel> channels = new Dictionary<Type, EventChannel>();
        
        private EventChannel<T> OfferChannel<T>() {
            var key = typeof(T);
            if (channels.TryGetValue(key, out var channel)) {
                return (EventChannel<T>)channel;
            }
            channel = new EventChannel<T>(this);
            channels.Add(key, channel);
            return (EventChannel<T>)channel;
        }

        public IEventReaction<T> Subscribe<T>(Action<T> action) {
            return OfferChannel<T>().Subscribe(action);
        }

        // public IEventReaction<T> Subscribe<T>(T e, Action<T> action) {
        //     return OfferChannel<T>().Subscribe(action);
        // }

        public IEventReaction<T> Subscribe<T>(EventAction<T> action) {
            return OfferChannel<T>().Subscribe(action);
        }
        
        public IEventReaction<T> Subscribe<T>(Func<T, Task> action) {
            return OfferChannel<T>().Subscribe(action);
        }
        
        public IEventReaction<T> Subscribe<T>(Func<T, ValueTask> action) {
            return OfferChannel<T>().Subscribe(action);
        }
        
        public IEventReaction<T> Subscribe<T>(EventFunc<T, Task> action) {
            return OfferChannel<T>().Subscribe(action);
        }
        
        public IEventReaction<T> Subscribe<T>(EventFunc<T, ValueTask> action) {
            return OfferChannel<T>().Subscribe(action);
        }
        
        public IEventReaction<T> Subscribe<T>(IEventReaction<T> reaction) {
            return OfferChannel<T>().Subscribe(reaction);
        }
        
#if NET5_0_OR_GREATER
         [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#else
         [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public override void Dispatch<T>(in T e) {
            if (channels.TryGetValue(typeof(T), out var channel)) {
                var reactions = ((EventChannel<T>)channel).Reactions;
                for (var i = 0; i < reactions.Count; i++) {
#if !DEBUG
                    try {
#endif
                    reactions[i].Execute(in e);
#if !DEBUG
                    } catch (Exception exception) {
                        LogUtil.Exception(exception);
                    }
#endif
                }
            }
            base.Dispatch(in e);
        }
        
        public override async ValueTask DispatchAsync<T>(T e) {
            if (channels.TryGetValue(typeof(T), out var channel)) {
                var reactions = ((EventChannel<T>)channel).Reactions;
                for (var i = 0; i < reactions.Count; i++) {
#if !DEBUG
                    try {
#endif
                    await reactions[i].ExecuteAsync(e);
#if !DEBUG
                    } catch (Exception exception) {
                        LogUtil.Exception(exception);
                    }
#endif
                }
            }
            await base.DispatchAsync(e);
        }

        
        public Task<T> ReceiveEvent<T>(int count = 1) {
            var future = new EventFuture<T>(count);
            Subscribe(future);
            return future.Task;
        }
        
        public Task<T> DelayFrame<T>(int count = 1) {
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