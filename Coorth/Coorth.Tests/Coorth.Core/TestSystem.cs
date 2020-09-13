using NUnit.Framework;

namespace Coorth.ECS.Test {
    class TestSystem {
        private EcsContainer container;
        private const int TEST_COUNT = 10;

        [SetUp]
        public void Setup() {
            container = new EcsContainer();
        }

        [Test]
        public void AddHasGetSystem() {
            var system0 = container.AddSystem<System00>();
            var system1 = container.AddSystem(new System01());

            Assert.IsTrue(container.HasSystem<System00>());
            Assert.IsTrue(container.HasSystem<System01>());
            Assert.IsFalse(container.HasSystem<System03>());
            Assert.IsFalse(container.HasSystem<System04>());

            Assert.AreSame(container.GetSystem<System00>(), system0);
            Assert.AreSame(container.GetSystem<System01>(), system1);

            Assert.IsTrue(container.GetSystem<System01>() != null);
            Assert.IsTrue(container.GetSystem<System03>() == null);
        }


        [Test]
        public void RemoveSystem() {
            container.AddSystem<System00>();
            container.AddSystem<System01>();

            Assert.IsTrue(container.RemoveSystem<System00>());
            Assert.IsFalse(container.RemoveSystem<System03>());
            Assert.IsTrue(container.RemoveSystem<System01>());

            Assert.IsFalse(container.HasSystem<System00>());
            Assert.IsFalse(container.HasSystem<System01>());

            Assert.IsTrue(container.GetSystem<System00>() == null);

        }

    }
}
