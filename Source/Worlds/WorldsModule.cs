using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Coorth.Framework;
using Coorth.Logs;

namespace Coorth.Worlds;

public interface IWorldsModule : IModule {
    World Main { get; }

    void Register(IFeature<World> feature);
    World CreateWorld(string name);
}

[Module, Guid("26D3438C-F2CC-4DC3-AAAF-D5EC74E97E9D")]
public class WorldsModule : Module, IWorldsModule {
    
    private World? main;
    public World Main => main ?? throw new NullReferenceException();
    private readonly List<World> worlds = new();

    private readonly List<IFeature<World>> features = new();
    
    public void Register(IFeature<World> feature) {
        features.Add(feature);
        foreach (var world in worlds) {
            feature.Install(world);
        }
    }

    protected override void OnAdd() {
        base.OnAdd();
        Subscribe<EventGameStart>(OnStartup);
    }

    protected override void OnSetup() {
        LogUtil.Error("World Create");
        main = CreateWorld("World-Main");
    }

    protected override void OnClear() {
        LogUtil.Error("World Clear");

        main?.Dispose();
        main = null;
    }

    private void OnStartup(EventGameStart e) {
        
    }

    public World CreateWorld(string name) {
        var (app, module, actors) = (App, this, App.Actors);
        var options = new WorldOptions(name, app, module, app.Services);
        var domain = actors.CreateDomain($"World_{name}");
        var world = World.Create(domain, options);
        world.ManageBy(ref Managed);
        worlds.Add(world);
        foreach (var feature in features) {
            feature.Install(world);
        }
        return world;
    }

    protected override void OnActive() {
        Main.SetActive(true);
    }
        
    protected override void OnDeActive() {
        Main.SetActive(false);
    }
}