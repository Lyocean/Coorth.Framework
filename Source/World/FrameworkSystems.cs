using Coorth.Common;

namespace Coorth {
    [System, StoreContract("7DA743A9-D0CB-4E81-9BB9-474928962016")]
    public class FrameworkSystems : SystemBase {
        protected override void OnAdd() {
            AddSystem<HierarchySystem>();
            AddSystem<TransformSystem>();
            AddSystem<ActiveSystem>();
            AddSystem<LifetimeSystem>();
            AddSystem<SandboxSystem>();
            AddSystem<ScriptSystem>();
            AddSystem<ActorSystem>();
            AddSystem<DebugSystem>();
            AddSystem<FolderSystem>();
        }
    }
}