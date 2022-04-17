using System;
using System.Threading.Tasks;

namespace Coorth {
    public partial class Module<TKey> {

        protected Disposables Managed;

        protected Disposables Actives;

        protected ref Disposables Collector => ref (IsActive ? ref Actives :  ref Managed);
        
        public void Subscribe<T>(Action<T> action) {
            var reaction = Dispatcher.Subscribe(action);
            Collector.Add(reaction);
        }
        
        public void Subscribe<T>(EventAction<T> action) {
            var reaction = Dispatcher.Subscribe(action);
            Collector.Add(reaction);
        }
        
        public void Subscribe<T>(Func<T, Task> action) {
            var reaction = Dispatcher.Subscribe(action);
            Collector.Add(reaction);
        }
        
        public void Subscribe<T>(EventDispatcher dispatcher, Action<T> action) {
            var reaction = dispatcher.Subscribe(action);
            Collector.Add(reaction);
        }
        
        public void Subscribe<T>(EventDispatcher dispatcher, Func<T, Task> action) {
            var reaction = dispatcher.Subscribe(action);
            Collector.Add(reaction);
        }
        
        
    }
}