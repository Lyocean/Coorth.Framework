
using NUnit.Framework;

namespace Coorth.Framework; 

public class TransformTests {
    
    private World world;
    
    [SetUp]
    public void Setup() {
        world = WorldTest.Create();

        world.BindComponent<HierarchyComponent>();
        world.AddSystem<HierarchySystem>();
        
        world.BindComponent<PositionComponent>();
        world.BindComponent<RotationComponent>();
        world.BindComponent<ScalingComponent>();
        world.BindComponent<TransformComponent>();
        world.BindComponent<LocalMatrixComponent>();
        world.BindComponent<WorldMatrixComponent>();
        world.AddSystem<TransformSystem>();
    }

    [Test]
    public void A() {
        
    }
    
}