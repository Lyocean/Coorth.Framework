using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Coorth.Framework;

public abstract partial class Module : IModule {

    public Type Key { get; set; } = typeof(Module);

    private ModuleRoot? root;
    public virtual ModuleRoot Root => root ?? throw new NullReferenceException();

    public virtual AppFrame App => Root.App;

    public virtual ServiceLocator Services => Root.Services;

    public virtual Dispatcher Dispatcher => Root.Dispatcher;
    
    public virtual ActorsRuntime Actors => Root.Actors;

    public virtual ActorLocalDomain LocalDomain => Root.LocalDomain;

    protected Disposables Managed;

    protected Disposables Actives;

    protected ref Disposables Collector => ref (IsActive ? ref Actives : ref Managed);


    public TModule AddModule<TKey, TModule>(TModule module) where TModule : Module, TKey where TKey : IModule {
        var key = typeof(TKey);
        AddChild(key, module);
        return module;
    }
    
    public TModule AddModule<TModule>() where TModule : Module, new() {
        var module = new TModule();
        var key = typeof(TModule);
        AddChild(key, module);
        return module;
    }
        
    public TModule AddModule<TModule>(Module module) {
        var type = typeof(TModule);
        AddChild(type, module);
        return (TModule)(object)module;
    }
        
    public void AddModule(Type key, Module module) => AddChild(key, module);

    public TKey GetModule<TKey>() where TKey : IModule {
        var key = typeof(TKey);
        var module = GetModule(key);
        if (module is TKey result) {
            return result;
        }
        throw new KeyNotFoundException(key.ToString());
    }

    public Module GetModule(Type key) => Children[key];
        
    public Module? FindModule(Type key) => Children.TryGetValue(key, out var module) ? module : default;

    public bool TryGetModule(Type type, [NotNullWhen(true)]out Module? module) {
        return Children.TryGetValue(type, out module);
    }

    public bool HasModule(Type type) => Children.ContainsKey(type);

    public bool HasModule<TModule>() => Children.ContainsKey(typeof(TModule));

    public TModule OfferModule<TModule>() where TModule : Module, new() {
        return Children.TryGetValue(typeof(TModule), out var module) ? (TModule)module : AddModule<TModule>();
    }
}

public static class ModuleExtension {
    public static ModuleScope AsScope(this IModule module) {
        return new ModuleScope((Module) module);
    }
}