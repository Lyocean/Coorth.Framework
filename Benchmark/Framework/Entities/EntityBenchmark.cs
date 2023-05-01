using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Coorth.Logs;

namespace Coorth.Framework; 

[MemoryDiagnoser]
[SimpleJob(RunStrategy.Monitoring, launchCount:1, warmupCount:1, iterationCount:1, invocationCount:10_000)]
public class EntityBenchmark {
    
    private World world = default!;

    private const int ENTITY_COUNT = 1000;

    private Entity[] entities = new Entity[ENTITY_COUNT];
    
    [IterationSetup]
    public void Setup() {
        world = new World(new WorldOptions() {
            Name = "World",
            Services = new ServiceLocator(),
            Dispatcher = new Dispatcher(null!),
            Logger = new LoggerConsole(),
        });
    }

    [IterationCleanup]
    public void Dispose() {
        world.Dispose();
    }
    
    [Benchmark]
    public void CreateEntity() {
        world.CreateEntity();
    }
    
    [Benchmark]
    public void CreateEntities() {
        world.CreateEntities(entities.AsSpan());
    }
    
}