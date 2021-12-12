using System;
using Coorth.Common;
using Coorth.Tests.Entities;
using NUnit.Framework;

namespace Coorth.Tests.World {
    public class LifetimeComponentTest {
        private Sandbox sandbox;
        
        [SetUp]
        public void Setup() {
            sandbox = SandboxTest.Create();
            sandbox.AddSystem<LifetimeSystem>();
        }

        [Test]
        public void AddLifetimeComponent() {
            var entity = sandbox.CreateEntity();
            ref var lifetime = ref entity.Add<LifetimeComponent>();
            lifetime.OnSetup(TimeSpan.Zero);
        }
        
        [Test]
        public void DestroyDelayTime0() {
            var entity = sandbox.CreateEntity();
            var id = entity.Id;
            entity.Destroy(TimeSpan.Zero);
            Assert.IsTrue(sandbox.GetEntity(id).IsNotNull);
            
            sandbox.Execute(new EventEndOfFrame());
            Assert.IsTrue(sandbox.GetEntity(id).IsNull);
        }
        
        [Test]
        public void DestroyDelayTime1() {
            var entity = sandbox.CreateEntity();
            var id = entity.Id;
            entity.Destroy(TimeSpan.FromSeconds(1f));
            Assert.IsTrue(sandbox.GetEntity(id).IsNotNull);
            sandbox.Execute(new EventEndOfFrame(TimeSpan.FromSeconds(0.5f), TimeSpan.FromSeconds(0.5f), 1));
            Assert.IsTrue(sandbox.GetEntity(id).IsNotNull);
            sandbox.Execute(new EventEndOfFrame(TimeSpan.FromSeconds(1f), TimeSpan.FromSeconds(0.51f), 2));
            Assert.IsTrue(sandbox.GetEntity(id).IsNull);
        }
        
        [Test]
        public void DestroyDelayFrame0() {
            var entity = sandbox.CreateEntity();
            var id = entity.Id;
            entity.Destroy(0);
            Assert.IsTrue(sandbox.GetEntity(id).IsNotNull);
            
            sandbox.Execute(new EventEndOfFrame());
            Assert.IsTrue(sandbox.GetEntity(id).IsNull);
        }
        
        [Test]
        public void DestroyDelayFrame1() {
            var entity = sandbox.CreateEntity();
            var id = entity.Id;
            entity.Destroy(5);
            Assert.IsTrue(sandbox.GetEntity(id).IsNotNull);
            for (var i = 1; i <= 10; i++) {
                sandbox.Execute(new EventEndOfFrame(TimeSpan.FromSeconds(0.5f), TimeSpan.FromSeconds(0.5f), i));
                if (i <= 5) {
                    Assert.IsTrue(sandbox.GetEntity(id).IsNotNull);
                }
                else {
                    Assert.IsTrue(sandbox.GetEntity(id).IsNull);
                }
            }
        }
    }
}