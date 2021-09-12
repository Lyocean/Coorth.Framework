using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
    [Component, DataContract, Guid("E3863A56-B271-4B67-BB8F-9DCD6CEAC2F1")]
    public class AgentComponent : RefComponent {
        public long AgentId { get; private set; }
        public bool IsProxy { get; private set; }
        public bool IsMaster => !IsProxy;
        
        public RouterComponent RouterComponent { get; private set; }

        public ActorComponent ActorComponent { get; private set; }

        public void Setup(RouterComponent routerComponent, long agentId, bool isProxy) {
            AgentId = agentId;
            IsProxy = isProxy;
            RouterComponent = routerComponent;
            RouterComponent.AddAgent(agentId, this);
        }

        public void OnAdd() { }

        public void OnRemove() {
            RouterComponent.RemoveAgent(AgentId);
            AgentId = 0;
        }
        
        public void Send<T>(T message) where T : IMessage, IAgentMessage {
            message.AgentId = this.AgentId;
            ActorComponent.Send(message);
        }

        public Task<TResp> Request<TReq, TResp>(TReq request) where TReq : IRequest, IAgentMessage {
            request.AgentId = this.AgentId;
            return ActorComponent.Request<TReq, TResp>(request);
        }
        
        public Task<TResp> Request<TReq, TResp>(TReq request, CancellationToken cancellation) where TReq : IRequest, IAgentMessage {
            request.AgentId = this.AgentId;
            return ActorComponent.Request<TReq, TResp>(request, cancellation);
        }
        
        public Task<TResp> Request<TReq, TResp>(TReq request, TimeSpan timeout) where TReq : IRequest, IAgentMessage {
            request.AgentId = this.AgentId;
            return ActorComponent.Request<TReq, TResp>(request, timeout);
        }
    }
}