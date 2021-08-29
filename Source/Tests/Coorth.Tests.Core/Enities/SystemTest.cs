using System;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;

namespace Coorth.Tests.Entities {
    internal class SystemTest {
        private Sandbox sandbox;

        [SetUp]
        public void Setup() {
            sandbox = SandboxTest.Create();
        }

        [Test]
        public void AddSystem() {
            var system = sandbox.AddSystem<TestSystem>();
            Assert.NotNull(system);

            Assert.Catch(() => { sandbox.AddSystem<TestSystem>(); });
        }

        [Test]
        public void HasSystem() {
            sandbox.AddSystem<TestSystem>();
            Assert.IsTrue(sandbox.HasSystem<TestSystem>());
        }

        [Test]
        public void RemoveSystem() {
            sandbox.AddSystem<TestSystem>();
            Assert.IsTrue(sandbox.RemoveSystem<TestSystem>());
            Assert.IsFalse(sandbox.HasSystem<TestSystem>());
        }

        [Test]
        public void SubSystem() {
            var system = sandbox.AddSystem<TestSystem>();
            var subSystem = system.AddSystem<TestSubSystem>();
            Assert.NotNull(subSystem);

            Assert.IsTrue(sandbox.HasSystem<TestSystem>());
            Assert.IsTrue(system.HasSystem<TestSubSystem>());

            Assert.AreSame(subSystem.Parent, system);

            Assert.IsTrue(sandbox.RemoveSystem<TestSubSystem>());
            Assert.IsFalse(sandbox.HasSystem<TestSubSystem>());
            Assert.IsFalse(system.HasSystem<TestSubSystem>());
        }

        [Test]
        public void SystemReactionX1() {
            var system = sandbox.AddSystem<TestForEachSystem>();
            var entity1 = sandbox.CreateEntity().With<TestClassComponent1>();
            sandbox.Execute(new EventTickUpdate());
            
            Assert.IsTrue(system.Entities1.Contains(entity1));
            Assert.IsTrue(entity1.Get<TestClassComponent1>().a == 1);
        }

        [Test]
        public void SystemReactionX2() {
            var system = sandbox.AddSystem<TestForEachSystem>();
            var entity2 = sandbox.CreateEntity().With<TestClassComponent1>().With<TestClassComponent2>();
            sandbox.Execute(new EventTickUpdate());
            
            Assert.IsTrue(system.Entities2.Contains(entity2));
            Assert.IsTrue(entity2.Get<TestClassComponent1>().a == 2);
            Assert.IsTrue(Math.Abs(entity2.Get<TestClassComponent2>().a - 1) < 1e-5);
        }

        [Test]
        public void SystemReactionX3() {
            var system = sandbox.AddSystem<TestForEachSystem>();
            var entity3 = sandbox.CreateEntity().With<TestClassComponent1>().With<TestClassComponent2>()
                .With<TestClassComponent3>();
            sandbox.Execute(new EventTickUpdate());

            Assert.IsTrue(system.Entities3.Contains(entity3));
            Assert.IsTrue(entity3.Get<TestClassComponent1>().a == 3);
            Assert.IsTrue(Math.Abs(entity3.Get<TestClassComponent2>().a - 2) < 1e-5);
            
            Assert.IsTrue(Math.Abs(entity3.Get<TestClassComponent3>().v1.Length() - 2.0) < 1e-5);
        }

        [Test]
        public void SystemReactionMatch() {
            var system = sandbox.AddSystem<TestForEachSystem>();
            var entity4 = sandbox.CreateEntity().With<TestClassComponent1>().With<TestClassComponent3>();
            sandbox.Execute(new EventTickUpdate());
            
            Assert.IsTrue(system.Entities4.Contains(entity4));
            Assert.IsTrue(entity4.Get<TestClassComponent1>().a == 1);
            Assert.IsTrue(entity4.Get<TestClassComponent1>().b == 1);
        }

