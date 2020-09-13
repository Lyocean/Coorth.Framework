using System.Threading.Tasks;

namespace Coorth.ECS {
    public class Schedular {
        protected readonly EcsContainer container;

        public Schedular(EcsContainer container) {
            this.container = container;
        }

        public virtual void Schedule<T>(Systems systems, T evt) where T : IEvent {
            foreach (var eventSystem in systems.GetEventSystems<T>()) {
                if (eventSystem is IAsyncEventSystem<T> asyncEventSystem) {
                    asyncEventSystem.Execute(evt);
                } else {
                    ((IEventSystem<T>)eventSystem).Execute(evt);
                }
            }
        }

        public virtual async Task ScheduleAsync<T>(Systems systems, T evt) where T : IEvent {
            foreach (var eventSystem in systems.GetEventSystems<T>()) {
                if (eventSystem is IAsyncEventSystem<T> asyncEventSystem) {
                    await asyncEventSystem.Execute(evt);
                } else {
                    ((IEventSystem<T>)eventSystem).Execute(evt);
                }
            }
        }

        public virtual void Schedule<T>(IEventSystem<T> system, T evt) where T : IEvent {
            system.Execute(evt);
        }
    }
}
