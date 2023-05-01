using NUnit.Framework;

namespace Coorth.Framework; 

public class DescriptionTests {
    private World world;
    
    [SetUp]
    public void Setup() {
        world = WorldTest.NewWorld();
        
        world.BindComponent<DescriptionComponent>();
        world.AddSystem<DescriptionSystem>();
    }

    [Test]
    public void SetName() {
        var name = "Test";
        var entity = world.CreateEntity();
        entity.SetName(name);
        
        Assert.IsTrue(entity.GetName() == name);
    }
    
    [Test]
    public void SetFlag() {
        var entity = world.CreateEntity();
        entity.SetFlag(5, true);
        
        Assert.IsTrue(entity.GetFlag(5));
        Assert.IsTrue(entity.GetFlag(7) == false);
    }
}