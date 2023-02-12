using System;
using System.Threading.Tasks;
using Coorth.Logs;
using NUnit.Framework;

namespace Coorth.Framework; 

public class ActorTest {

    private ActorsRuntime container;

    [SetUp]
    public void Setup() {
        container = new ActorsRuntime(Dispatcher.Root, new ServiceLocator(), new LoggerConsole());
    }

    [Test]
    public void CreateActor() {
        var domain = container.CreateDomain("l");
        var actorRef = domain.CreateActor<ActorDispatchForTest>(nameof(ActorDispatchForTest));
        Assert.IsTrue(!actorRef.IsNull);
    }
        
    [Test]
    public void GetActor() {
        var domain = container.CreateDomain("123");
        var actorRef1 = domain.CreateActor<ActorDispatchForTest>(nameof(ActorDispatchForTest));
        var actorRef2 = container.GetRef(actorRef1.Id);

        Assert.IsTrue(!actorRef2.IsNull);
        Assert.IsTrue(actorRef1 == actorRef2);
    }
        
    [Test]
    public void RemoveActor() {
        var domain = container.CreateDomain("456");
        var actorRef11 = domain.CreateActor<ActorDispatchForTest>(nameof(ActorDispatchForTest));
        domain.RemoveActor(actorRef11);
        var actorRef12 = container.GetRef(actorRef11.Id);
        Assert.IsTrue(actorRef12.IsNull);
            
        var actorRef21 = domain.CreateActor<ActorDispatchForTest>(nameof(ActorDispatchForTest));
        domain.RemoveActor(actorRef21.Id);
        var actorRef22 = container.GetRef(actorRef21.Id);
        Assert.IsTrue(actorRef22.IsNull);
    }
        
    [Test]
    public void SendMessage() {
        var domain = container.CreateDomain("789");
        var actor = new ActorDispatchForTest();
        var actorRef = domain.CreateActor<ActorDispatchForTest>(actor, nameof(ActorDispatchForTest));
        actorRef.Send(new MessageTestAdd());
        Assert.IsTrue(actor.Count == 1);
    }

    [Test]
    public void RequestMessage() {
        var domain = container.CreateDomain("1011");
        var actor = new ActorDispatchForTest();
        var actorRef = domain.CreateActor<ActorDispatchForTest>(actor, nameof(ActorDispatchForTest));
        var task = actorRef.Request(new MessageTestRequest()).AsTask();
        Assert.IsTrue(actor.Requested);
        task.ContinueWith((t) => {
            Assert.NotNull(t.Result);
        });
    }
        
    [Test]
    public void RequestMessage2() {
        var domain = container.CreateDomain("asd");
        var actor = new ActorDispatchForTest();
        var actorRef =  domain.CreateActor<ActorDispatchForTest>(actor, nameof(ActorDispatchForTest));
        var task = actorRef.Request(new MessageTestRequest()).AsTask();
        Assert.IsTrue(actor.Requested);
        task.ContinueWith((t) => {
            Assert.IsNull(t.Result);
        });
    }
}

public interface IActorTest {
        
}

public class ActorDispatchForTest : Actor, IActorTest {

    public int Count = 0;

    public bool Requested = false;

    public override ValueTask ReceiveAsync(MessageContext context, IMessage m) {
        if (m is MessageTestAdd message) {
            Count++;
        }else if (m is MessageTestRequest request) {
            Requested = true;
            context.Reply(new MessageTestResponse());
        }
        return new ValueTask();
    }
}

public class MessageTestAdd : IMessage {
        
}
    
public class MessageTestRequest : IRequest {
        
}
    
public class MessageTestResponse : IResponse {
        
}