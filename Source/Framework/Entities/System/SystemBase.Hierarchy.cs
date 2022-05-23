using System;
using System.Collections.Generic;

namespace Coorth.Framework; 

public abstract partial class SystemBase {

    private Type? key;
    public Type Key => key ?? throw new NullReferenceException();

    private SystemBase? parent;
    public SystemBase Parent => parent ?? throw new NullReferenceException();

    private static readonly Dictionary<Type, SystemBase> empty = new();
    private Dictionary<Type, SystemBase>? children;
    public IReadOnlyDictionary<Type, SystemBase> Children => children ?? empty;
        
    public int ChildCount => children?.Count ?? 0;

    public bool IsSelfActive { get; private set; }

    private bool IsParentActive => parent == null || parent.IsActive;
        
    public bool IsActive => IsParentActive && IsSelfActive;
        
    protected Disposables Managed;

    protected Disposables Actives;

    protected ref Disposables Collector => ref (IsActive ? ref Actives : ref Managed);

    private void AddChild(Type k, SystemBase child) {
        child.parent?.RemoveChild(child.Key);
        child.parent = this;
            
        children ??= new Dictionary<Type, SystemBase>();
        children.Add(k, child);
        child.key = k;

        OnChildAdd();
        child.OnAdd();
    }

    private void OnChildAdd() {
    }

    private bool RemoveChild(Type k) {
        if (children == null) {
            return false;
        }
        if (!children.TryGetValue(k, out var child)) {
            return false;
        }
            
        child.OnRemove();
        OnChildRemove(child);
        child.Managed.Clear();

        child.parent = default;
        return children.Remove(k);
    }

    private void OnChildRemove(SystemBase child) {
        child.subscriptions.Clear();
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
        if (children == null) {
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