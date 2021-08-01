namespace Coorth {
    [System, StoreContract("A46F1768-A306-4670-88AD-DEB2F0663BBB")]
    public class CoreSystems : SystemBase {
        protected override void OnAdd() {
            Sandbox.BindComponent<TransformComponent>();
            Sandbox.BindComponent<PositionComponent>();
            Sandbox.BindComponent<RotationComponent>();
            Sandbox.BindComponent<WorldMatrixComponent>();
            AddSystem<TransformSystem>();
            
            Sandbox.BindComponent<ActiveComponent>();
            AddSystem<ActiveSystem>();
            
            Sandbox.BindComponent<ScriptComponent>();
            AddSystem<ScriptSystem>();
            
            Sandbox.BindComponent<ActorComponent>();
            Sandbox.BindComponent<AgentComponent>();
            AddSystem<ActorSystem>();

            Sandbox.BindComponent<DebugComponent>();
        }

        protected override void OnRemove() {
            RemoveSystem<ActorSystem>();
            RemoveSystem<ScriptSystem>();
            RemoveSystem<ActiveSystem>();
            RemoveSystem<TransformSystem>();
        }
    }
}