using System;
using System.Threading.Tasks;

namespace Coorth.Framework; 

public interface ISession {
    
    void OnReceive(Action<IMessage> message);
    
    void Send(IMessage message);
    
    ValueTask<IResponse> Request(IMessage message);
}

public interface ISession<in TContext> {
    
    void OnReceive(TContext context, Action<IMessage> message);
    
    void Send(TContext context, IMessage message);
    
    ValueTask<IResponse> Request(TContext context, IMessage message);
}