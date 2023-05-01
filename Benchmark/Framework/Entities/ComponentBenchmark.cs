using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Coorth.Logs;

namespace Coorth.Framework; 

[MemoryDiagnoser]
[SimpleJob(RunStrategy.Monitoring, launchCount:1, warmupCount:1, iterationCount:1, invocationCount:100_000)]
public class ComponentBenchmark {
    private World world;

    private Entity[] entities;
    
    [IterationSetup]
    public void Setup() {
        world = new World(new WorldOptions() {
            Name = "World",
            Services = new ServiceLocator(),
            Dispatcher = new Dispatcher(null!),
            Logger = new LoggerConsole(),
        });
        entities = new Entity[1000];
        for (var i = 0; i < entities.Length; i++) {
            entities[i] = world.CreateEntity();
            entities[i].Add<DescriptionComponent>();
        }
    }

    [IterationCleanup]
    public void Dispose() {
        world.Dispose();
    }
    
    [Benchmark]
    public void AddComponent1() {
        for (var i = 0; i < entities.Length; i += 2) {
            entities[i].Add<HierarchyComponent>();
        }
    }
    
    [Benchmark]
    public void AddComponent2() {
        for (var i = 0; i < entities.Length; i += 2) {
            entities[i].Add<TransformComponent>();
        }
    }
    
    [Benchmark]
    public void RemoveComponent2() {
        for (var i = 0; i < entities.Length; i += 2) {
            entities[i].Remove<DescriptionComponent>();
        }
    }
}