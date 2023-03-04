using System;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Serialize;

namespace Coorth.Framework;

public interface IWorldActor {
    ValueTask AddSystem(Type type);
    ValueTask AddSystems(params Type[] types);
    ValueTask<bool> HasSystem(Type type);
    ValueTask<bool> HasSystems(params Type[] types);
    ValueTask RemoveSystem(Type type);
    ValueTask RemoveSystems(params Type[] types);

    ValueTask Send<TMessage>(TMessage message) where TMessage : notnull;
    ValueTask<TResponse> Request<TRequest, TResponse>(TRequest request) where TRequest : notnull;

}

public sealed class WorldActor : ActorBase, IWorldActor {

    public readonly World World;

    public WorldActor(World world) {
        World = world;
    }

    public override ValueTask ReceiveAsync(MessageContext context, IMessage message) {
        return World.ReceiveAsync(context, message);
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
