using System;
using System.Collections.Generic;

namespace Coorth {
    
    public interface IModule : IDisposable { }
    
    public abstract partial class Module : IModule {

        public abstract Type Key { get; }

        public abstract ModuleRoot Root { get; }
        
        public abstract AppFrame App { get; }

        public abstract ServiceLocator Services { get; }

        public abstract EventDispatcher Dispatcher { get; }

        public TModule AddModule<TInterface, TModule>(TModule module) where TModule : Module<TInterface>, TInterface {
            var key = typeof(TInterface);
            AddChild(key, module);
            return module;
        }
        
        public TModule AddModule<TModule>() where TModule : Module, new() {
            var module = new TModule();
            var key = typeof(TModule);
            AddChild(key, module);
            return module;
        }
        
        public TModule AddModule<TModule>(TModule module) where TModule : Module {
            var type = typeof(TModule);
            AddChild(type, module);
            return module;
        }
        
        public void AddModule(Type key, Module module) => AddChild(key, module);

        public TModule GetModule<TModule>() where TModule : IModule {
            var module = GetModule(typeof(TModule));
            if (module is TModule result) {
                return result;
            }
            throw new KeyNotFoundException(typeof(TModule).ToString());
        }

        public Module GetModule(Type type) {
            return Children.TryGetValue(type, out var module) ? module : throw new KeyNotFoundException(type.ToString());
        }
        
        public Module? TryGetModule(Type type) {
            return Children.TryGetValue(type, out var module) ? module : default;
        }
        
        public bool TryGetModule(Type type, out Module? module) {
            return Children.TryGetValue(type, out module);
        }
        
        public bool HasModule(Type type) {
            return Children.ContainsKey(type);
        }
        
        public bool HasModule<TModule>() {
            return Children.ContainsKey(typeof(TModule));
        }

        public TModule OfferModule<TModule>() where TModule : Module, new() {
            return HasModule<TModule>() ? GetModule<TModule>() : AddModule<TModule>();
        }
    }

    public abstract partial class Module<TKey> : Module {
        
        public override Type Key => typeof(TKey);
        
        public override ModuleRoot Root => Parent?.Root ?? throw new InvalidOperationException();
        
        public override AppFrame App => Root.App;

        public override ServiceLocator Services => Root.Services;
        
        public override EventDispatcher Dispatcher => Root.Dispatcher;
        
        public ActorRuntime Actors => Root.Actors;

        public LocalDomain Domain => Root.Domain;

    }
}