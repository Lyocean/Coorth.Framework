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
            // container.CreateDomain(null);
        }
        

    }
}