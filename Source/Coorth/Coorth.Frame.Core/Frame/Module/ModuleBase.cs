using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coorth {

    public abstract class TreeObject<TKey, TValue, TRoot> : Disposable where TValue : TreeObject<TKey, TValue, TRoot> where TRoot : TValue {

        private TKey Key { get; set; }

        public TValue Parent { get; private set; }

        private Dictionary<TKey, TValue> children;
        public IReadOnlyDictionary<TKey, TValue> Children => children;

        public TRoot Root => Parent == null ? (TRoot)this : Parent.Root;

        public void AddChild(TKey key, TValue child) {
            child.Parent = (TValue)this;
            children ??= new Dictionary<TKey, TValue>();
            children.Add(key, child);
            child.Key = key;
            OnChildAdd(key, child);
        }

        protected virtual void OnChildAdd(TKey key, TValue value) { }

        public bool RemoveChild(TKey key) {
            if (children == null) {
                return false;
            }
            if (children.TryGetValue(key, out var value)) {
                OnChildRemove(key, value);
                value.Parent = default;
                return children.Remove(key);
            }
            return false;
        }
        
        
        protected virtual void OnChildRemove(TKey key, TValue value) { }

        protected override void Dispose(bool dispose) {
            if (children == null) {
                return;
            }
            foreach (var pair in children) {
                OnChildRemove(pair.Key, pair.Value);
            }
            children.Clear();
            Parent?.RemoveChild(Key);
        }
    }
    
    public abstract class ModuleBase : TreeObject<Type, ModuleBase, RootModule> {

        protected Disposables Managed;

        public virtual ServiceContainer Services => Root.Services;
        
        public virtual EventDispatcher Dispatcher => Root.Dispatcher;
        
        public virtual ActorContainer Actors => Root.Actors;

        public World World => Root.App.CurrentWorld;

        public Sandbox Sandbox => World.Active;
        
        protected override void OnChildAdd(Type key, ModuleBase value) {
            value.OnAdd();
            Root.Dispatcher.Execute(new EventModuleAdd(this, key, value));
            // Root._OnAddModule(this, key, value);
        }

        protected override void OnChildRemove(Type key, ModuleBase value) {
            value.OnRemove();
            Managed.Clear();
            Root.Dispatcher.Execute(new EventModuleRemove(this, key, value));

            // Root.Dispatcher.Execute(new EventMd);
            // Root.Even(this, key, value);
        }

        protected virtual void OnAdd() { }

        protected virtual void OnRemove() { }

        public void Subscribe<T>(Action<T> action) where T : IEvent {
            var reaction = Dispatcher.Subscribe(action);
            Managed.Add(reaction);
        }
        
        public void Subscribe<T>(Func<T, Task> action) where T : IEvent {
            var reaction = Dispatcher.Subscribe(action);
            Managed.Add(reaction);
        }
        
        public void Subscribe<T>(EventDispatcher dispatcher, Action<T> action) where T : IEvent {
            var reaction = dispatcher.Subscribe(action);
            Managed.Add(reaction);
        }
        
        public void Subscribe<T>(EventDispatcher dispatcher, Func<T, Task> action) where T : IEvent {
            var reaction = dispatcher.Subscribe(action);
            Managed.Add(reaction);
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

    public sealed class RootModule : ModuleBase {
        public override ServiceContainer Services { get; } = new ServiceContainer();

        public override EventDispatcher Dispatcher { get; } = new EventDispatcher();

        private readonly ActorContainer actors;
        public override ActorContainer Actors => actors;

        private readonly AppFrame app;
        public AppFrame App => app;

        public RootModule(ServiceContainer parent, ActorContainer actors, AppFrame app) {
            parent.AddChild(Services);
            this.actors = actors;
            this.app = app;
        }
    }

    public abstract class ModuleBase<TParent> : ModuleBase where TParent : ModuleBase {
        public new TParent Parent => base.Parent as TParent;
        
        
    }
}