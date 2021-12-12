using System;

namespace Coorth {
    
    public abstract class MessageSystem : SystemBase {

        protected ActorContainer Container => Singleton<WorldComponent>().World.Actors;

        protected RouterComponent Router => Singleton<RouterComponent>();
        
        protected bool IsDebug => Sandbox.Singleton().Has<DebugComponent>();

        protected DebugComponent Debug => IsDebug ? Singleton<DebugComponent>() : default;
        
        protected bool IsReflectionEnable => IsDebug && Sandbox.Singleton().Get<DebugComponent>().IsReflectionEnable;

        protected void OnReceive<T>(Action<RouterComponent, T> action) where T : IMessage {
            Router.Receive(action).ManageBy(ref Collector);
        }
        
        protected void OnReceive<T>(Action<AgentComponent, T> action) where T : IAgentMessage {
            Router.Receive(action).ManageBy(ref Collector);
        }
    }
}