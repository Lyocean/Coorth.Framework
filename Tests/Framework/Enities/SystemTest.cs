using System;
using System.Collections.Generic;
using System.Numerics;
using Coorth.Logs;
using NUnit.Framework;

namespace Coorth.Framework;

internal class SystemTest {
    private World world;

    [SetUp]
    public void Setup() {
        world = WorldTest.Create();
    }

    [Test]
    public void AddSystem() {
        var system = world.AddSystem<TestSystem>();
        Assert.NotNull(system);

        Assert.Catch(() => { world.AddSystem<TestSystem>(); });
    }

    [Test]
    public void HasSystem() {
        world.AddSystem<TestSystem>();
        Assert.IsTrue(world.HasSystem<TestSystem>());
    }

    [Test]
    public void RemoveSystem1() {
        world.AddSystem<TestSystem>();
        Assert.IsTrue(world.RemoveSystem<TestSystem>());
    }

    [Test]
    public void RemoveSystem() {
        world.AddSystem<TestSystem>();
        Assert.IsTrue(world.RemoveSystem<TestSystem>());
        Assert.IsFalse(world.HasSystem<TestSystem>());
    }

    [Test]
    public void SubSystem() {
        var system = world.AddSystem<TestSystem>();
        var subSystem = system.AddChild<TestSubSystem>();
        Assert.NotNull(subSystem);

        Assert.IsTrue(world.HasSystem<TestSystem>());
        Assert.IsTrue(system.HasChild<TestSubSystem>());

        Assert.AreSame(subSystem.Parent, system);

        Assert.IsTrue(world.RemoveSystem<TestSubSystem>());
        Assert.IsFalse(world.HasSystem<TestSubSystem>());
        Assert.IsFalse(system.HasChild<TestSubSystem>());
    }

    [Test]
    public void SystemReactionX1() {
        var system = world.AddSystem<TestForEachSystem>();
        var entity1 = world.CreateEntity().With<TestClassComponent1>();
        world.Dispatch(new TickUpdateEvent());

        Assert.IsTrue(system.Entities1.Contains(entity1));
        Assert.IsTrue(entity1.Get<TestClassComponent1>().a == 1);
    }

    [Test]
    public void SystemReactionX2() {
        var system = world.AddSystem<TestForEachSystem>();
        var entity2 = world.CreateEntity().With<TestClassComponent1>().With<TestClassComponent2>();
        world.Dispatch(new TickUpdateEvent());

        Assert.IsTrue(system.Entities2.Contains(entity2));
        Assert.IsTrue(entity2.Get<TestClassComponent1>().a == 2);
        Assert.IsTrue(Math.Abs(entity2.Get<TestClassComponent2>().a - 1) < 1e-5);
    }

    [Test]
    public void SystemReactionX3() {
        var system = world.AddSystem<TestForEachSystem>();
        var entity3 = world.CreateEntity().With<TestClassComponent1>().With<TestClassComponent2>()
            .With<TestClassComponent3>();
        world.Dispatch(new TickUpdateEvent());

        Assert.IsTrue(system.Entities3.Contains(entity3));
        Assert.IsTrue(entity3.Get<TestClassComponent1>().a == 3);
        Assert.IsTrue(Math.Abs(entity3.Get<TestClassComponent2>().a - 2) < 1e-5);

        Assert.IsTrue(Math.Abs(entity3.Get<TestClassComponent3>().v1.Length() - 2.0) < 1e-5);
    }

    [Test]
    public void SystemReactionMatch0() {
        var system = world.AddSystem<TestForEachSystem>();
        var archetype = world.CreateArchetype<TestClassComponent1, TestClassComponent3>();
        var entity4 = archetype.CreateEntity();
        // var entity4 = world.CreateEntity().With<TestClassComponent1>().With<TestClassComponent3>();
        world.Dispatch(new TickUpdateEvent());
 
        Assert.IsTrue(system.Entities4.Contains(entity4));
        Assert.IsTrue(entity4.Get<TestClassComponent1>().a == 1);
        Assert.IsTrue(entity4.Get<TestClassComponent1>().b == 1);
    }

    [Test]
    public void SystemReactionMatch1() {
        var system = world.AddSystem<TestForEachSystem>();
        // var archetype = world.CreateArchetype<TestClassComponent1, TestClassComponent3>();
        // var entity4 = archetype.CreateEntity();
        var entity4 = world.CreateEntity().With<TestClassComponent1>().With<TestClassComponent3>();
        world.Dispatch(new TickUpdateEvent());

        Assert.IsTrue(system.Entities4.Contains(entity4));
        Assert.IsTrue(entity4.Get<TestClassComponent1>().a == 1);
        Assert.IsTrue(entity4.Get<TestClassComponent1>().b == 1);
    }

    
    [Test]
    public void SystemReactionMatch2() {
        var system = world.AddSystem<TestForEachSystem>();
        var entity5 = world.CreateEntity().With<TestClassComponent1>().With<TestClassComponent2>();
        world.Dispatch(new TickUpdateEvent());

        Assert.IsFalse(system.Entities4.Contains(entity5));
        Assert.IsTrue(entity5.Get<TestClassComponent1>().a == 2);
    }


    [Test]
    public void SystemReactionMulti() {
        var system = world.AddSystem<TestForEachSystem>();

        var entity1 = world.CreateEntity().With<TestClassComponent1>();
        var entity2 = world.CreateEntity().With<TestClassComponent1>().With<TestClassComponent2>();
        var entity3 = world.CreateEntity().With<TestClassComponent1>().With<TestClassComponent2>()
            .With<TestClassComponent3>();
        var entity4 = world.CreateEntity().With<TestClassComponent1>().With<TestClassComponent3>();
        var entity5 = world.CreateEntity().With<TestClassComponent2>().With<TestClassComponent3>();

        world.Dispatch(new TickUpdateEvent());

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