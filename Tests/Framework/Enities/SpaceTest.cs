using NUnit.Framework;

namespace Coorth.Framework; 

public class SpaceTest {
    private World world;
    
    [SetUp]
    public void Setup() {
        world = WorldTest.Create();
    }
    
    [Test]
    public void CreateSpace() {
        world.CreateSpace();
    }
    
    [Test]
    public void HasSpace() {
        var space = world.CreateSpace();
        Assert.IsTrue(world.HasSpace(space));
    }
    
    [Test]
    public void DestroySpace() {
        var space = world.CreateSpace();
        space.Dispose();
    }
    
    [Test]
    public void CreateEntityInSpace() {
        var space = world.CreateSpace();
        var entity = world.CreateEntity(space);
        Assert.IsTrue(entity.Space == space);

        var entity1 = space.CreateEntity();
        Assert.IsTrue(entity1.Space == space);
    }
}