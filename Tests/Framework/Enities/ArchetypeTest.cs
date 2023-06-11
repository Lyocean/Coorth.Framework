using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Coorth.Framework; 

public class ArchetypeTest {
        
    private World world;
        
    [SetUp]
    public void Setup() {
        world = WorldTest.Create();
    }

    // [Test]
    // public void Archetype_Null() {
    //     var archetype = new ArchetypeInstance();
    //     Assert.IsTrue(archetype.IsNull);
    // }

    [Test]
    public void Archetype_World() {
        var archetype = world.BuildArchetype().Build();
        Assert.IsTrue(archetype.World == world);
    }
        
    [Test]
    public void Archetype_Empty() {
        var entity = world.CreateEntity();
        var archetype1 = world.GetArchetype(entity.Id);
        var archetype2 = entity.Archetype;
        var archetype3 = world.BuildArchetype().Build();

        Assert.IsTrue(archetype1.ComponentCount == 0);
        Assert.IsTrue(archetype2.ComponentCount == 0);
        Assert.IsTrue(archetype3.ComponentCount == 0);
            
        Assert.IsTrue(archetype1 == archetype2);
        Assert.IsTrue(archetype1 == archetype3);
    }
        
    [Test]
    public void Archetype_AddComponent_1() {
        var archetype1 = world.BuildArchetype().Add<TestClassComponent0>().Build();
            
        Assert.IsTrue(archetype1.ComponentCount == 1);
            
        Assert.IsTrue(archetype1.HasComponent<TestClassComponent0>());
        Assert.IsFalse(archetype1.HasComponent<TestClassComponent1>());
        Assert.IsFalse(archetype1.HasComponent<TestClassComponent2>());
            
        var archetype2 = world.BuildArchetype().Add(typeof(TestClassComponent0)).Build();
        Assert.IsTrue(archetype2.ComponentCount == 1);

    }

    [Test]
    public void Archetype_AddComponent_N() {
        var archetype1 = world.BuildArchetype().Add<TestClassComponent0>().Build();
        var archetype2 = world.BuildArchetype().Add<TestClassComponent0>().Add<TestClassComponent1>().Build();
        var archetype3 = world.BuildArchetype().Add<TestClassComponent0>().Add<TestClassComponent1>().Add<TestClassComponent2>().Build();
            
        Assert.IsTrue(archetype1.ComponentCount == 1);
        Assert.IsTrue(archetype2.ComponentCount == 2);
        Assert.IsTrue(archetype3.ComponentCount == 3);

        Assert.IsTrue(archetype2.HasComponent<TestClassComponent0>());
        Assert.IsTrue(archetype2.HasComponent<TestClassComponent1>());
        Assert.IsFalse(archetype2.HasComponent<TestClassComponent2>());
            
        Assert.IsTrue(archetype3.HasComponent<TestClassComponent0>());
        Assert.IsTrue(archetype3.HasComponent<TestClassComponent1>());
        Assert.IsTrue(archetype3.HasComponent<TestClassComponent2>());
        Assert.IsFalse(archetype3.HasComponent<TestClassComponent3>());
    }

    [Test]
    public void Archetype_CreateEntity() {
        var archetype0 = world.BuildArchetype().Build();
        var entity0 = archetype0.CreateEntity();
        Assert.IsTrue(entity0.Count == 0);
        Assert.IsTrue(!entity0.Has<TestClassComponent0>());
            
        var archetype1 = world.BuildArchetype().Add<TestClassComponent0>().Build();
        var entity1 = archetype1.CreateEntity();
        Assert.IsTrue(entity1.Count == 1);
        Assert.IsTrue(entity1.Has<TestClassComponent0>() && !entity1.Has<TestClassComponent1>());
            
        var archetype2 = world.BuildArchetype().Add<TestClassComponent0>().Add<TestClassComponent1>().Build();
        var entity2 = archetype2.CreateEntity();
        Assert.IsTrue(entity2.Count == 2);
        Assert.IsTrue(entity2.Has<TestClassComponent0>() && entity2.Has<TestClassComponent1>());
    }

    [Test]
    public void Archetype_CreateEntities1() {
        var archetype = world.BuildArchetype().Add<TestClassComponent0>().Build();
        var entities = archetype.CreateEntities(10);
        foreach (var entity in entities) {
            Assert.IsTrue(!entity.IsNull && entity.Count == 1 && entity.Has<TestClassComponent0>());
        }
        var entities2 = new Entity[10];
        archetype.CreateEntities(entities2.AsSpan());
    }
        
    [Test]
    public void Archetype_CreateEntities2() {
        var archetype = world.BuildArchetype().Add<TestClassComponent0>().Build();
        var array = new Entity[10];
        archetype.CreateEntities(array.AsSpan());
        foreach (Entity entity in array) {
            Assert.IsTrue(!entity.IsNull && entity.Count == 1 && entity.Has<TestClassComponent0>());
        }
    }
        
    [Test]
    public void Archetype_CreateEntities3() {
        var archetype = world.BuildArchetype().Add<TestClassComponent0>().Build();
        var list = new List<Entity>(1);
        archetype.CreateEntities(list, 1);
        foreach (var entity in list) {
            Assert.IsTrue(!entity.IsNull && entity.Count == 1 && entity.Has<TestClassComponent0>());
        }
    }
}