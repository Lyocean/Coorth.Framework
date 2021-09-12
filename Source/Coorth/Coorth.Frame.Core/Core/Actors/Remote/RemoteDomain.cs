using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
    public class RemoteDomain : ActorDomain<RemoteContext>, IAwake<IRemoteSession> {
        private IRemoteSession Session { get; set; }

        public void OnAwake(IRemoteSession session) {
            this.Session = session;
            this.Session.Setup(this);
        }
        
        #region Actor
        
        public ActorRef GetRef(ActorId id) {
            var context = Context.GetChild(id);
            return context?.Ref ?? ActorRef.Null;
        }

        private ActorRef OfferRef(ActorId id) {
            var context = Context.GetChild(id);
            if (context != null) {
                return context.Ref;
            }
            var name = id.ToShortString();
            context = new RemoteContext(id, this, new ActorPath(Name + "/" + name));
            this.Context.AddChild(context);
            return context.Ref;
        }
        
        public T CreateProxy<T>(ActorId id, string name) {
            var proxy = TypeBinding.Create<T>();
            name = string.IsNullOrEmpty(name) ? id.ToShortString() : name;
            var context = new RemoteContext(id, this, new ActorPath(Name + "/" + name));
            this.Context.AddChild(context);
            return proxy;
        }

        public T GetService<T>() where T : IActor {
            var guid = typeof(T).GUID;
            var actorId = new ActorId(guid);
            var context = Context.GetChild(actorId);
            if (context != null) {
                return context.GetActor<T>();
            }
            var name = typeof(T).Name;
            context = new RemoteContext(actorId, this, new ActorPath(Name + "/" + name));
            this.Context.AddChild(context);
            return context.GetActor<T>();
        }
        
        #endregion

        #region Message

        public void Send<T>(ActorRef target, ActorRef sender, T message) {
            var actorMessage = new ActorMessage(target.Id, sender.Id, (IMessage) message);
            Session.Send(actorMessage);
        }

        public async Task<TResp> Request<TReq, TResp>(ActorRef target, ActorRef sender, TReq message, CancellationToken cancellation) {
            var request = new ActorRequest(target.Id, sender.Id, (IRequest) message);
            var response = await Session.Request(request, cancellation);
            return (TResp)response;
        }
        
        public Result OnReceive(ActorMessage message) {
            var senderId = message.Sender;
            var targetId =  message.Target;
            var targetRef = Container.GetRef(targetId);
            if (targetRef.IsNull) {
                return Result.Failure("Can't find actor: " + targetId);
            }
            var senderRef = OfferRef(senderId);
            targetRef.Send(message.Message, senderRef);
            return Result.Success();
        }

        public Result<Task<IResponse>> OnRequest(ActorRequest request) {
            var senderId = request.Sender;
            var targetId =  request.Target;
            var targetRef = Container.GetRef(targetId);
            if (targetRef.IsNull) {
                return Result.Failure<Task<IResponse>>("Can't find actor: " + targetId);
            }
            var senderRef = OfferRef(senderId);
            var task = targetRef.Request<IRequest, IResponse>(request.Request, senderRef);
            return Result.Success(task);
        }
        
        #endregion
    }
    
    public interface IRemoteSession {

        void Setup(RemoteDomain domain);
        
        void Send(ActorMessage message);

        Task<IResponse> Request(ActorRequest request, CancellationToken cancellation);
    }
}