using System.Collections.Generic;

namespace Coorth {
    public class DestroySystem : SystemBase {

        private readonly HashSet<Entity> entities = new HashSet<Entity>();

        protected override void OnAdd() {
            Subscribe<EventLateUpdate>().ForEachRef<DestroyComponent>(Execute);
            Subscribe<EventEndOfFrame>(Execute);
        }

        private void Execute(in EventLateUpdate e, Entity entity, ref DestroyComponent component) {
            if (component.DelayFrameCount <= 0) {
                entities.Add(entity);
            }
            else {
                component.DelayFrameCount--;
            }
        }
        
        private void Execute(EventEndOfFrame e) {
            if (entities.Count == 0) {
                return;
            }
            foreach (var entity in entities) {
                if (entity.TryGet(out DestroyComponent component) && component.RecycleFactory != null) {
                    component.RecycleFactory.Recycle(entity);
                    
                } else {
                    entity.Dispose();
                }
            }
            entities.Clear();
        }
    }
}