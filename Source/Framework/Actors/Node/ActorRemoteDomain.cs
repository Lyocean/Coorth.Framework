using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Framework;

public sealed class ActorRemoteDomain : ActorDomain {

    public readonly ISession Session;
    
    public ActorRemoteDomain(string name, ActorsRuntime runtime, ISession session, ActorNode? parent) : base(name, runtime, parent) {
        Session = session;
    }
    
    public override ValueTask ReceiveAsync(ActorContext context, IMessage m) {
        
        
        
        return new ValueTask();
    }

        
    // #region Actor
    //     
    // public ActorRef GetRef(ActorId id) {
    //     var context = Node.GetChild(id);
    //     return context?.Ref ?? ActorRef.Null;
    // }
    //
    // private ActorRef OfferRef(ActorId id) {
    //     var node = Node.FindChild(id);
    //     if (node != null) {
    //         return node.Ref;
    //     }
    //     var name = id.Id.ToString();
    //     node = new ActorRemoteNode(id, this, new ActorPath(Name + "/" + name));
    //     Node.AddChild(node);
    //     return node.Ref;
    // }
    //     
    // public T? CreateProxy<T>(ActorId id, string name) {
    //     var proxy = TypeBinding.Create<T>();
    //     name = string.IsNullOrEmpty(name) ? id.Id.ToString() : name;
    //     var context = new ActorRemoteNode(id, this, new ActorPath(Name + "/" + name));
    //     this.Node.AddChild(context);
    //     return proxy;
    // }
    //
    // public T GetService<T>() where T : IActor {
    //     var guid = typeof(T).GUID;
    //     var actorId = new ActorId(guid);
    //     var context = Node.FindChild(actorId);
    //     if (context != null) {
    //         return context.GetActor<T>();
    //     }
    //     var name = typeof(T).Name;
    //     context = new ActorRemoteNode(actorId, this, new ActorPath(Name + "/" + name));
    //     this.Node.AddChild(context);
    //     return context.GetActor<T>();
    // }
    //     
    // #endregion
    //
    // #region Message
    //
    // public void Send<T>(ActorRef target, ActorRef sender, T message) {
    //     var actorMessage = new ActorMessage(target.Id, sender.Id, (IMessage) message);
    //     Session.Send(actorMessage);
    // }
    //
    // public async Task<TResp> Request<TReq, TResp>(ActorRef target, ActorRef sender, TReq message, CancellationToken cancellation) {
    //     var request = new ActorRequest(target.Id, sender.Id, (IRequest) message);
    //     var response = await Session.Request(request, cancellation);
    //     return (TResp)response;
    // }
    //     
    // public Result OnReceive(ActorMessage message) {
    //     var senderId = message.Sender;
    //     var targetId =  message.Target;
    //     var targetRef = Runtime.GetRef(targetId);
    //     if (targetRef.IsNull) {
    //         return Result.Failure("Can't find actor: " + targetId);
    //     }
    //     var senderRef = OfferRef(senderId);
    //     targetRef.Send(message.Message, senderRef);
    //     return Result.Success();
    // }
    //
    // public Result<Task<IResponse>> OnRequest(ActorRequest request) {
    //     var senderId = request.Sender;
    //     var targetId =  request.Target;
    //     var targetRef = Runtime.GetRef(targetId);
    //     if (targetRef.IsNull) {
    //         return Result.Failure<Task<IResponse>>("Can't find actor: " + targetId);
    //     }
    //     var senderRef = OfferRef(senderId);
    //     var task = targetRef.Request<IRequest, IResponse>(request.Request, senderRef);
    //     return Result.Success(task);
    // }
    //     
    // #endregion
    //



}
    