using System;

namespace Coorth {
    public abstract class MessageSystem : SystemBase {
        
        protected ActorContainer Container => World.Actors;
        protected ActorComponent Actor => Singleton<ActorComponent>();
        
        protected bool IsDebug => Sandbox.Singleton().Has<DebugComponent>();

        protected DebugComponent Debug => IsDebug ? Singleton<DebugComponent>() : default;
        
        protected bool IsReflectionEnable => IsDebug && Sandbox.Singleton().Get<DebugComponent>().IsReflectionEnable;

        protected void Receive<T>(Action<ActorComponent, T> action) where T : IMessage {
            Actor.Receive(action).ManageBy(ref Managed);
        }
        
        protected void Receive<T>(Action<AgentComponent, T> action) where T : IAgentMessage {
            Actor.Receive(action).ManageBy(ref Managed);
        }
    }
    
    [System, StoreContract("D4144E18-DF68-43E6-8CDE-F601AF95FCB0")]
    public class ActorSystem : MessageSystem {

        #region Common

        protected override void OnAdd() {
            //Entity Events
            Subscribe<EventComponentAdd<AgentComponent>>(Execute);
            Subscribe<EventComponentRemove<AgentComponent>>(Execute);

            //Actor Message
            ////Entity Message
            Receive<MessageCreateEntity>(OnReceive);
            Receive<MessageDestroyEntity>(OnReceive);
            ////Component Message
            Receive<MessageAddComponent>(OnReceive);
            Receive<MessageRemoveComponent>(OnReceive);
            Receive<MessageModifyComponent>(OnReceive);
            ////System Message
            Receive<MessageAddSystem>(OnReceive);
            Receive<MessageRemoveSystem>(OnReceive);
            Receive<MessageActiveSystem>(OnReceive);
        }
        
        private static void Execute(EventComponentAdd<AgentComponent> e) {
            e.Component.OnAdd();
        }
        
        private static void Execute(EventComponentRemove<AgentComponent> e) {
            e.Component.OnRemove();
        }

        #endregion


        #region Message

        private void OnReceive(ActorComponent actor, MessageCreateEntity msg) {
            if (actor.TryGetAgent(msg.AgentId, out var agent)) {
                Debug?.Logger?.LogWarning($"Duplicate agent id: {msg.AgentId}");
                agent.Entity.Dispose();
            }
            Entity entity = Sandbox.CreateEntity();
            agent = entity.Add<AgentComponent>();
            agent.Setup(actor, msg.AgentId, true);
        }
        
        private void OnReceive(ActorComponent actor, MessageDestroyEntity msg) {
            if (!actor.TryGetAgent(msg.AgentId, out var agent)) {
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
        
        private void OnReceive(ActorComponent actor, MessageAddSystem msg) {
            if (msg.ParentType == null) {
                Sandbox.AddSystem(msg.SystemType, IsReflectionEnable);
            }
            else {
                var parentSystem = Sandbox.GetSystem(msg.ParentType) ?? Sandbox.AddSystem(msg.ParentType);
                parentSystem.AddSystem(msg.SystemType, IsReflectionEnable);
            }
        }
        
        private void OnReceive(ActorComponent actor, MessageRemoveSystem msg) {
            Sandbox.RemoveSystem(msg.SystemType);
        }
        
        private void OnReceive(ActorComponent actor, MessageActiveSystem msg) {
            Sandbox.ActiveSystem(msg.SystemType, msg.IsActive);
        }

        #endregion
        

    }
}