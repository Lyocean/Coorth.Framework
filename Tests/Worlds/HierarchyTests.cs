using NUnit.Framework;

namespace Coorth.Framework; 

public class HierarchyTests {
    private World world;

    [SetUp]
    public void Setup() {
        world = WorldTest.NewWorld();
        
        world.BindComponent<HierarchyComponent>();
        world.AddSystem<HierarchySystem>();
    }
    
    [Test]
    public void AddHierarchy() {
        var entity = world.CreateEntity();
        ref var hierarchy = ref entity.Add<HierarchyComponent>();
        Assert.IsTrue(hierarchy.Entity == entity);
        Assert.IsTrue(hierarchy.Count == 0);
        Assert.IsTrue(hierarchy.ParentEntity == Entity.Null);
    }

    [Test]
    public void SetParent() {
        var entity = world.CreateEntity();
        var parent = world.CreateEntity();
        entity.Offer<HierarchyComponent>().SetParent(parent);
        Assert.IsTrue(parent.Count == 1);
        Assert.IsTrue(entity.Offer<HierarchyComponent>().ParentEntity == parent);
    }
}