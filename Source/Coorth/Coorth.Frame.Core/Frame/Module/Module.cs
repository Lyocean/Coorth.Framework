using System;
using System.Collections.Generic;

namespace Coorth {

    public abstract class TreeObject<TKey, TValue, TRoot> : Disposable where TValue : TreeObject<TKey, TValue, TRoot> where TRoot : TValue {

        private TKey Key { get; set; }

        public TValue Parent { get; private set; }

        private Dictionary<TKey, TValue> children;
        public IReadOnlyDictionary<TKey, TValue> Children => children;

        public TRoot Root => Parent == null ? (TRoot)this : Parent.Root;

        public void AddChild(TKey key, TValue child) {
            child.Parent = (TValue)this;
            children = children ?? new Dictionary<TKey, TValue>();
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
    
    public abstract class Module : TreeObject<Type, Module, RootModule> {

        protected Disposables Managed;

        public virtual ServiceContainer Services => Root.Services;
        
        public virtual EventDispatcher Dispatcher => Root.Dispatcher;
        
        protected override void OnChildAdd(Type key, Module value) {
            value.OnAdd();
            Root.Dispatcher.Execute(new EventModuleAdd(this, key, value));
            // Root._OnAddModule(this, key, value);
        }

        protected override void OnChildRemove(Type key, Module value) {
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
        
        public TModule AddModule<TModule>(TModule module = default) where TModule : Module, new() {
            var type = module != null ? module.GetType() : typeof(TModule);
            module  = module ?? Activator.CreateInstance<TModule>();
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

        public Module GetModule(Type type) {
            return Children.TryGetValue(type, out var module) ? module : default;
        }
        
        public bool HasModule(Type type) {
            return Children.ContainsKey(type);
        }
        
        public bool HasModule<TModule>() where TModule : Module {
            return Children.ContainsKey(typeof(TModule));
        }

        public TModule OfferModule<TModule>()  where TModule : Module, new() {
            return HasModule<TModule>() ? GetModule<TModule>() : AddModule<TModule>();
        }
    }

    public sealed class RootModule : Module {
        public override ServiceContainer Services { get; } = new ServiceContainer();

        public override EventDispatcher Dispatcher { get; } = new EventDispatcher();

        
        public RootModule(ServiceContainer parent) {
            parent.AddChild(Services);
        }
    }
}