using System;
using System.Threading.Tasks;

namespace Coorth {
    public abstract class ModuleBase : LayerObject<Type, ModuleBase> {

        public ModuleRoot Root => Parent == null ? (ModuleRoot) this : Parent.Root;

        public virtual ServiceLocator Services => Root.Services;
        
        public virtual EventDispatcher Dispatcher => Root.Dispatcher;
        
        public virtual ActorRuntime Actors => Root.Actors;

        public World World => Root.App.CurrentWorld;

        protected Infra Infra => Root.App.Infra;
        
        public Sandbox Sandbox => World.Active;
        
        protected override void OnChildAdd(Type key, ModuleBase value) {
            Root.Dispatcher.Dispatch(new EventModuleAdd(this, key, value));
        }

        protected override void OnChildRemove(Type key, ModuleBase value) {
            Root.Dispatcher.Dispatch(new EventModuleRemove(this, key, value));
        }

        public void Subscribe<T>(Action<T> action) where T : IEvent {
            var reaction = Dispatcher.Subscribe(action);
            Collector.Add(reaction);
        }
        
        public void Subscribe<T>(Func<T, Task> action) where T : IEvent {
            var reaction = Dispatcher.Subscribe(action);
            Collector.Add(reaction);
        }
        
        public void Subscribe<T>(EventDispatcher dispatcher, Action<T> action) where T : IEvent {
            var reaction = dispatcher.Subscribe(action);
            Collector.Add(reaction);
        }
        
        public void Subscribe<T>(EventDispatcher dispatcher, Func<T, Task> action) where T : IEvent {
            var reaction = dispatcher.Subscribe(action);
            Collector.Add(reaction);
        }
        
        public TModule AddModule<TModule>(TModule module = default) where TModule : ModuleBase, new() {
            var type = module != null ? module.GetType() : typeof(TModule);
            module ??= Activator.CreateInstance<TModule>();
            AddChild(type, module);
            return module;
        }

        public TModule GetModule<TModule>() {
            var module = GetModule(typeof(TModule));
            if (module is TModule result) {
                return result;
            }
            return default;
        }

        public ModuleBase GetModule(Type type) {
            return Children.TryGetValue(type, out var module) ? module : default;
        }
        
        public bool HasModule(Type type) {
            return Children.ContainsKey(type);
        }
        
        public bool HasModule<TModule>() where TModule : ModuleBase {
            return Children.ContainsKey(typeof(TModule));
        }

        public TModule OfferModule<TModule>()  where TModule : ModuleBase, new() {
            return HasModule<TModule>() ? GetModule<TModule>() : AddModule<TModule>();
        }
    }

    public abstract class ModuleBase<TParent> : ModuleBase where TParent : ModuleBase {
        public new TParent Parent => base.Parent as TParent;
    }
}