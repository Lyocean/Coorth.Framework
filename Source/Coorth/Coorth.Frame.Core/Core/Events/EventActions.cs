using System.Threading.Tasks;

namespace Coorth {
    public class EventActions : Disposable, IEventNode {
        
        public EventId ProcessId { get; } = EventId.New();
        
        public IEventNode Parent { get; set; }

        public void Execute<T>(in T e) where T : IEvent {
            throw new System.NotImplementedException();
        }

        public Task ExecuteAsync<T>(T e) where T : IEvent {
            throw new System.NotImplementedException();
        }

        public void AddAction<T>() where T : IEvent {
            
        }
    }
}