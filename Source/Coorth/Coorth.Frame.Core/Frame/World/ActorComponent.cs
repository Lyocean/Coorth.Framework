using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
    [Component, StoreContract("857C72EF-0954-4F3E-99ED-E5EC0738B25B")]
    public class ActorComponent : RefComponent {

        #region Fields

        private readonly MessageDispatcher<ActorComponent> dispatcher = new MessageDispatcher<ActorComponent>();

        private readonly Dictionary<long, AgentComponent> agents = new Dictionary<long, AgentComponent>();

        private readonly Dictionary<int, MessageFuture> futures = new Dictionary<int, MessageFuture>();

        private int currentId = 0;

        #endregion

        #region Agents

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
            if (!futures.TryGetValue(response.MsgId, out var future)) {
                OnInvalidMessage(response, "Can't find request data of response message: %s");
                return;
            }

            futures.Remove(response.MsgId);
            var responseType = future.GetResponseType();
            if (!responseType.IsAssignableFrom(type)) {
                OnInvalidMessage(response, "Response message type error: %s");
            }

            future.SetResult(response);
        }

        #endregion

        #region Send

        private void _Send<T>(T message) where T : IMessage {
            
        }

        public void Send<T>(T message) where T : IMessage {
            _Send(message);
        }

        public Task<TResponse> Query<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest where TResponse : IResponse {
            var msgId = ++currentId;
            request.Setup(msgId);
            var future = new MessageFuture<TResponse>(cancellationToken);
            futures.Add(msgId, future);
            _Send(request);
            return future.Task;
        }

        public void Reply<TResponse>(int msgId, TResponse response) where TResponse : IResponse {
            response.Setup(msgId);
            _Send(response);
        }

        public void Reply<TResponse>(IRequest request, TResponse response) where TResponse : IResponse {
            Reply(request.MsgId, response);
        }

        public IDisposable Receive<T>(Action<ActorComponent, T> action) where T : IMessage {
            return dispatcher.Receive(action);
        }

        public IDisposable Receive<T>(Action<AgentComponent, T> action) where T : IAgentMessage {
            return Receive<T>((actor, msg) => {
                var result = actor.TryGetAgent(msg.AgentId, out var agent);
                if (result == false || agent == null || agent.AgentId == 0) {
                    var debug = Sandbox.Singleton<DebugComponent>();
                    debug?.Logger.LogWarning($"Missing agent with id: {msg.AgentId}");
                    return;
                }
                action(agent, msg);
            });
        }
        
        #endregion
    }

    [Component, StoreContract("E3863A56-B271-4B67-BB8F-9DCD6CEAC2F1")]
    public class AgentComponent : RefComponent {
        public long AgentId { get; private set; }
        public bool IsProxy { get; private set; }
        public bool IsMaster => !IsProxy;
        public ActorComponent ActorComponent { get; private set; }

        public void Setup(ActorComponent actorComponent, long agentId, bool isProxy) {
            AgentId = agentId;
            IsProxy = isProxy;
            ActorComponent = actorComponent;

            ActorComponent.AddAgent(agentId, this);
        }

        public void OnAdd() {
        }

        public void OnRemove() {
            ActorComponent.RemoveAgent(AgentId);
            AgentId = 0;
        }

        public void Send<T>(T message) where T : IMessage, IAgentMessage {
            message.Setup(AgentId);
            ActorComponent.Send(message);
        }

        public Task<TResponse> Query<TRequest, TResponse>(TRequest message, CancellationToken cancellationToken = default) where TRequest : IRequest, IAgentMessage where TResponse : IResponse {
            // message.Setup(AgentId);
            return ActorComponent.Query<TRequest, TResponse>(message, cancellationToken);
        }

        public void Reply<TResponse>(int msgId, TResponse message) where TResponse : IResponse, IAgentMessage {
            message.Setup(AgentId);
            ActorComponent.Reply(msgId, message);
        }

        public void Reply<TResponse>(IRequest request, TResponse message) where TResponse : IResponse, IAgentMessage {
            message.Setup(AgentId);
            ActorComponent.Reply(request, message);
        }
    }
}