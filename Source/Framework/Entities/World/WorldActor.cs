using System;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Serialize;

namespace Coorth.Framework;

public interface IWorldActor : IActor {
    [DataMethod(1)] ValueTask AddSystem(Type type);
    [DataMethod(2)] ValueTask AddSystems(params Type[] types);
    [DataMethod(3)] ValueTask<bool> HasSystem(Type type);
    [DataMethod(4)] ValueTask<bool> HasSystems(params Type[] types);
    [DataMethod(5)] ValueTask RemoveSystem(Type type);
    [DataMethod(6)] ValueTask RemoveSystems(params Type[] types);
    [DataMethod(7)] ValueTask Send<TMessage>(TMessage message) where TMessage : notnull;
    [DataMethod(8)] ValueTask<TResponse> Request<TRequest, TResponse>(TRequest request) where TRequest : notnull;
}

public sealed class WorldActor : ActorBase, IWorldActor {

    public readonly World World;

    public WorldActor(World world) {
        World = world;
    }

    public override ValueTask ReceiveAsync(MessageContext context, IMessage message) {
        return World.DispatchAsync(context, message);
    }

    public ValueTask AddSystem(Type type) {
        World.AddSystem(type);
        return new ValueTask();
    }

    public ValueTask AddSystems(params Type[] types) {
        foreach (var type in types) {
            World.AddSystem(type);
        }
        return new ValueTask();
    }

    public ValueTask<bool> HasSystem(Type type) {
        return new ValueTask<bool>(World.HasSystem(type));
    }

    public ValueTask<bool> HasSystems(params Type[] types) {
        foreach (var type in types) {
            if (!World.HasSystem(type)) {
                return new ValueTask<bool>(false);
            }
        }
        return new ValueTask<bool>(true);
    }

    public ValueTask RemoveSystem(Type type) {
        World.RemoveSystem(type);
        return new ValueTask();
    }

    public ValueTask RemoveSystems(params Type[] types) {
        foreach (var type in types) {
            World.RemoveSystem(type);
        }
        return new ValueTask();
    }
    
    public ValueTask Send<TMessage>(TMessage message) where TMessage : notnull {
        var context = new MessageContext(Node, ActorRef.Null, CancellationToken.None);
        World.Router.Dispatch(context, message);
        return new ValueTask();
    }
    
    public async ValueTask<TResponse> Request<TRequest, TResponse>(TRequest request) where TRequest : notnull {
        var source = new TaskCompletionSource<IResponse>();
        var context = new MessageContext(Node, ActorRef.Null, source);
        await World.Router.DispatchAsync(context, request);
        var response = await source.Task;
        return (TResponse)response;
    }
}
