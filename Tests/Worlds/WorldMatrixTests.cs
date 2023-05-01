using System.Numerics;
using NUnit.Framework;

namespace Coorth.Framework; 

public class WorldMatrixTests {
    private World world;

    [SetUp]
    public void Setup() {
        world = WorldTest.NewWorld();
        
        world.BindComponent<PositionComponent>();
        world.BindComponent<RotationComponent>();
        world.BindComponent<ScalingComponent>();
        world.BindComponent<LocalMatrixComponent>();
        world.BindComponent<WorldMatrixComponent>();
        world.AddSystem<TransformSystem>();
    }
    
    [Test]
    public void SetPosition() {
        var entity = world.CreateEntity();
        entity.Add(new PositionComponent(Vector3.One));
        Assert.IsTrue(entity.Get<PositionComponent>().Value == Vector3.One);
    }

    [Test]
    public void SetRotation() {
        var entity = world.CreateEntity();
        entity.Add<RotationComponent>();
        Assert.IsTrue(entity.Get<RotationComponent>().Value == Quaternion.Identity);
    }
    
    [Test]
    public void SetScaling() {
        var entity = world.CreateEntity();
        ref var scaling = ref entity.Add<ScalingComponent>();
        scaling.Value = new Vector3(1, 2, 3);
        Assert.IsTrue(entity.Get<ScalingComponent>().Value == new Vector3(1, 2, 3));
    }
}