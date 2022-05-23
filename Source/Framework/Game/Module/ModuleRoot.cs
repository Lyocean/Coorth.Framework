using System;
using Coorth.Framework;

namespace Coorth.Framework;

public sealed class ModuleRoot : Module {

    public override ModuleRoot Root => this;

    public override AppFrame App { get; }

    public override ServiceLocator Services { get; }

    public override Dispatcher Dispatcher { get; }

    public override ActorsRuntime Actors { get; }

    public override ActorLocalDomain LocalDomain { get; }

    public ModuleRoot(AppFrame app, ServiceLocator services, Dispatcher dispatcher, ActorsRuntime actors) {
        App = app;
        Services = services;
        Dispatcher = dispatcher;
        Actors = actors;
        LocalDomain = Actors.CreateDomain("Root");
    }
}