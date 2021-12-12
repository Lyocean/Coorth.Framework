using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth {
    [System, DataContract, Guid("D4144E18-DF68-43E6-8CDE-F601AF95FCB0")]
    public class ActorSystem : MessageSystem {

        #region Common

        protected override void OnAdd() {
            Sandbox.BindComponent<RouterComponent>();
            Sandbox.BindComponent<AgentComponent>();
            
            //Entity Events
            Subscribe<EventComponentAdd<AgentComponent>>(Execute);
            Subscribe<EventComponentRemove<AgentComponent>>(Execute);

            //Actor Message
            ////Entity Message
            OnReceive<MessageCreateEntity>(OnReceive);
            OnReceive<MessageDestroyEntity>(OnReceive);
            ////Component Message
            OnReceive<MessageAddComponent>(OnReceive);
            OnReceive<MessageRemoveComponent>(OnReceive);
            OnReceive<MessageModifyComponent>(OnReceive);
            ////System Message
            OnReceive<MessageAddSystem>(OnReceive);
            OnReceive<MessageRemoveSystem>(OnReceive);
            OnReceive<MessageActiveSystem>(OnReceive);
        }
        
        private static void Execute(EventComponentAdd<AgentComponent> e) {
            e.Component.OnAdd();
        }
        
        private static void Execute(EventComponentRemove<AgentComponent> e) {
            e.Component.OnRemove();
        }

        #endregion


        #region Message

        private void OnReceive(RouterComponent router, MessageCreateEntity msg) {
            if (router.TryGetAgent(msg.AgentId, out var agent)) {
                Debug?.Logger?.LogWarning($"Duplicate agent id: {msg.AgentId}");
                agent.Entity.Dispose();
            }
            Entity entity = Sandbox.CreateEntity();
            agent = entity.Add<AgentComponent>();
            agent.Setup(router, msg.AgentId, true);
        }
        
        private void OnReceive(RouterComponent router, MessageDestroyEntity msg) {
            if (!router.TryGetAgent(msg.AgentId, out var agent)) {
                Debug?.Logger?.LogWarning($"Missing agent with id: {msg.AgentId}");
                return;
            }
            agent.Entity.Dispose();
        }
        
        private void OnReceive(AgentComponent agent, MessageAddComponent msg) {
            agent.Entity.Add(msg.ComponentType);
        }
        
        private void OnReceive(AgentComponent agent, MessageRemoveComponent msg) {
            agent.Entity.Remove(msg.ComponentType);
        }
        
        private void OnReceive(AgentComponent agent, MessageModifyComponent msg) {
            // agent.Entity.Modify()
        }
        
        private void OnReceive(RouterComponent router, MessageAddSystem msg) {
            if (msg.ParentType == null) {
                Sandbox.AddSystem(msg.SystemType, IsReflectionEnable);
            }
            else {
                var parentSystem = Sandbox.GetSystem(msg.ParentType) ?? Sandbox.AddSystem(msg.ParentType);
                parentSystem.AddSystem(msg.SystemType, IsReflectionEnable);
            }
        }
        
        private void OnReceive(RouterComponent router, MessageRemoveSystem msg) {
            Sandbox.RemoveSystem(msg.SystemType);
        }
        
        private void OnReceive(RouterComponent router, MessageActiveSystem msg) {
            Sandbox.ActiveSystem(msg.SystemType, msg.IsActive);
        }

        #endregion
    }
}