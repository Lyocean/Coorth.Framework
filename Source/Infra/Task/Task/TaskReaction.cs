using System;
using System.Collections.Concurrent;

namespace Coorth {
    public class TaskReaction {
        
        public ConcurrentDictionary<Type, ConcurrentQueue<Action>> queues = new ConcurrentDictionary<Type, ConcurrentQueue<Action>>();

        public void AddContinuation(Type type, Action continuation) {
            var queue = queues.GetOrAdd(type, _ => new ConcurrentQueue<Action>());
            queue.Enqueue(continuation);
        }
    }
    
}