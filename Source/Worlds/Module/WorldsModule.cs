using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coorth.Logs;

namespace Coorth.Framework;

public interface IWorldsModule : IModule {
    ValueTask<IWorldActor> GetActiveAsync();
    ValueTask<IWorldActor> CreateWorldAsync(string name);
    ValueTask<IWorldActor?> FindWorldAsync(ActorId id);
}

[Module, Guid("26D3438C-F2CC-4DC3-AAAF-D5EC74E97E9D")]
public class WorldsModule : ModuleBase, IWorldsModule {
    
    private World? active;
    public World Active => active ?? throw new NullReferenceException();
    
    private readonly List<World> worlds = new();
    public IReadOnlyList<World> Worlds => worlds;
    
    private readonly List<IFeature<World>> features = new();

    private ILogger Logger { get; set; } = LoggerNull.Instance;

    protected override void OnAdd() {
        Logger = Services.Get<ILogManager>().Create("Worlds");
        active = CreateWorld("Active");
    }

    protected override void OnActive() {
        foreach (var world in worlds) {
            world.SetActive(true);
        }
    }

    protected override void OnDeActive() {
        foreach (var world in worlds) {
            world.SetActive(false);
        }
    }

    protected override void OnRemove() {
        foreach (var world in worlds) {
            world.Dispose();
        }
        worlds.Clear();
    }

    public void Register(IFeature<World> feature) {
        features.Add(feature);
        foreach (var world in worlds) {
            feature.Install(world);
        }
    }

    public World CreateWorld(string name) {
        
        Logger.Trace($"CreateWorld: {name}");
        var logger = Services.Get<ILogManager>().Create($"World:{name}");
        var options = new WorldOptions() {
            Name = name,
            Services = Services.CreateChild(),
            Dispatcher = Dispatcher.CreateChild(),
            Logger = logger,
            SyncContext = App.SyncContext
        };
        var world = new World(options);
        Node.CreateChild(ActorId.New(), new ActorOptions(name, -1, -1), world.Actor);

        worlds.Add(world);
        world.Singleton().Add(new WorldComponent(this, App));
        foreach (var feature in features) {
            feature.Install(world);
        }
        return world;
    }

    public ValueTask<IWorldActor> GetActiveAsync() {
        return new ValueTask<IWorldActor>(Active.Actor);
    }

    public ValueTask<IWorldActor> CreateWorldAsync(string name) {
        var world = CreateWorld(name);
        return new ValueTask<IWorldActor>(world.Actor);
    }

    public ValueTask<IWorldActor?> FindWorldAsync(ActorId id) {
        foreach (var world in worlds) {
            if (world.Actor.ActorId == id) {
                return new ValueTask<IWorldActor?>(world.Actor);
            }
        }
        return new ValueTask<IWorldActor?>(null as IWorldActor);
    }
}