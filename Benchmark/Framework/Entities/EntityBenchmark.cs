using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Coorth.Logs;

namespace Coorth.Framework; 

[MemoryDiagnoser]
[HardwareCounters(HardwareCounter.CacheMisses)]
public class EntityBenchmark {
    
    private World world = default!;
    
    [Params(EntityConst.COUNT)]
    public int EntityCount { get; set; }
    
    private readonly Entity[] entities = new Entity[EntityConst.COUNT];

    private Archetype archetype1;

    private Archetype archetype2;
    
    private Archetype archetype3;

    [IterationSetup]
    public void Setup() {
        world = new World(new WorldOptions() {
            Name = "World",
            Services = new ServiceLocator(),
            Dispatcher = new Dispatcher(null!),
            Logger = new LoggerConsole(),
        });
        world.BindComponent<HierarchyComponent>();
        world.BindComponent<LifetimeComponent>();
        world.BindComponent<PositionComponent>();
        archetype1 = world.BuildArchetype()
            .Add<HierarchyComponent>()
            .Build();
        archetype2 = world.BuildArchetype()
            .Add<HierarchyComponent>()
            .Add<PositionComponent>()
            .Build();
        archetype3 = world.BuildArchetype()
            .Add<HierarchyComponent>()
            .Add<PositionComponent>()
            .Add<LifetimeComponent>()
            .Build();

    }

    [IterationCleanup]
    public void Dispose() {
        world.Dispose();
    }
    
    [Benchmark]
    public void CreateEntities0() {
        for (var i = 0; i < EntityConst.COUNT; i++) {
            world.CreateEntity();
        }
    }
    
    [Benchmark]
    public void CreateEntities1() {
        world.CreateEntities(entities.AsSpan());
    }
    
    [Benchmark]
    public void CreateEntitiesWith1Component() {
        archetype1.CreateEntities(entities.AsSpan());
    }
    
    [Benchmark]
    public void CreateEntitiesWith2Component() {
        archetype2.CreateEntities(entities.AsSpan());
    }
    
    [Benchmark]
    public void CreateEntitiesWith3Component() {
        archetype3.CreateEntities(entities.AsSpan());
    }
    
    

}