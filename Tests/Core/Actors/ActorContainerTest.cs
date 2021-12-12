using NUnit.Framework;

namespace Coorth.Tests.Actors {
    public class ActorContainerTest {
        
        [Test]
        public void CreateContainer() {
            var container = new ActorContainer();
            Assert.NotNull(container);
        }
        
        [Test]
        public void CreateDomain() {
            var container = new ActorContainer();
            var domain = container.CreateDomain<LocalDomain>();
            Assert.NotNull(domain);
        }
        
        [Test]
        public void GetDomain() {
            var container = new ActorContainer();
            var domain10 = container.CreateDomain<LocalDomain>("111");
            var domain20 = container.CreateDomain<LocalDomain>("222");

            var domain11 = container.GetDomain("111");
            var domain21 = container.GetDomain("222");
            var domain31 = container.GetDomain("333");

            Assert.NotNull(domain10);
            Assert.NotNull(domain20);
            Assert.NotNull(domain11);
            Assert.NotNull(domain21);
            Assert.IsNull(domain31);
            
            Assert.IsTrue(ReferenceEquals(domain10, domain11));
            Assert.IsTrue(ReferenceEquals(domain20, domain21));
        }
        
        [Test]
        public void DisposeDomain() {
            var container = new ActorContainer();
            var domain10 = container.CreateDomain<LocalDomain>("111");
            var _ = container.CreateDomain<LocalDomain>("222");

            domain10.Dispose();
            
            Assert.IsNull(container.GetDomain("111"));
            Assert.NotNull(container.GetDomain("222"));
        }
    }
}