        [Test]
        public void SystemReactionMatch2() {
            var system = sandbox.AddSystem<TestForEachSystem>();
            var entity5 = sandbox.CreateEntity().With<TestClassComponent1>().With<TestClassComponent2>();
            sandbox.Execute(new EventTickUpdate());
            
            Assert.IsFalse(system.Entities4.Contains(entity5));
            Assert.IsTrue(entity5.Get<TestClassComponent1>().a == 2);
        }


        [Test]
        public void SystemReactionMulti() {
            var system = sandbox.AddSystem<TestForEachSystem>();

            var entity1 = sandbox.CreateEntity().With<TestClassComponent1>();
            var entity2 = sandbox.CreateEntity().With<TestClassComponent1>().With<TestClassComponent2>();
            var entity3 = sandbox.CreateEntity().With<TestClassComponent1>().With<TestClassComponent2>().With<TestClassComponent3>();
            var entity4 = sandbox.CreateEntity().With<TestClassComponent1>().With<TestClassComponent3>();
            var entity5 = sandbox.CreateEntity().With<TestClassComponent2>().With<TestClassComponent3>();

            sandbox.Execute(new EventTickUpdate());

            Assert.IsTrue(system.Entities1.Contains(entity1));
            Assert.IsTrue(system.Entities2.Contains(entity2));
            Assert.IsTrue(system.Entities3.Contains(entity3));
            Assert.IsTrue(system.Entities4.Contains(entity4));

            var entity1Component1 = entity1.Get<TestClassComponent1>();
            Assert.IsTrue(entity1Component1.a == 1);

            Assert.IsTrue(entity2.Get<TestClassComponent1>().a == 2);
            Assert.IsTrue(Math.Abs(entity2.Get<TestClassComponent2>().a - 1.0) < 1e-5);

            Assert.IsTrue(entity3.Get<TestClassComponent1>().a == 3);
            Assert.IsTrue(Math.Abs(entity3.Get<TestClassComponent2>().a - 2.0) < 1e-5);
            Assert.IsTrue(entity3.Get<TestClassComponent3>().v1.Length() > 0);

            Assert.IsTrue(entity4.Get<TestClassComponent1>().b == 1);

            Assert.IsTrue(entity5.Get<TestClassComponent2>().a == 0 && entity5.Get<TestClassComponent2>().b == 0);
        }
    }

    public class TestSystem : SystemBase {
    }

    public class TestSubSystem : SystemBase {
    }

    public class TestForEachSystem : SystemBase {
        public readonly HashSet<Entity> Entities1 = new HashSet<Entity>();
        public readonly HashSet<Entity> Entities2 = new HashSet<Entity>();
        public readonly HashSet<Entity> Entities3 = new HashSet<Entity>();
        public readonly HashSet<Entity> Entities4 = new HashSet<Entity>();

        protected override void OnAdd() {
            Subscribe<EventTickUpdate>().ForEach<TestClassComponent1>(Execute);
            Subscribe<EventTickUpdate>().ForEach<TestClassComponent1, TestClassComponent2>(Execute);
            Subscribe<EventTickUpdate>()
                .ForEach<TestClassComponent1, TestClassComponent2, TestClassComponent3>(Execute);

            var matcher = Matcher.Include<TestClassComponent1, TestClassComponent3>().Exclude<TestClassComponent2>();
            Subscribe<EventTickUpdate>().OnMatch(matcher, Execute);
        }

        private void Execute(EventTickUpdate e, Entity entity, TestClassComponent1 component1) {
            component1.a++;
            Entities1.Add(entity);
        }

        private void Execute(EventTickUpdate e, Entity entity, TestClassComponent1 component1,
            TestClassComponent2 component2) {
            component1.a++;
            component2.a++;
            Entities2.Add(entity);
        }

        private void Execute(EventTickUpdate e, Entity entity, TestClassComponent1 component1,
            TestClassComponent2 component2, TestClassComponent3 component3) {
            component1.a++;
            component2.a++;
            component3.v1 = Vector4.One;
            Entities3.Add(entity);
        }

        private void Execute(EventTickUpdate e, Entity entity) {
            entity.Get<TestClassComponent1>().b++;
            Entities4.Add(entity);
        }
    }
}