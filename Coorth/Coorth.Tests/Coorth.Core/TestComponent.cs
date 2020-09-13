using NUnit.Framework;

namespace Coorth.ECS.Test {

    class TestComponent {
        private EcsContainer container;
        private const int TEST_COUNT = 10;

        [SetUp]
        public void Setup() {
            container = new EcsContainer();
        }

        [Test]
        public void AddRefComponent() {
            var context = container.CreateContext();
            var entity = container.CreateEntity();
            context.Add<ValComp00>();
            entity.Add<ValComp00>();
            context.Add(new ValComp01());
            entity.Add(new ValComp01());

            Assert.IsTrue(context.Ref<ValComp01>().a == 0);
            Assert.IsTrue(entity.Ref<ValComp01>().a == 0);

            context.Ref<ValComp01>().a = 10;
            entity.Ref<ValComp01>().a = 11;

            Assert.IsTrue(context.Ref<ValComp01>().a == 10);
            Assert.IsTrue(entity.Ref<ValComp01>().a == 11);
        }

        [Test]
        public void HasComponent() {
            var entity = container.CreateEntity();
            entity.Add<ValComp03>();
            Assert.IsTrue(entity.Has<ValComp03>());
            Assert.IsFalse(entity.Has<ValComp04>());
        }

        [Test]
        public void ModifyComponent() {
            var entity = container.CreateEntity();
            //Val Comp
            entity.Add<ValComp01>();
            entity.Ref<ValComp01>().a = 1;
            Assert.IsTrue(entity.Ref<ValComp01>().a == 1);

            entity.Modify(new ValComp01() { a = 10, b = 7 });
            Assert.IsTrue(entity.Ref<ValComp01>().a == 10 && entity.Ref<ValComp01>().b == 7);

            entity.Modify<ValComp01>((_, c) => new ValComp01() { a = c.a + 1, b = 0 });
            Assert.IsTrue(entity.Ref<ValComp01>().a == 11 && entity.Ref<ValComp01>().b == 0);


            //Ref Comp
            entity.Add<RefComp01>();
            Assert.IsTrue(entity.Get<RefComp01>().a == 0);

            entity.Modify<RefComp01>((_, c) => c.a = 5);
            Assert.IsTrue(entity.Get<RefComp01>().a == 5);
        }

        [Test]
        public void ClearComponent() {
            var entity = container.CreateEntity();
            entity.Add<ValComp01>();
            entity.Add<RefComp01>();
            entity.Clear();
            Assert.IsFalse(entity.Has<ValComp01>());
            Assert.IsFalse(entity.Has<RefComp01>());
        }

    }
}
