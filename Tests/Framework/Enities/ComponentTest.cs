using System.Collections.Generic;
using NUnit.Framework;

namespace Coorth.Framework; 

internal class ComponentTest {
    private Sandbox sandbox;
        
    [SetUp]
    public void Setup() {
        sandbox = SandboxTest.Create();
    }
        
    [Test]
    public void AddComponent() {
        var entity = sandbox.CreateEntity();
        entity.Add<TestValueComponent0>();
        Assert.IsTrue(entity.Count == 1);
        Assert.IsTrue(sandbox.ComponentCount<TestValueComponent0>() == 1);
    }
        
    [Test]
    public void AddComponent2() {
        var entity1 = sandbox.CreateEntity();
        entity1.Add<TestClassComponent0>();
        var entity2 = sandbox.CreateEntity();
        entity2.Add<TestClassComponent0>();
            
        Assert.IsFalse(ReferenceEquals(entity1.Get<TestClassComponent0>(), entity2.Get<TestClassComponent0>()));
        Assert.IsTrue(sandbox.ComponentCount<TestClassComponent0>() == 2);
    }
        
    [Test]
    public void SingletonComponent() {
        var component1 = sandbox.Singleton<TestClassComponent0>();
        var component2 = sandbox.Singleton<TestClassComponent0>();
        Assert.IsTrue(ReferenceEquals(component1, component2));
    }

    [Test]
    public void HasComponent() {
        var entity = sandbox.CreateEntity();
        entity.Add<TestValueComponent0>();
        Assert.IsTrue(entity.Has<TestValueComponent0>());
    }
        
    [Test]
    public void GetComponent() {
        var entity = sandbox.CreateEntity();
        entity.Add(new TestValueComponent1(){ a =  5, b = 6});
        var component = entity.Get<TestValueComponent1>();
        Assert.IsTrue(component.a == 5 && component.b == 6);
    }

    [Test]
    public void RefComponent() {
        var entity = sandbox.CreateEntity();
        entity.Add(new TestValueComponent1() { a = 5, b = 6 });

        ref var component = ref entity.Get<TestValueComponent1>();

        Assert.IsTrue(component.a == 5);
        component.a = 8;

        var component2 = entity.Get<TestValueComponent1>();
        Assert.IsTrue(component2.a == 8);
    }

    [Test]
    public void RemoveComponent() {
        var entity = sandbox.CreateEntity();
        entity.Add<TestValueComponent0>();
        entity.Remove<TestValueComponent0>();
        Assert.IsTrue(entity.Count == 0);
        Assert.IsFalse(entity.Has<TestValueComponent0>());
        Assert.IsTrue(sandbox.ComponentCount<TestValueComponent0>() == 0);
    }
        
    [Test]
    public void ClearComponent() {
        var entity = sandbox.CreateEntity();
        entity.Add<TestValueComponent0>();
        entity.Add<TestClassComponent2>();
        entity.Clear();
        Assert.IsTrue(entity.Count == 0);
    }
        
    [Test]
    public void EnableComponent() {
        var entity = sandbox.CreateEntity();
        entity.Add<TestValueComponent0>();
        entity.Add<TestClassComponent2>();
            
        Assert.IsTrue(entity.IsEnable<TestValueComponent0>());
        Assert.IsTrue(entity.IsEnable<TestClassComponent2>());
            
        entity.SetEnable<TestValueComponent0>(false);
        Assert.IsFalse(entity.IsEnable<TestValueComponent0>());
        Assert.IsTrue(entity.IsEnable<TestClassComponent2>());
            
        entity.SetEnable<TestValueComponent0>(false);
        Assert.IsFalse(entity.IsEnable<TestValueComponent0>());
            
        entity.SetEnable<TestClassComponent2>(false);
        Assert.IsFalse(entity.IsEnable<TestValueComponent0>());
        Assert.IsFalse(entity.IsEnable<TestClassComponent2>());
    }

    // [Test]
    // public void EnableForEachComponent() {
    //     var Count = 10;
    //     var system = sandbox.OfferSystem<TestEnableSystem>();
    //     system.Count = 0;
    //     var entities = new Entity[Count];
    //     for (var i = 0; i < Count; i++) {
    //         var entity = sandbox.CreateEntity();
    //         entity.Add<TestValueComponent0>();
    //         entities[i] = entity;
    //     }
    //     Assert.IsTrue(system.Count == 0);
    //     sandbox.Execute(new EventTickUpdate());
    //     Assert.IsTrue(system.Count == Count);
    //     system.Count = 0;
    //     
    //     for (var i = 0; i < 3; i++) {
    //         entities[i].SetEnable<TestValueComponent0>(false);
    //     }
    //     sandbox.Execute(new EventTickUpdate());
    //     Assert.IsTrue(system.Count == 7);
    //
    // }
}