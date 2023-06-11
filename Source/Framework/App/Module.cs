using System;
using System.Threading.Tasks;


namespace Coorth.Framework;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
public class ModuleAttribute : Attribute { }

public interface IModule : IActor { }

public abstract class ModuleBase : ServiceNode<ModuleBase>, IModule, IActorLifetime {
    
    public virtual AppBase App => Root.App;

    private ActorLocalNode? node;
    public ActorLocalNode Node => node ?? throw new NullReferenceException();
    
    protected override IServiceLocator Services => Root.Services;

    protected override Dispatcher Dispatcher => Root.Dispatcher;
    
    public virtual ActorsRuntime Actors => Root.Actors;
    
    public virtual ActorLocalDomain LocalDomain => Root.LocalDomain;
    
    protected void Subscribe<TEvent>(Action<TEvent> action) where TEvent : notnull {
        var reaction = Dispatcher.Subscribe(action);
        Collector.Add(reaction);
    }
    
    protected void Subscribe<TEvent>(ActionI1<TEvent> action) where TEvent : notnull {
        var reaction = Dispatcher.Subscribe(action);
        Collector.Add(reaction);
    }
    
    protected void Subscribe<TEvent>(Func<TEvent, Task> action) where TEvent : notnull {
        var reaction = Dispatcher.Subscribe(action);
        Collector.Add(reaction);
    }
    
    protected override void OnChildAdd(ModuleBase child) {
        App.OnAddModule(child.Key, child);
    }

    protected override void OnChildRemove(ModuleBase child) {
        App.OnRemoveModule(child.Key, child);
    }

    public void Setup(ActorLocalNode value) {
        node = value;
        OnSetup();
    }
    
    protected virtual void OnSetup() { }

    public void Clear() {
        OnClear();
        node = null;
    }
    
    protected virtual void OnClear() { }

}

public sealed class ModuleRoot : ModuleBase {

    public override AppBase App { get; }

    protected override IServiceLocator Services { get; }

    protected override Dispatcher Dispatcher { get; }

    public override ActorsRuntime Actors { get; }

    public override ActorLocalDomain LocalDomain { get; }

    public ModuleRoot(in AppBase app, in ServiceLocator services, in Dispatcher dispatcher, in ActorsRuntime actors) {
        root = this;
        App = app;
        Services = services;
        Dispatcher = dispatcher;
        Actors = actors;
        LocalDomain = Actors.CreateDomain("Root");
    }
}
