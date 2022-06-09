using System;
using System.Collections.Generic;

namespace Coorth.Framework;

public partial class AppFrame : IModuleContainer {
        
    private readonly ModuleRoot root;

    private readonly Dictionary<Type, Module> modules = new();

    private ModuleRoot InitModules() {
        var module = new ModuleRoot(this, Services, Dispatcher, Actors);
        module.SetActive(false);
        return module;
    }

    void IModuleContainer.OnAddModule(Type key, Module module) {
        modules[key] = module;
        Dispatcher.Dispatch(new EventModuleAdd(key, module));
    }
    
    void IModuleContainer.OnRemoveModule(Type key, Module module) {
        if (!modules.Remove(key)) {
            return;
        }
        Dispatcher.Dispatch(new EventModuleRemove(key, module));
    }
    
    public TModule Bind<TModule>(TModule module) where TModule : IModule {
        root.AddModule(typeof(TModule), (Module)(object)module);
        return module;
    }
    
    public TKey Bind<TKey, TModule>(Func<AppFrame, TModule> func)  where TModule : Module, TKey where TKey : IModule {
        var module = func(this);
        return Bind<TKey, TModule>(module);
    }
        
    public TModule Bind<TModule>() where TModule : Module, new() {
        var module = new TModule();
        return Bind<TModule, TModule>(module);
    }
    
    public TModule Bind<TKey, TModule>(TModule module) where TModule : Module, TKey where TKey : IModule {
        root.AddModule<TKey>(module);
        return module;
    }

    public TKey Get<TKey>() where TKey : IModule {
        return modules.TryGetValue(typeof(TKey), out var module) ? (TKey)(object)module : throw new KeyNotFoundException();
    }
    
    public TKey? Find<TKey>() where TKey : IModule {
        return modules.TryGetValue(typeof(TKey), out var module) ? (TKey)(object)module : default;
    }
    
    public Module Get(Type key) {
        return modules.TryGetValue(key, out var module) ? module : throw new KeyNotFoundException();
    }
    
    public bool Remove(Type key) {
        if (!modules.TryGetValue(key, out var module)) {
            return false;
        }
        module.Dispose();
        return true;
    }
    
    public bool Remove<TKey>() where TKey : IModule {
        return Remove(typeof(TKey));
    }
}