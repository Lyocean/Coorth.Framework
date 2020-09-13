using NUnit.Framework;

namespace Coorth.ECS.Test {
    class TestEvent {
        private EcsContainer container;
        private const int TEST_COUNT = 10;

        [SetUp]
        public void Setup() {
            container = new EcsContainer();
        }

        [Test]
        public void InitDestroyEvent() {
            int initCount = 0;
            container.AddSystem<EventBeginInit>(evt => {
                initCount++;
            });
            container.Execute(new EventBeginInit());
            Assert.IsTrue(initCount == 1);
            for (var i = 0; i < TEST_COUNT; i++) {
                container.Execute(new EventTickUpdate());
            }
        }

        [Test]
        public void ComponentEvent() {
            int addCount1 = 0;
            container.AddSystem<EventComponentAdd>(evt => {
                addCount1++;
            });
            int addCount2 = 0;
            container.AddSystem<EventComponentAdd<RefComp00>>(evt => {
                addCount2++;
            });
            int modifyCount1 = 0;
            container.AddSystem<EventComponentModify>(evt => {
                modifyCount1++;
            });
            int modifyCount2 = 0;
            container.AddSystem<EventComponentModify<RefComp00>>(evt => {
                modifyCount2++;
            });
            int removeCount1 = 0;
            container.AddSystem<EventComponentRemove>(evt => {
                removeCount1++;
            });
            int removeCount2 = 0;
            container.AddSystem<EventComponentRemove<RefComp00>>(evt => {
                removeCount2++;
            });

            var entity = container.CreateEntity();
            Assert.IsTrue(addCount1 == 0);
            Assert.IsTrue(addCount2 == 0);
            Assert.IsTrue(modifyCount1 == 0);
            Assert.IsTrue(modifyCount2 == 0);
            Assert.IsTrue(removeCount1 == 0);
            Assert.IsTrue(removeCount2 == 0);

            entity.Add<RefComp00>();
            entity.Add<RefComp01>();

            Assert.IsTrue(addCount1 == 2);
            Assert.IsTrue(addCount2 == 1);
  

            entity.Modify<RefComp00>();
            entity.Modify<RefComp01>();
            Assert.IsTrue(modifyCount1 == 2);
            Assert.IsTrue(modifyCount2 == 1);

            entity.Remove<RefComp00>();
            entity.Remove<RefComp01>();

            Assert.IsTrue(removeCount1 == 2);
            Assert.IsTrue(removeCount2 == 1);
        }
    }
}
