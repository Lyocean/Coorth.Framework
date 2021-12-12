using System;
using System.Threading.Tasks;

namespace Coorth {
    public class EventActions : Disposable, IEventNode {
        
        public EventId ProcessId { get; } = EventId.New();
        
        public IEventNode Parent { get; set; }

        public void Dispatch<T>(in T e) {
            throw new System.NotImplementedException();
        }

        public Task DispatchAsync<T>(T e) {
            throw new System.NotImplementedException();
        }

        public void AddAction<T>() {
        }
    }
}