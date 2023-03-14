using BenchmarkDotNet.Attributes;
using Coorth.Logs;

namespace Coorth.Framework; 

public class EntityBenchmark {

    private World world = default!;

    private int ENTITY_COUNT = 1_000_000;
    
    [GlobalSetup]
    public void Setup() {
        world = new World(new WorldOptions() {
            Name = "World",
            Services = new ServiceLocator(),
            Dispatcher = new Dispatcher(null!),
            Logger = new LoggerConsole(),
        });
        world.BindComponent<HierarchyComponent>();
        world.BindComponent<TransformComponent>();
    }

    [IterationSetup]
    public void IterationSetup() {
        
    }

    [IterationCleanup]
    public void IterationClear() {
        world.Clear();
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

    [Benchmark]
    public void AddComponent1() {
        for (var i = 0; i < ENTITY_COUNT; i++) {
            var entity = world.CreateEntity();
            entity.Add<HierarchyComponent>();
        }
    }
    
    [Benchmark]
    public void AddComponent2() {
        for (var i = 0; i < ENTITY_COUNT; i++) {
            var entity = world.CreateEntity();
            entity.Add<HierarchyComponent>();
            entity.Add<TransformComponent>();
        }
    }
}