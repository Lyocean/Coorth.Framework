using Coorth.Logs;
using NUnit.Framework;

namespace Coorth.Framework; 

public class WorldTest {

    public static World NewWorld() {
        var world = new World(new WorldOptions() {
            Name = "World",
            Services = new ServiceLocator(),
            Dispatcher = new Dispatcher(null!),
            Logger = new LoggerConsole(),
        });
        return world;
    }
    
    [Test]
    public void CreateWorld() {
        var world = NewWorld();
        Assert.NotNull(world);
        Assert.NotNull(world.Options);
        Assert.IsFalse(world.IsDisposed);

        world.Dispose();
        Assert.IsTrue(world.IsDisposed);
    }
        
    [Test]
    public void BindComponent() {
        var world = NewWorld();
        world.BindComponent<TestValueComponent0>();
    }
        
    [Test]
    public void Service() {
        var world = NewWorld();
        Assert.IsNull(world.FindService(typeof(WorldTest)));
        Assert.IsNull(world.FindService<WorldTest>());
    }
        
    public static World Create() {
        var world = NewWorld();
        world.BindComponent<TestValueComponent0>();
        world.BindComponent<TestValueComponent1>();
        world.BindComponent<TestValueComponent2>();
        world.BindComponent<TestValueComponent3>();
        world.BindComponent<TestValueComponent4>();

        world.BindComponent<TestClassComponent0>();
        world.BindComponent<TestClassComponent1>();
        world.BindComponent<TestClassComponent2>();
        world.BindComponent<TestClassComponent3>();
        world.BindComponent<TestClassComponent4>();

        return world;
    }
}