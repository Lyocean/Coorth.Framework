using NUnit.Framework;

namespace Coorth.Tests.Entities {
    internal class ComponentTest {
        private Sandbox sandbox;
        
        [SetUp]
        public void Setup() {
            sandbox = SandboxTest.Create();
        }
        
        [Test]
        public void AddComponent() {
            var entity = sandbox.CreateEntity();
            entity.Add<TestValueComponent0>();
            Assert.IsTrue(entity.Count == 1);
            Assert.IsTrue(sandbox.ComponentCount<TestValueComponent0>() == 1);
        }
        
        [Test]
        public void AddComponent2() {
            var entity1 = sandbox.CreateEntity();
            entity1.Add<TestClassComponent0>();
            var entity2 = sandbox.CreateEntity();
            entity2.Add<TestClassComponent0>();
            
            Assert.IsFalse(ReferenceEquals(entity1.Get<TestClassComponent0>(), entity2.Get<TestClassComponent0>()));
            Assert.IsTrue(sandbox.ComponentCount<TestClassComponent0>() == 2);
        }
        
        [Test]
        public void SingletonComponent() {
            var component1 = sandbox.Singleton<TestClassComponent0>();
            var component2 = sandbox.Singleton<TestClassComponent0>();
            Assert.IsTrue(ReferenceEquals(component1, component2));
        }

        [Test]
        public void HasComponent() {
            var entity = sandbox.CreateEntity();
            entity.Add<TestValueComponent0>();
            Assert.IsTrue(entity.Has<TestValueComponent0>());
        }
        
        [Test]
        public void GetComponent() {
            var entity = sandbox.CreateEntity();
            entity.Add(new TestValueComponent1(){ a =  5, b = 6});
            var component = entity.Get<TestValueComponent1>();
            Assert.IsTrue(component.a == 5 && component.b == 6);
        }

        [Test]
        public void RefComponent() {
            var entity = sandbox.CreateEntity();
            entity.Add(new TestValueComponent1() { a = 5, b = 6 });

            ref var component = ref entity.Ref<TestValueComponent1>();

            Assert.IsTrue(component.a == 5);
            component.a = 8;

            var component2 = entity.Get<TestValueComponent1>();
            Assert.IsTrue(component2.a == 8);
        }

        [Test]
        public void RemoveComponent() {
            var entity = sandbox.CreateEntity();
            entity.Add<TestValueComponent0>();
            entity.Remove<TestValueComponent0>();
            Assert.IsTrue(entity.Count == 0);
            Assert.IsFalse(entity.Has<TestValueComponent0>());
            Assert.IsTrue(sandbox.ComponentCount<TestValueComponent0>() == 0);
        }
        
        [Test]
        public void ClearComponent() {
            var entity = sandbox.CreateEntity();
            entity.Add<TestValueComponent0>();
            entity.Add<TestClassComponent2>();
            entity.Clear();
            Assert.IsTrue(entity.Count == 0);
        }
    }
}