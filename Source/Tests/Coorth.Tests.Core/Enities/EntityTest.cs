using NUnit.Framework;

namespace Coorth.Tests.Entities {
    public class EntityTest {

        private Sandbox sandbox;
        
        [SetUp]
        public void Setup() {
            sandbox = SandboxTest.Create();
        }
        
        [Test]
        public void ValidateSandbox() {
            Assert.NotNull(sandbox);
        }

        [Test]
        public void CreateEmptyEntity() {
            var entity = sandbox.CreateEntity();
            Assert.IsTrue(entity.Sandbox == sandbox);
            Assert.IsTrue(!entity.IsNull);
        }
        
        [Test]
        public void CreateEmptyEntitiesX2() {
            var entity1 = sandbox.CreateEntity();
            var entity2 = sandbox.CreateEntity();
            Assert.IsTrue(entity1 != entity2);
        }
        
        [Test]
        public void CreateEmptyEntitiesX1000() {
            for (var i = 0; i < 1000; i++) {
                var entity = sandbox.CreateEntity();
                Assert.IsFalse(entity.IsNull);
            }
        }

        [Test]
        public void SingletonEntity() {
            var entity = sandbox.Singleton();
            Assert.IsFalse(entity.Id.IsNull);
            Assert.IsFalse(entity.IsNull);
            var entity1 = sandbox.Singleton();
            var entity2 = sandbox.Singleton();
            Assert.IsTrue(entity1 == entity2);

            var entity3 = sandbox.CreateEntity();
            Assert.IsFalse(entity == entity3);

        }
        
        [Test]
        public void HasEntity() {
            var entity = sandbox.CreateEntity();
            Assert.IsTrue(sandbox.HasEntity(entity));
            Assert.IsTrue(sandbox.HasEntity(entity.Id));
        }

        [Test]
        public void GetEntity() {
            var entity = sandbox.CreateEntity();
            Assert.IsTrue(sandbox.GetEntity(entity.Id) == entity);
        }

        [Test]
        public void DestroyEntity() {
            var entity = sandbox.CreateEntity();
            entity.Destroy();
            Assert.IsFalse(sandbox.HasEntity(entity));
        }

        [Test]
        public void CreateArchetype() {
            var builder = sandbox.CreateArchetype();
            var archetypeCompiled = builder.Compile();
            Assert.IsFalse(archetypeCompiled.IsNull);
            var entity = archetypeCompiled.CreateEntity();
            Assert.IsTrue(sandbox.HasEntity(entity));
        }
        
        [Test]
        public void ArchetypeAddComponent() {
            var builder = sandbox.CreateArchetype();
            builder.Add<TestValueComponent0>()
                .Add<TestValueComponent1>()
                .Add<TestClassComponent2>();
            var archetypeCompiled = builder.Compile();
            Assert.IsTrue(archetypeCompiled.Has<TestValueComponent0>());
            Assert.IsTrue(archetypeCompiled.Has<TestValueComponent1>());
            Assert.IsTrue(archetypeCompiled.Has<TestClassComponent2>());
            
            Assert.IsFalse(archetypeCompiled.Has<TestClassComponent0>());
            Assert.IsFalse(archetypeCompiled.Has<TestClassComponent1>());
            Assert.IsFalse(archetypeCompiled.Has<TestValueComponent2>());
        }
        
        [Test]
        public void ArchetypeCreateEntity() {
            var builder = sandbox.CreateArchetype();
            builder.Add<TestValueComponent0>()
                .Add<TestValueComponent1>()
                .Add<TestClassComponent2>();
            var archetypeCompiled = builder.Compile();
            var entity = archetypeCompiled.CreateEntity();

            Assert.IsTrue(sandbox.HasEntity(entity));
            Assert.IsTrue(entity.Has<TestValueComponent0>());
            Assert.IsTrue(entity.Has<TestValueComponent1>());
            Assert.IsTrue(entity.Has<TestClassComponent2>());
        }
        
        [Test]
        public void ArchetypeCreateEntityX1000() {
            var builder = sandbox.CreateArchetype();
            builder.Add<TestValueComponent0>()
                .Add<TestValueComponent1>()
                .Add<TestClassComponent2>();
            var archetypeCompiled = builder.Compile();

            for (int i = 0; i < 1000; i++) {
                var entity = archetypeCompiled.CreateEntity();
                Assert.IsTrue(sandbox.HasEntity(entity));
                Assert.IsTrue(entity.Has<TestValueComponent0>());
                Assert.IsTrue(entity.Has<TestValueComponent1>());
                Assert.IsTrue(entity.Has<TestClassComponent2>());
            }
            Assert.IsTrue(sandbox.EntityCount == 1001);
        }
    }
}