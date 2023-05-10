using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Coorth.Logs;

namespace Coorth.Framework; 

[MemoryDiagnoser]
// [HardwareCounters(HardwareCounter.CacheMisses)]
public class ComponentBenchmark {
    
    private World world = default!;

    private Entity[] entities = default!;
    
    [Params(EntityConst.COUNT)]
    public int EntityCount { get; set; }
    
    [IterationSetup]
    public void Setup() {
        world = new World(new WorldOptions() {
            Name = "World",
            Services = new ServiceLocator(),
            Dispatcher = new Dispatcher(null!),
            Logger = new LoggerConsole(),
        });
        entities = new Entity[EntityCount];
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