using NUnit.Framework;

namespace Coorth.Framework; 

public class EntityTest {

    private World world;
        
    [SetUp]
    public void Setup() {
        world = WorldTest.Create();
    }
        
    [Test]
    public void ValidateWorld() {
        Assert.NotNull(world);
    }

    [Test]
    public void CreateEmptyEntity() {
        var entity = world.CreateEntity();
        Assert.IsTrue(entity.World == world);
        Assert.IsTrue(!entity.IsNull);
    }
        
    [Test]
    public void CreateEmptyEntitiesX2() {
        var entity1 = world.CreateEntity();
        var entity2 = world.CreateEntity();
        Assert.IsTrue(entity1 != entity2);
    }
        
    [Test]
    public void CreateEmptyEntitiesX1000() {
        for (var i = 0; i < 1000; i++) {
            var entity = world.CreateEntity();
            Assert.IsFalse(entity.IsNull);
        }
    }

    [Test]
    public void SingletonEntity() {
        var entity = world.Singleton();
        Assert.IsFalse(entity.Id.IsNull);
        Assert.IsFalse(entity.IsNull);
        var entity1 = world.Singleton();
        var entity2 = world.Singleton();
        Assert.IsTrue(entity1 == entity2);

        var entity3 = world.CreateEntity();
        Assert.IsFalse(entity == entity3);

    }
        
    [Test]
    public void HasEntity() {
        var entity = world.CreateEntity();
        Assert.IsTrue(world.HasEntity(entity));
        Assert.IsTrue(world.HasEntity(entity.Id));
    }

    [Test]
    public void GetEntity() {
        var entity = world.CreateEntity();
        Assert.IsTrue(world.GetEntity(entity.Id) == entity);
    }

    [Test]
    public void DestroyEntity() {
        var entity = world.CreateEntity();
        entity.Dispose();
        Assert.IsFalse(world.HasEntity(entity));
    }

    [Test]
    public void CreateArchetype() {
        var archetype = world.BuildArchetype().Build();
        // Assert.IsFalse(archetype.IsNull);
        var entity = archetype.CreateEntity();
        Assert.IsTrue(world.HasEntity(entity));
    }
        
    [Test]
    public void ArchetypeAddComponent() {
        var archetype = world.BuildArchetype()
                            .Add<TestValueComponent0>()
                            .Add<TestValueComponent1>()
                            .Add<TestClassComponent2>()
                            .Build();
        // var archetypeCompiled = builder.Build();
        Assert.IsTrue(archetype.HasComponent<TestValueComponent0>());
        Assert.IsTrue(archetype.HasComponent<TestValueComponent1>());
        Assert.IsTrue(archetype.HasComponent<TestClassComponent2>());
            
        Assert.IsFalse(archetype.HasComponent<TestClassComponent0>());
        Assert.IsFalse(archetype.HasComponent<TestClassComponent1>());
        Assert.IsFalse(archetype.HasComponent<TestValueComponent2>());
    }
        
    [Test]
    public void ArchetypeCreateEntity() {
        var archetype = world.BuildArchetype()
                            .Add<TestValueComponent0>()
                            .Add<TestValueComponent1>()
                            .Add<TestClassComponent2>()
                            .Build();
        var entity = archetype.CreateEntity();

        Assert.IsTrue(world.HasEntity(entity));
        Assert.IsTrue(entity.Has<TestValueComponent0>());
        Assert.IsTrue(entity.Has<TestValueComponent1>());
        Assert.IsTrue(entity.Has<TestClassComponent2>());
    }
        
    [Test]
    public void ArchetypeCreateEntityX1000() {
        var archetype = world.BuildArchetype()
                            .Add<TestValueComponent0>()
                            .Add<TestValueComponent1>()
                            .Add<TestClassComponent2>()
                            .Build();

        for (int i = 0; i < 1000; i++) {
            var entity = archetype.CreateEntity();
            Assert.IsTrue(world.HasEntity(entity));
            Assert.IsTrue(entity.Has<TestValueComponent0>());
            Assert.IsTrue(entity.Has<TestValueComponent1>());
            Assert.IsTrue(entity.Has<TestClassComponent2>());
        }
        Assert.IsTrue(world.EntityCount == 1001);
    }
}