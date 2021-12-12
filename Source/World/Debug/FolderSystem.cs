namespace Coorth.Common {
    [System, StoreContract("4C94F9C9-C178-4E34-9755-DA59E08D96DF")]
    public class FolderSystem : SystemBase {
        
        protected override void OnAdd() {
            Sandbox.BindComponent<FolderComponent>();
        }
        
        protected override void OnActive() {
            Subscribe<EventEntityRemove>(Execute);
        }

        private void Execute(in EventEntityRemove e) {
            var entity = e.Entity;
            if (!entity.Has<HierarchyComponent>()) {
                Singleton<FolderComponent>().RemoveEntity(entity);
                return;
            }
            ref var hierarchy = ref entity.Get<HierarchyComponent>();
            var parent = hierarchy.ParentEntity;
            if (parent.IsNull) {
                Singleton<FolderComponent>().RemoveEntity(entity);
            }
            else {
                if (parent.TryGet<FolderComponent>(out var folder)) {
                    folder.RemoveEntity(entity);
                }
            }
        }
    }
}