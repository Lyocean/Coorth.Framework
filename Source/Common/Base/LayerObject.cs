using System.Collections.Generic;

namespace Coorth {
    public abstract class LayerObject<TKey, TValue> : Disposable 
                                                where TValue : LayerObject<TKey, TValue>  {

        public TKey Key { get; private set; }
        
        public TValue Parent { get; private set; }

        private Dictionary<TKey, TValue> children;
        public IReadOnlyDictionary<TKey, TValue> Children => children;
        
        public int ChildCount => children?.Count ?? 0;

        public bool IsSelfActive { get; private set; }

        private bool IsParentActive => Parent == null || Parent.IsActive;
        
        public bool IsActive => IsParentActive && IsSelfActive;
        
        protected Disposables Managed;

        protected Disposables Actives;

        protected ref Disposables Collector => ref (IsActive ? ref Actives :  ref Managed);

        protected void AddChild(TKey key, TValue child) {
            child.Parent?.RemoveChild(child.Key);
            child.Parent = (TValue) this;
            
            children ??= new Dictionary<TKey, TValue>();
            children.Add(key, child);
            child.Key = key;

            OnChildAdd(key, child);
            child.OnAdd();
        }
        
        protected virtual void OnChildAdd(TKey key, TValue value) {
        }

        protected bool RemoveChild(TKey key) {
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
        
        protected virtual void OnChildRemove(TKey key, TValue value) {
        }
        
        protected override void OnDispose(bool dispose) {
            if (children != null) {
                foreach (var value in children.Values) {
                    value.Dispose();
                }
                children.Clear();
            }
            Parent?.RemoveChild(Key);
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