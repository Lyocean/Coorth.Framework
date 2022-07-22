using System;
using System.Collections.Generic;
using Coorth.Framework;
using Coorth.Logs;

namespace Coorth.Worlds;

public sealed partial class World : Actor, IDisposable {

    public readonly string Name;

    public readonly AppFrame App;

    public readonly WorldsModule Module;
    
    public readonly IServiceLocator Services;

    public ActorRef ActorRef { get; private set; }

    private readonly List<Sandbox> sandboxes = new();
    public IReadOnlyList<Sandbox> Sandboxes => sandboxes;

    public Sandbox Active { get; private set; }

    public Dispatcher Dispatcher => App.Dispatcher;
    
    public World(WorldOptions options) {
        Name = options.Name ?? $"[World]";
        App = options.App;
        Module = options.Module;
        Services = options.Services;
        ActorRef = ActorRef.Null;
        Active = CreateSandbox("[Sandbox_Active]");
    }

    public static World Create(ActorLocalDomain domain, WorldOptions options) {
        var world = new World(options);
        world.ActorRef = domain.CreateActor(world, options.Name);
        Cosmos.Register(world);
        return world;
    }

    public void ChangeActive(Sandbox sandbox) {
        if (sandboxes.Exists(s => s == sandbox)) {
            Active = sandbox;
        }
    }

    public void SetActive(bool active) {
        for (var i = 0; i < sandboxes.Count; i++) {
            var sandbox = sandboxes[i];
            sandbox.SetActive(active);
        }
    }

    public Sandbox CreateSandbox(string name) {
        Module.Logger.Trace("Create Sandbox");
        var logger = Services.Get<ILogManager>().Create(name);
        var options = new SandboxOptions() {
            Name = name,
            Services = Services,
            Dispatcher = Dispatcher.CreateChild(),
            Logger = logger,
        };
        var sandbox = new Sandbox(options);
        sandboxes.Add(sandbox);
        return sandbox;
    }

    public void Dispose() {
        Cosmos.Remove(this);
        foreach (var sandbox in sandboxes) {
            sandbox.Dispose();
        }
        sandboxes.Clear();
    }
}