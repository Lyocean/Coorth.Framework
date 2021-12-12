using System.Linq;
using Coorth.Common;
using Coorth.Tests.Entities;
using NUnit.Framework;

namespace Coorth.Tests.World {
    public class HierarchyComponentTest {
        private Sandbox sandbox;
        
        [SetUp]
        public void Setup() {
            sandbox = SandboxTest.Create();
            sandbox.AddSystem<HierarchySystem>();
        }

        [Test]
        public void AddHierarchyComponent() {
            var entity = sandbox.CreateEntity();
            ref var hierarchy = ref entity.Add<HierarchyComponent>();
            Assert.IsTrue(hierarchy.Entity == entity);
            Assert.IsTrue(hierarchy.IsEnableSelf());
            Assert.IsTrue(hierarchy.ParentEntity.IsNull);
            Assert.IsTrue(hierarchy.Count == 0);

        }
        
        [Test]
        public void Enable() {
            var entity = sandbox.CreateEntity();
            ref var hierarchy = ref entity.Add<HierarchyComponent>();
            Assert.IsTrue(hierarchy.IsEnableSelf());
            Assert.IsTrue(hierarchy.IsEnableInHierarchy());
        }
        
        [Test]
        public void Parent() {
            var childEntity = sandbox.CreateEntity();
            ref var childHierarchy = ref childEntity.Add<HierarchyComponent>();

            var parentEntity = sandbox.CreateEntity();

            childHierarchy.SetParent(parentEntity);
            ref var parentHierarchy = ref parentEntity.Get<HierarchyComponent>();
            
            Assert.IsTrue(childHierarchy.ParentEntity == parentEntity);
            Assert.IsTrue(parentHierarchy.Count == 1);

            var childEntity2 = sandbox.CreateEntity();
            ref var childHierarchy2 = ref childEntity2.Add<HierarchyComponent>();
            parentHierarchy.AddChild(ref childHierarchy2);
            
            Assert.IsTrue(parentHierarchy.Count == 2);
        }

        [Test]
        public void Children0() {
            var parentEntity = sandbox.CreateEntity();
            ref var parentHierarchy = ref parentEntity.Add<HierarchyComponent>();
            Assert.IsTrue(parentHierarchy.Count == 0);
            var number = parentHierarchy.GetChildrenEntities().Count();
            Assert.IsTrue(number == 0);
        }

        [Test]
        public void Children1() {
            var parentEntity = sandbox.CreateEntity();
            ref var parentHierarchy = ref parentEntity.Add<HierarchyComponent>();
            
            var childEntity1 = sandbox.CreateEntity();
            ref var childHierarchy1 = ref childEntity1.Add<HierarchyComponent>();

            parentHierarchy.AddChild(ref childHierarchy1);
            Assert.IsTrue(parentHierarchy.Count == 1);
            var number = parentHierarchy.GetChildrenEntities().Count();
            Assert.IsTrue(number == 1);
            Assert.IsTrue(parentHierarchy.GetChildrenEntities().First() == childEntity1);
        }
        
        [Test]
        public void Children2() {
            var parentEntity = sandbox.CreateEntity();
            ref var parentHierarchy = ref parentEntity.Add<HierarchyComponent>();

            var childEntity1 = sandbox.CreateEntity();
            ref var childHierarchy1 = ref childEntity1.Add<HierarchyComponent>();

            var childEntity2 = sandbox.CreateEntity();
            ref var childHierarchy2 = ref childEntity2.Add<HierarchyComponent>();
            
            parentHierarchy.AddChild(ref childHierarchy1);
            parentHierarchy.AddChild(in childEntity2);

            Assert.IsTrue(parentHierarchy.Count == 2);

            var number = parentHierarchy.GetChildrenEntities().Count();
            Assert.IsTrue(number == 2);
            
            Assert.IsTrue(parentHierarchy.GetChildrenEntities().First() == childEntity1);
            Assert.IsTrue(parentHierarchy.GetChildrenEntities().Last() == childEntity2);

        }
    }
}