using System;
using System.Collections.Generic;


namespace Coorth {
    public partial class Module  {
        
        private static readonly IReadOnlyDictionary<Type, Module> empty = new Dictionary<Type, Module>();

        public Module? Parent { get; private set; }

        private Dictionary<Type, Module>? children;
        public IReadOnlyDictionary<Type, Module> Children => children ?? empty;
        
        public int ChildCount => children?.Count ?? 0;

        private bool isSetup;

        private bool isActive;
        
        public bool IsSelfActive { get; private set; }

        public bool IsParentActive => Parent == null || Parent.IsHierarchyActive;

        public bool IsHierarchyActive => IsParentActive && IsSelfActive;

        public bool IsActive => IsHierarchyActive;

        private void AddChild(Type key, Module child) {
            children ??= new Dictionary<Type, Module>();
            children.Add(key, child);
            child._OnAdd(this);
        }

        private void _OnAdd(Module parent) {
            Parent = parent;
            OnAdd();
            if (isSetup) {
                return;
            }
            isSetup = true;
            OnSetup();
        }
        
        public void SetActive(bool active) {
            if (IsSelfActive == active) {
                return;
            }
            IsSelfActive = active;
            if (!IsParentActive) {
                return;
            }
            _OnActive(active);
        }

        private void _OnActive(bool active) {
            if (active) {
                OnActive();
            }
            else {
                OnDeActive();
            }
            if (children == null) {
                return;
            }
            foreach (var (_, child) in children) {
                if (!IsSelfActive) {
                    continue;
                }
                child._OnActive(active);
            }
        }

        private bool RemoveChild(Type key) {
            if (children == null) {
                return false;
            }
            if (!children.TryGetValue(key, out var child)) {
                return false;
            }
            child._OnRemove();
            return children.Remove(key);
        }

        private void _OnRemove() {
            SetActive(false);
            OnRemove();
            Clear();
            Parent = default;
        }

        private void Clear() {
            if (isSetup) {
                isSetup = false;
                OnClear();
            }
            RemoveAll();
        }

        public void RemoveAll() {
            if (children == null) {
                return;
            }
            foreach (var (_, child) in children) {
                child._OnRemove();
            }
            children.Clear();
        }
        
        public void Dispose() {
            Parent?.RemoveChild(Key);
        }
        
        protected virtual void OnAdd() { }

        protected virtual void OnSetup() { }
            
        protected virtual void OnActive() { }
            
        protected virtual void OnDeActive() { }

        protected virtual void OnClear() { }

        protected virtual void OnRemove() {  }

        
        //
        // public abstract Type Key { get; }
        //
        // public DictTreeNode<Type, Module> Node { get; } = new();
        //
        // public IReadOnlyDictionary<Type, Module> Children => Node.Children;
        // public int ChildCount => Node.ChildCount;
        //
        // public Module Parent => Node.Parent ?? throw new InvalidOperationException("Can't access parent before OnAdd.");
        //
        // public Module Root => Parent.Root;
        //
        // public bool IsActive => Node.IsHierarchyActive;
        // public bool IsActiveSelf => Node.IsSelfActive;
        // public bool IsActiveHierarchy => Node.IsHierarchyActive;
        //
        // private bool isInit = false;
        //
        // void IDictTreeObject<Type, Module>.OnAdd(Module parent) {
        //     Root.Dispatcher.Dispatch(new EventModuleAdd(parent, Key, this));
        //     this.OnAdd();
        //     if (!isInit) {
        //         this.OnSetup();
        //         isInit = true;
        //     }
        // }
        //
        // void IDictTreeObject<Type, Module>.OnActive() {
        //     OnActive();
        // }
        //
        // void IDictTreeObject<Type, Module>.OnDeActive() {
        //     OnDeActive();
        //     Actives.Clear();
        // }
        //
        // void IDictTreeObject<Type, Module>.OnRemove(Module parent) {
        //     if (isInit) {
        //         this.OnClear();
        //         isInit = false;
        //     }
        //     Root.Dispatcher.Dispatch(new EventModuleRemove(parent, Key, this));
        //     OnRemove();
        //     Managed.Clear();
        // }
        //
        // public void SetActive(bool active) => Node.SetActive(this, active);
        //
        //
        // protected virtual void OnSetup() { }
        //
        // protected virtual void OnClear() { }
        
    } 
}
