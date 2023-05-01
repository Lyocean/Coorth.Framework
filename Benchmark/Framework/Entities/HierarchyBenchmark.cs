using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Coorth.Logs;

namespace Coorth.Framework; 

[MemoryDiagnoser]
[SimpleJob(RunStrategy.Monitoring, launchCount:1, warmupCount:1, iterationCount:1, invocationCount:1_000)]
public class HierarchyBenchmark : IDisposable {
    
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
            entities[i].Add<HierarchyComponent>();
        }
    }

    [IterationCleanup]
    public void Dispose() {
        world.Dispose();
    }
    
    [Benchmark]
    public void SetParent() {
        for (var i = 0; i < entities.Length; i += 2) {
            entities[i].SetParent(entities[i + 1]);
        }
    }
}