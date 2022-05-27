using System;
using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Worlds; 

public abstract class MessageSystem : SystemBase {
    
    protected RouterComponent Router => Singleton<RouterComponent>();

    protected bool IsDebug => Singleton<DirectorComponent>().IsDebug;

    protected void OnReceive<T>(Action<ActorContext, T> action) where T: IMessage {
        Router.OnReceive(action).ManageBy(ref Collector);
    }
    
    protected void OnReceive<T>(Action<RouterComponent, ActorContext, T> action) where T: IMessage {
        Router.OnReceive<T>((context, message) => action(Router, context, message)).ManageBy(ref Collector);
    }
    
    protected void OnReceive<T>(Func<ActorContext, T, ValueTask> action) where T: IRequest {
        Router.OnReceive(action).ManageBy(ref Collector);
    }
    
    protected void OnReceive<T>(Func<RouterComponent, ActorContext, T, ValueTask> action) where T: IRequest {
        Router.OnReceive<T>((context, message) => action(Router, context, message)).ManageBy(ref Collector);
    }
    
    protected void OnReceive<T>(Action<AgentComponent, ActorContext, T> action) {

    }
    
    protected void OnReceive<T>(Action<RouterComponent, ActorContext, T, ValueTask> action) {
        
    }
}