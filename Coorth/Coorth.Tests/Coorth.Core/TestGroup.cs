using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coorth.ECS.Test {
    class TestGroup {
        private EcsContainer container;
        private const int TEST_COUNT = 10;

        [SetUp]
        public void Setup() {
            container = new EcsContainer();
        }

        [Test]
        public void TestMatch() {
            var entity0 = container.CreateEntity(); 
            var entity1 = container.CreateEntity();
            var entity2 = container.CreateEntity(); 
            var entity3 = container.CreateEntity();
            var entity4 = container.CreateEntity();

            entity0.Add<RefComp00>();
            entity1.Add<RefComp00, RefComp01>();
            entity2.Add<RefComp01, RefComp02>();
            entity3.Add<RefComp00, RefComp02>();
            entity4.Add<RefComp00, RefComp01, RefComp02>();

            var group0 = container.GetGroup(Matcher.Include<RefComp00>());
            var group1 = container.GetGroup(Matcher.Exclude<RefComp00>()); 
            var group2 = container.GetGroup(Matcher.Include<RefComp00>().Exclude<RefComp01>());
            var group3 = container.GetGroup(Matcher.Include<RefComp00, RefComp01>());

            Assert.IsTrue(group0.Count == 4);
            Assert.IsTrue(group1.Count == 1 && group1.Contains(entity2));
            Assert.IsTrue(group2.Count == 2 && group2.Contains(entity0) && group2.Contains(entity3));
            Assert.IsTrue(group3.Count == 2 && group3.Contains(entity1) && group3.Contains(entity4));
        }
    }
}
