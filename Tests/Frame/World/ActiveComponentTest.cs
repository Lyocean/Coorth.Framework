using Coorth.Common;
using NUnit.Framework;
using Coorth.Tests.Entities;

namespace Coorth.Tests.World {
    public class ActiveComponentTest {
        private Sandbox sandbox;
        
        [SetUp]
        public void Setup() {
            sandbox = SandboxTest.Create();
            sandbox.AddSystem<ActiveSystem>();
        }
                
        [Test]
        public void AddActiveComponent() {
            var entity = sandbox.CreateEntity();
            ref var component = ref entity.Add<ActiveComponent>();
            Assert.IsFalse(component.Get(0) && component.Get(1) && component.Get(15));
        }
        
        [Test]
        public void SetComponentValue() {
            var entity = sandbox.CreateEntity();
            ref var component = ref entity.Add<ActiveComponent>();
            component.Set(15, true);
            Assert.IsTrue(component.Get(15));
            Assert.IsTrue(entity.Get<ActiveComponent>().Get(15));
        }
    }


}