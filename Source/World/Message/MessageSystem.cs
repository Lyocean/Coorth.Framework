using System;
using Coorth.Common;

namespace Coorth {
    
    public abstract class MessageSystem : SystemBase {

        protected ActorRuntime Runtime => Singleton<WorldComponent>().World.Actors;

        protected RouterComponent Router => Singleton<RouterComponent>();
        
        protected bool IsDebug => Singleton<SandboxComponent>().IsDebug;
        
        protected bool IsReflectionEnable => Singleton<SandboxComponent>().IsDebug && Singleton<SandboxComponent>().IsReflectionEnable;

        protected void OnReceive<T>(Action<RouterComponent, T> action) where T : IMessage {
            Router.Receive(action).ManageBy(ref Collector);
        }
        
        protected void OnReceive<T>(Action<AgentComponent, T> action) where T : IAgentMessage {
            Router.Receive(action).ManageBy(ref Collector);
        }
    }
}