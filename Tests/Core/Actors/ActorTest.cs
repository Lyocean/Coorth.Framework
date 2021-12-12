using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Coorth.Tests.Actors {
    public class ActorTest {

        private ActorContainer container;

        [SetUp]
        public void Setup() {
            container = new ActorContainer();
        }

        [Test]
        public void CreateActor() {
            var domain = container.CreateDomain<LocalDomain>();
            var actorRef = domain.CreateActor<ActorForTest>(nameof(ActorForTest));
            Assert.IsTrue(!actorRef.IsNull);
        }
        
        [Test]
        public void GetActor() {
            var domain = container.CreateDomain<LocalDomain>();
            var actorRef1 = domain.CreateActor<ActorForTest>(nameof(ActorForTest));
            var actorRef2 = container.GetRef(actorRef1.Id);
            
            Assert.IsTrue(!actorRef2.IsNull);
            Assert.IsTrue(actorRef1 == actorRef2);
        }
        
        [Test]
        public void RemoveActor() {
            var domain = container.CreateDomain<LocalDomain>();
            var actorRef11 = domain.CreateActor<ActorForTest>(nameof(ActorForTest));
            domain.RemoveActor(actorRef11);
            var actorRef12 = container.GetRef(actorRef11.Id);
            Assert.IsTrue(actorRef12.IsNull);
            
            var actorRef21 = domain.CreateActor<ActorForTest>(nameof(ActorForTest));
            domain.RemoveActor(actorRef21.Id);
            var actorRef22 = container.GetRef(actorRef21.Id);
            Assert.IsTrue(actorRef22.IsNull);
        }
        
        [Test]
        public void SendMessage() {
            var domain = container.CreateDomain<LocalDomain>();
            var actor = new ActorForTest();
            var actorRef = domain.CreateActor<ActorForTest>(actor, nameof(ActorForTest));
            actorRef.Send(new MessageTestAdd());
            Assert.IsTrue(actor.Count == 1);
        }

        [Test]
        public void RequestMessage() {
            var domain = container.CreateDomain<LocalDomain>();
            var actor = new ActorForTest();
            var actorRef = domain.CreateActor<ActorForTest>(actor, nameof(ActorForTest));
            var task = actorRef.Request<MessageTestRequest, MessageTestResponse>(new MessageTestRequest());
            Assert.IsTrue(actor.Requested);
            task.ContinueWith((t) => {
                Assert.NotNull(t.Result);
            });
        }
        
        [Test]
        public void RequestMessage2() {
            var domain = container.CreateDomain<LocalDomain>();
            var actor = new ActorForTest();
            var actorRef =  domain.CreateActor<ActorForTest>(actor, nameof(ActorForTest));
            var task = actorRef.Request<MessageTestRequest, MessageTestResponse>(new MessageTestRequest());
            Assert.IsTrue(actor.Requested);
            task.ContinueWith((t) => {
                Assert.IsNull(t.Result);
            });
        }
    }

    public interface IActorTest {
        
    }

    public class ActorForTest : Actor, IActorTest {

        public int Count = 0;

        public bool Requested = false;
        
        public override Task ReceiveAsync(in ActorMail e) {
            if (e.Message is MessageTestAdd message) {
                Count++;
            }else if (e.Message is MessageTestRequest request) {
                Requested = true;
                e.Response(new MessageTestResponse());
            }
            return Task.CompletedTask;
        }
    }

    public class MessageTestAdd : IMessage {
        
    }
    
    public class MessageTestRequest : IRequest {
        
    }
    
    public class MessageTestResponse : IResponse {
        
    }
}
