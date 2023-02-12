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
        var builder = world.CreateArchetype();
        var archetypeCompiled = builder.Compile();
        Assert.IsFalse(archetypeCompiled.IsNull);
        var entity = archetypeCompiled.CreateEntity();
        Assert.IsTrue(world.HasEntity(entity));
    }
        
    [Test]
    public void ArchetypeAddComponent() {
        var builder = world.CreateArchetype();
        builder.Add<TestValueComponent0>()
            .Add<TestValueComponent1>()
            .Add<TestClassComponent2>();
        var archetypeCompiled = builder.Compile();
        Assert.IsTrue(archetypeCompiled.Has<TestValueComponent0>());
        Assert.IsTrue(archetypeCompiled.Has<TestValueComponent1>());
        Assert.IsTrue(archetypeCompiled.Has<TestClassComponent2>());
            
        Assert.IsFalse(archetypeCompiled.Has<TestClassComponent0>());
        Assert.IsFalse(archetypeCompiled.Has<TestClassComponent1>());
        Assert.IsFalse(archetypeCompiled.Has<TestValueComponent2>());
    }
        
    [Test]
    public void ArchetypeCreateEntity() {
        var builder = world.CreateArchetype();
        builder.Add<TestValueComponent0>()
            .Add<TestValueComponent1>()
            .Add<TestClassComponent2>();
        var archetypeCompiled = builder.Compile();
        var entity = archetypeCompiled.CreateEntity();

        Assert.IsTrue(world.HasEntity(entity));
        Assert.IsTrue(entity.Has<TestValueComponent0>());
        Assert.IsTrue(entity.Has<TestValueComponent1>());
        Assert.IsTrue(entity.Has<TestClassComponent2>());
    }
        
    [Test]
    public void ArchetypeCreateEntityX1000() {
        var builder = world.CreateArchetype();
        builder.Add<TestValueComponent0>()
            .Add<TestValueComponent1>()
            .Add<TestClassComponent2>();
        var archetypeCompiled = builder.Compile();

        for (int i = 0; i < 1000; i++) {
            var entity = archetypeCompiled.CreateEntity();
            Assert.IsTrue(world.HasEntity(entity));
            Assert.IsTrue(entity.Has<TestValueComponent0>());
            Assert.IsTrue(entity.Has<TestValueComponent1>());
            Assert.IsTrue(entity.Has<TestClassComponent2>());
        }
        Assert.IsTrue(world.EntityCount == 1001);
    }
}