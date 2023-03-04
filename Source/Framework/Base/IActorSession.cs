using System;
using System.Threading.Tasks;

namespace Coorth.Framework; 

public interface IActorSession : IActor, IActorProcessor {
    void OnReceive(Action<IMessage> message);
    void Send(IMessage message);
    ValueTask<IResponse> Request(IMessage message);
}

public interface IActorSession<in TContext> {
    void OnReceive(TContext context, Action<IMessage> message);
    void Send(TContext context, IMessage message);
    ValueTask<IResponse> Request(TContext context, IMessage message);
}