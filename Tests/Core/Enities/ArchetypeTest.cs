using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Coorth.Tests.Entities {
    public class ArchetypeTest {
        
        private Sandbox sandbox;
        
        [SetUp]
        public void Setup() {
            sandbox = SandboxTest.Create();
        }

        [Test]
        public void Archetype_Null() {
            var archetype = new Archetype();
            Assert.IsTrue(archetype.IsNull);
        }

        [Test]
        public void Archetype_Sandbox() {
            var archetype = sandbox.CreateArchetype().Compile();
            Assert.IsTrue(archetype.Sandbox == sandbox);
        }
        
        [Test]
        public void Archetype_Empty() {
            var entity = sandbox.CreateEntity();
            var archetype1 = sandbox.GetArchetype(entity.Id);
            var archetype2 = entity.Archetype;
            var archetype3 = sandbox.CreateArchetype().Compile();

            Assert.IsTrue(archetype1.Count == 0);
            Assert.IsTrue(archetype2.Count == 0);
            Assert.IsTrue(archetype3.Count == 0);
            
            Assert.IsTrue(archetype1 == archetype2);
            Assert.IsTrue(archetype1 == archetype3);
        }
        
        [Test]
        public void Archetype_AddComponent_1() {
            var archetype1 = sandbox.CreateArchetype().Add<TestClassComponent0>().Compile();
            
            Assert.IsTrue(archetype1.Count == 1);
            
            Assert.IsTrue(archetype1.Has<TestClassComponent0>());
            Assert.IsFalse(archetype1.Has<TestClassComponent1>());
            Assert.IsFalse(archetype1.Has<TestClassComponent2>());
            
            var archetype2 = sandbox.CreateArchetype().Add(typeof(TestClassComponent0)).Compile();
            Assert.IsTrue(archetype2.Count == 1);

        }

        [Test]
        public void Archetype_AddComponent_N() {
            var archetype1 = sandbox.CreateArchetype().Add<TestClassComponent0>().Compile();
            var archetype2 = sandbox.CreateArchetype().Add<TestClassComponent0>().Add<TestClassComponent1>().Compile();
            var archetype3 = sandbox.CreateArchetype().Add<TestClassComponent0>().Add<TestClassComponent1>().Add<TestClassComponent2>().Compile();
            
            Assert.IsTrue(archetype1.Count == 1);
            Assert.IsTrue(archetype2.Count == 2);
            Assert.IsTrue(archetype3.Count == 3);

            Assert.IsTrue(archetype2.Has<TestClassComponent0>());
            Assert.IsTrue(archetype2.Has<TestClassComponent1>());
            Assert.IsFalse(archetype2.Has<TestClassComponent2>());
            
            Assert.IsTrue(archetype3.Has<TestClassComponent0>());
            Assert.IsTrue(archetype3.Has<TestClassComponent1>());
            Assert.IsTrue(archetype3.Has<TestClassComponent2>());
            Assert.IsFalse(archetype3.Has<TestClassComponent3>());
        }

        [Test]
        public void Archetype_CreateEntity() {
            var archetype0 = sandbox.CreateArchetype().Compile();
            var entity0 = archetype0.CreateEntity();
            Assert.IsTrue(entity0.Count == 0);
            Assert.IsTrue(!entity0.Has<TestClassComponent0>());
            
            var archetype1 = sandbox.CreateArchetype().Add<TestClassComponent0>().Compile();
            var entity1 = archetype1.CreateEntity();
            Assert.IsTrue(entity1.Count == 1);
            Assert.IsTrue(entity1.Has<TestClassComponent0>() && !entity1.Has<TestClassComponent1>());
            
            var archetype2 = sandbox.CreateArchetype().Add<TestClassComponent0>().Add<TestClassComponent1>().Compile();
            var entity2 = archetype2.CreateEntity();
            Assert.IsTrue(entity2.Count == 2);
            Assert.IsTrue(entity2.Has<TestClassComponent0>() && entity2.Has<TestClassComponent1>());
        }

        [Test]
        public void Archetype_CreateEntities1() {
            var archetype = sandbox.CreateArchetype().Add<TestClassComponent0>().Compile();
            var entities = archetype.CreateEntities(10);
            foreach (Entity entity in entities) {
                Assert.IsTrue(!entity.IsNull && entity.Count == 1 && entity.Has<TestClassComponent0>());
            }
        }
        
        [Test]
        public void Archetype_CreateEntities2() {
            var archetype = sandbox.CreateArchetype().Add<TestClassComponent0>().Compile();
            var array = new Entity[10];
            archetype.CreateEntities(array.AsSpan(), 10);
            foreach (Entity entity in array) {
                Assert.IsTrue(!entity.IsNull && entity.Count == 1 && entity.Has<TestClassComponent0>());
            }
        }
        
        [Test]
        public void Archetype_CreateEntities3() {
            var archetype = sandbox.CreateArchetype().Add<TestClassComponent0>().Compile();
            var list = new List<Entity>(1);
            archetype.CreateEntities(list, 1);
            foreach (Entity entity in list) {
                Assert.IsTrue(!entity.IsNull && entity.Count == 1 && entity.Has<TestClassComponent0>());
            }
        }
    }
}