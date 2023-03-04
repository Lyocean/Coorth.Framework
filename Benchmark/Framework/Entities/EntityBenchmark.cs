using BenchmarkDotNet.Attributes;
using Coorth.Logs;

namespace Coorth.Framework; 

public class EntityBenchmark {

    private World world = default!;

    private int ENTITY_COUNT = 1024;
    
    [GlobalSetup]
    public void Setup() {
    }

    [IterationSetup]
    public void IterationSetup() {
        world = new World(new WorldOptions() {
            Name = "World",
            Services = new ServiceLocator(),
            Dispatcher = new Dispatcher(null!),
            Logger = new LoggerConsole(),
        });
    }

    [IterationCleanup]
    public void IterationClear() {
        world.Dispose();
    }

    [Benchmark]
    public void CreateEntity() {
        for (var i = 0; i < ENTITY_COUNT; i++) {
            world.CreateEntity();
        }
    }
    
    [Benchmark]
    public void CreateEntities() {
        world.CreateEntities(ENTITY_COUNT);
    }
    
    
}