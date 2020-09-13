using NUnit.Framework;

namespace Coorth.ECS.Test {
    class TestEntity {

        private EcsContainer container;
        private const int TEST_COUNT = 10;

        [SetUp]
        public void Setup() {
            container = new EcsContainer();
        }

        private (EntityContext[], Entity[]) CreateTestEntity() {
            var contexts = new EntityContext[TEST_COUNT];
            var entities = new Entity[TEST_COUNT];
            for (var i = 0; i < TEST_COUNT; i++) {
                contexts[i] = container.CreateContext();
                entities[i] = container.CreateEntity();
            }
            return (contexts, entities);
        }

        [Test]
        public void CreateEntity() {
            var (contexts, entities) = CreateTestEntity();
            Assert.IsTrue(container.EntityCount == TEST_COUNT * 2);
            for (var i = 0;i< contexts.Length-1; i++){
                for (var j = i+1; j < contexts.Length; j++) {
                    Assert.IsTrue(contexts[i].Id != contexts[j].Id);
                }
            }
            for (var i = 0; i < entities.Length - 1; i++) {
                for (var j = i + 1; j < entities.Length; j++) {
                    Assert.IsTrue(entities[i].Id != entities[j].Id);
                }
            }
            for (var i = 0; i < contexts.Length; i++) {
                for (var j = 0; j < entities.Length; j++) {
                    Assert.IsTrue(contexts[i].Id != entities[j].Id);
                }
            }
            Assert.IsTrue(container.CreateContext().Id != EntityId.Null);
            Assert.IsTrue(container.CreateEntity().Id != EntityId.Null);
        }

        [Test]
        public void HasEntity() {
            var (contexts, entities) = CreateTestEntity();
            for (var i = 0; i < TEST_COUNT; i++) {
                Assert.IsTrue(container.HasEntity(contexts[i].Id));
                Assert.IsTrue(container.HasEntity(entities[i].Id));
            }
        }

        [Test]
        public void GetEntity() {
            var (contexts, entities) = CreateTestEntity();
            for (var i = 0; i < TEST_COUNT; i++) {
                Assert.IsTrue(contexts[i] == container.GetContext(contexts[i].Id));
                Assert.IsTrue(ReferenceEquals(entities[i], container.GetEntity(entities[i].Id)));
            }
        }

        [Test]
        public void DestroyEntity() {
            var (contexts, entities) = CreateTestEntity();
            for (var i = 0; i < TEST_COUNT; i++) {
                container.DestroyContext(contexts[i]);
                Assert.IsFalse(container.HasEntity(contexts[i].Id));
            }
            for (var i = 0; i < TEST_COUNT; i++) {
                container.DestroyEntity(entities[i]);
                Assert.IsFalse(container.HasEntity(entities[i].Id));
            }
            var (contexts2, entities2) = CreateTestEntity();
            for (var i = 0; i < TEST_COUNT; i++) {
                container.DestroyEntity(contexts2[i].Id);
                container.DestroyEntity(entities2[i].Id);
                Assert.IsFalse(container.HasEntity(contexts2[i].Id));
                Assert.IsFalse(container.HasEntity(entities2[i].Id));
            }
        }
    }
}
