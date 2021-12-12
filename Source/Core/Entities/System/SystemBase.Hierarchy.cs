using System;
using System.Collections.Generic;

namespace Coorth {
    public abstract partial class SystemBase {
        
        public Type Key { get; private set; }
        
        public SystemBase Parent { get; private set; }

        private Dictionary<Type, SystemBase> children;
        public IReadOnlyDictionary<Type, SystemBase> Children => children;
        
        public int ChildCount => children?.Count ?? 0;

        public bool IsSelfActive { get; private set; }

        private bool IsParentActive => Parent == null || Parent.IsActive;
        
        public bool IsActive => IsParentActive && IsSelfActive;
        
        protected Disposables Managed;

        protected Disposables Actives;

        protected ref Disposables Collector => ref (IsActive ? ref Actives :  ref Managed);

        private void AddChild(Type key, SystemBase child) {
            child.Parent?.RemoveChild(child.Key);
            child.Parent = (SystemBase) this;
            
            children ??= new Dictionary<Type, SystemBase>();
            children.Add(key, child);
            child.Key = key;

            OnChildAdd(key, child);
            child.OnAdd();
        }

        private void OnChildAdd(Type key, SystemBase value) {
        }

        private bool RemoveChild(Type key) {
            if (children == null) {
                return false;
            }
            if (!children.TryGetValue(key, out var child)) {
                return false;
            }
            
            child.OnRemove();
            OnChildRemove(key, child);
            child.Managed.Clear();

            child.Parent = default;
            return children.Remove(key);
        }

        private void OnChildRemove(Type key, SystemBase child) {
            child.reactions.Clear();
            child.ClearSystems();
        }

        public void SetActive(bool active) {
            if (IsSelfActive == active) {
                return;
            }
            IsSelfActive = active;
            if (!IsParentActive) {
                return;
            }
            OnSetActive(active);
        }

        private void OnSetActive(bool active) {
            if (active) {
                OnActive();
            } else {
                OnDeActive(); 
                Actives.Clear();
            }
            if (Children == null) {
                return;
            }
            foreach (var pair in children) {
                if (!pair.Value.IsSelfActive) {
                    continue;
                }
                pair.Value.OnSetActive(active);
            }
        }


        protected virtual void OnAdd() {
        }

        protected virtual void OnRemove() {
        }
        
        protected virtual void OnActive() {
        }

        protected virtual void OnDeActive() {
        }
    }
}