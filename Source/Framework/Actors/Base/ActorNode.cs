using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Coorth.Framework;

public abstract class ActorNode : Disposable {
    
    public readonly ActorId Id;
    
    public readonly ActorPath Path;

    public IActor Actor { get; }

    public IActorProcessor? Processor { get; }

    public abstract ActorDomain Domain { get; }

    public abstract ActorsRuntime Runtime { get; }

    public readonly ActorNode? Parent;
    
    private static readonly Dictionary<ActorId, ActorNode> empty = new();
    private Dictionary<ActorId, ActorNode>? children;
    public IReadOnlyDictionary<ActorId, ActorNode> Children => children ?? empty;
    
    public int ChildCount => Children.Count;

    public TActor GetActor<TActor>() => (TActor) Actor;

    public ActorRef Ref => new(this, Id);

    #region Lifecycle
    
    protected ActorNode(ActorId id, string? name, ActorNode? parent, IActor actor, IActorProcessor? processor) {
        Id = id;
        name ??= id.Id.ToString();
        Path = new ActorPath(parent?.Path.FullPath ?? string.Empty, name);
        Parent = parent;
        Actor = actor;
        Processor = processor;
        Parent?.AddChild(this);
    }

    protected void AddChild(ActorNode child) {
        children ??= new Dictionary<ActorId, ActorNode>();
        children.Add(child.Id, child);
        Runtime.OnActorNodeAttach(child.Id, child);
        child.OnAdd();
    }

    public ActorNode GetChild(ActorId id) => Children[id];

    public ActorNode? FindChild(ActorId id) => Children.TryGetValue(id, out var node) ? node : null;
    
    private void RemoveChild(ActorNode child) {
        child.OnRemove();
        Runtime.OnActorNodeDetach(child.Id);
        children?.Remove(child.Id);
    }
    
    protected override void OnDispose(bool dispose) {
        (Actor as IDisposable)?.Dispose();
        Parent?.RemoveChild(this);
        if (children == null) {
            return;
        }
        foreach (var (_, node) in children) {
            node.Dispose();
        }
        children.Clear();
    }
    
    protected virtual void OnAdd() { }

    protected virtual void OnRemove() { }

    #endregion
    
    #region Message

    public void Send(IMessage message) {
        using var context = new MessageContext(this, ActorRef.Null, CancellationToken.None);       
        Receive(in context, in message);
    }

    public async void Send(IMessage message, ActorRef sender) {
        using var context = new MessageContext(this, sender, CancellationToken.None);       
        await Receive(in context, in message);
    }
    
    public async ValueTask<IResponse> Request(IRequest message) {
        var completion = new TaskCompletionSource<IResponse>();
        using var context = new MessageContext(this, ActorRef.Null, completion);       
        await Receive(in context, message);
        var response = await completion.Task;
        return response;
    }

    public ValueTask<IResponse> Request(IRequest message, ActorRef sender) {
        return Request(message, sender, CancellationToken.None);
    }
    
    public async ValueTask<IResponse> Request(IRequest message, ActorRef sender, CancellationToken cancellation) {
        var completion = new TaskCompletionSource<IResponse>(cancellation);
        var context = new MessageContext(this, sender, completion);       
        await Receive(in context, message);
        var response = await completion.Task;
        return response;
    }
    
    public ValueTask<IResponse> Request(IRequest message, ActorRef sender, TimeSpan timeSpan) {
        var cancellation = new CancellationTokenSource(timeSpan);
        return Request(message, sender, cancellation.Token);
    }
    
    protected abstract ValueTask Receive(in MessageContext context, in IMessage message);
    
    #endregion
}