using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coorth.Logs;

namespace Coorth.Framework;

public sealed class ActorsRuntime : IActor {

    public readonly Dispatcher Dispatcher;
    
    public readonly ServiceLocator Services;

    public readonly ActorRoot Root;

    private ILogger Logger { get; }

    private readonly ConcurrentDictionary<ActorId, ActorNode> nodes = new();

    private readonly ConcurrentDictionary<ActorPath, ActorId> paths = new();

    public ActorsRuntime Runtime => this;

    #region Lifecycle

    public ActorsRuntime(Dispatcher dispatcher, ServiceLocator services, ILogger logger) {
        Dispatcher = dispatcher;
        Services   = services;
        Root       = new ActorRoot(this);
        Logger     = logger;
    }

    #endregion

    #region Domain
    
    public ActorLocalDomain CreateDomain(string? name = null) {
        var domain = new ActorLocalDomain(name, this, Root);
        return domain;
    }

    public ActorRemoteDomain CreateDomain(string name, ISession session) {
        var domain = new ActorRemoteDomain(name, this, session, Root);
        return domain;
    }

    public ActorDomain GetDomain(string name) {
        return FindDomain(name) ?? throw new KeyNotFoundException(name);
    }
    
    public ActorDomain? FindDomain(string name) {
        foreach (var (_, node) in Root.Children) {
            if (node is ActorDomain domain && node.Path.Name == name.AsSpan()) {
                return domain;
            }
        }
        return null;
    }
    
    #endregion
    
    #region Node

    internal void OnActorNodeAttach(ActorId id, ActorNode node) {
        nodes.TryAdd(id, node);
    }

    internal void OnActorNodeDetach(ActorId id) {
        nodes.TryRemove(id, out _);
    }

    public ActorRef GetRef(ActorId id) {
        if(nodes.TryGetValue(id, out var context)) {
            return context.Ref;
        }
        return ActorRef.Null;
    }
    
    #endregion

    #region Message

    internal void ThroughputOverflow(ActorLocalNode node) {
        Logger.Error($"Actor {node.Id} throughput overflow: {node.Mailbox.Reader.Count}");
    }
    
    internal void OnMailOverflow(ActorLocalNode node, ActorMail mail) {
        Logger.Error($"Actor {node.Id} mailbox overflow: {node.Mailbox.Reader.Count}");
    }

    public ValueTask ReceiveAsync(MessageContext context, IMessage m) {
        Logger.Log(LogLevel.Error, $"Actor runtime receive message: {m}");
        return new ValueTask();
    }

    #endregion
    
}