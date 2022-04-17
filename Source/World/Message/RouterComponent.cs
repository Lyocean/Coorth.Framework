using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth {
    [Component, DataContract, Guid("857C72EF-0954-4F3E-99ED-E5EC0738B25B")]
    public class RouterComponent : Component {

        #region Fields
        
        private readonly MessageDispatcher<RouterComponent> dispatcher = new MessageDispatcher<RouterComponent>();

        private readonly Dictionary<long, AgentComponent> agents = new Dictionary<long, AgentComponent>();
        
        #endregion

        #region Agents

        private void Register(ActorComponent actor) {
            actor.OnRegister(this);
        }
        
        public void Register(Entity entity, ActorRef actorRef) {
            var actor = entity.Offer<ActorComponent>();
            // actor.Setup(actorRef);
            Register(actor);
        }

        public void UnRegister(ActorComponent actor) {
            actor.UnRegister(this);
        }
        
        public void UnRegister(Entity entity) {
            var actor = entity.Offer<ActorComponent>();
            UnRegister(actor);
        }
        
        public void AddAgent(long agentId, AgentComponent agentComponent) {
            agents.Add(agentId, agentComponent);
        }

        public bool RemoveAgent(long agentId) {
            return agents.Remove(agentId);
        }

        public AgentComponent GetAgent(long agentId) {
            return agents.TryGetValue(agentId, out var agentComponent) ? agentComponent : default;
        }

        public bool TryGetAgent(long agentId, out AgentComponent agentComponent) {
            return agents.TryGetValue(agentId, out agentComponent);
        }

        #endregion

        #region Receive

        private void OnInvalidMessage(IMessage msg, string info) {
            LogUtil.Error(string.Format(info, msg.ToString()));
        }

        public void OnReceive(Type type, IMessage msg) {
            dispatcher.Execute(this, msg);
            if (msg is IRequest request) {
                OnRequest(type, request);
            }
            if (msg is IResponse response) {
                OnResponse(type, response);
            }
        }

        private void OnRequest(Type type, IRequest request) {
            
        }

        private void OnResponse(Type type, IResponse response) {
            // if (!futures.TryGetValue(response.MsgId, out var future)) {
            //     OnInvalidMessage(response, "Can't find request data of response message: %s");
            //     return;
            // }

            // futures.Remove(response.MsgId);
            // var responseType = future.GetResponseType();
            // if (!responseType.IsAssignableFrom(type)) {
            //     OnInvalidMessage(response, "Response message type error: %s");
            // }
            //
            // future.SetResult(response);
        }

        #endregion

        #region Send

        private void _Send<T>(T message) where T : IMessage {
            // session.Remote.Send(message, session.Local.Context.Ref);
        }

        // public void Send<T>(T message) where T : IMessage {
        //     _Send(message);
        // }
        //
        // public Task<TResponse> Query<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest where TResponse : IResponse {
        //     var msgId = ++currentId;
        //     request.Setup(msgId);
        //     var future = new MessageFuture<TResponse>(cancellationToken);
        //     futures.Add(msgId, future);
        //     _Send(request);
        //     return future.Task;
        // }
        //
        // public void Reply<TResponse>(int msgId, TResponse response) where TResponse : IResponse {
        //     response.Setup(msgId);
        //     _Send(response);
        // }
        //
        // public void Reply<TResponse>(IRequest request, TResponse response) where TResponse : IResponse {
        //     Reply(request.MsgId, response);
        // }

        public IDisposable Receive<T>(Action<RouterComponent, T> action) where T : IMessage {
            return dispatcher.Receive(action);
        }

        public IDisposable Receive<T>(Action<AgentComponent, T> action) where T : IAgentMessage {
            return Receive<T>((actor, msg) => {
                var result = actor.TryGetAgent(msg.AgentId, out var agent);
                if (result == false || agent == null || agent.AgentId == 0) {
                    LogUtil.Warning($"Missing agent with id: {msg.AgentId}");
                    return;
                }
                action(agent, msg);
            });
        }
        
        #endregion
    }

}