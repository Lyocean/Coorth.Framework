using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Coorth.Framework;

public abstract class ServiceNode<T> : ServiceBase, IDisposable where T : ServiceNode<T> {
    private Type? key;
    public Type Key => key ?? throw new NullReferenceException();

    private T? parent;
    public T Parent => parent ?? throw new NullReferenceException();

    protected T? root;
    public T Root => root ?? throw new NullReferenceException();

    private static readonly Dictionary<Type, T> empty = new();
    private Dictionary<Type, T>? children;
    public IReadOnlyDictionary<Type, T> Children => children ?? empty;

    public int ChildCount => children?.Count ?? 0;

    public bool IsDisposed { get; private set; }

    public bool IsSelfActive { get; private set; }

    private bool IsParentActive => parent == null || parent.IsActive;

    public override bool IsActive => IsParentActive && IsSelfActive;


    protected void _AddChild(Type k, T child, bool active) {
        child.key = k;
        child.parent = (T)this;
        child.root = root;

        children ??= new Dictionary<Type, T>();
        children.Add(k, child);

        OnChildAdd(child);
        child.OnAdd();
        child.SetActive(active);
    }

    public T AddChild(Type type) {
        var child = Activator.CreateInstance(type) as T;
        if (child == null) {
            throw new InvalidDataException($"Create instance failed: {type}");
        }
        _AddChild(type, child, true);
        return child;
    }

    public TKey AddChild<TKey>(TKey child) where TKey: notnull {
        _AddChild(typeof(TKey), (T)(object)child, true);
        return child;
    }

    public TKey AddChild<TKey>(Type type, TKey child) where TKey: notnull {
        _AddChild(type, (T)(object)child, true);
        return child;
    }

    public TChild AddChild<TChild>() where TChild : T, new() {
        var child = new TChild();
        _AddChild(typeof(TChild), child, true);
        return child;
    }

    public void AddChild(Type type, T child) => _AddChild(type, child, true);


    public T OfferChild(Type type) => Children.TryGetValue(type, out var child) ? child : AddChild(type);

    public TChild OfferChild<TChild>() where TChild : T, new() {
        return Children.TryGetValue(typeof(TChild), out var child) ? (TChild)child : AddChild<TChild>();
    }


    public bool HasChild(Type type) => Children.ContainsKey(type);

    public bool HasChild<TChild>() => Children.ContainsKey(typeof(TChild));


    public T GetChild(Type type) => Children[type];

    public TKey GetChild<TKey>() where TKey : notnull => (TKey)(object)Children[typeof(TKey)];

    public T? FindChild(Type type) => Children.TryGetValue(type, out var child) ? child : default;

    public TKey? FindChild<TKey>() where TKey : notnull {
        return Children.TryGetValue(typeof(TKey), out var child) ? (TKey)(object)child : default;
    }


    public bool TryGetChild(Type type, [NotNullWhen(true)] out T? child) {
        return Children.TryGetValue(type, out child);
    }

    public bool TryGetChild<TChild>(Type type, [NotNullWhen(true)] out TChild? child) where TChild : T {
        if (Children.TryGetValue(type, out var result) && result is TChild value) {
            child = value;
            return true;
        }

        child = default;
        return false;
    }


    public T[] GetChildren() => Children.Values.ToArray();

    protected bool _RemoveChild(Type type) {
        if (children == null || !children.TryGetValue(type, out var child)) {
            return false;
        }

        child.SetActive(false);
        child.OnRemove();
        OnChildRemove(child);

        child.ClearChildren();

        child.Managed.Clear();
        child.Actives.Clear();

        child.parent = default;
        return children.Remove(type);
    }

    public bool RemoveChild(Type type) => _RemoveChild(type);
    
    public bool RemoveChild<TChild>() => _RemoveChild(typeof(TChild));

    public void ClearChildren() {
        if (children == null || children.Count == 0) {
            return;
        }

        var keys = children.Keys.ToList();
        foreach (var k in keys) {
            _RemoveChild(k);
        }

        children.Clear();
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
        }
        else {
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


    public void Dispose() {
        if (IsDisposed) {
            return;
        }
        IsDisposed = true;
        ClearChildren();
        parent?._RemoveChild(Key);
        OnDispose();
    }

    protected virtual void OnAdd() {
    }

    protected virtual void OnChildAdd(T child) {
    }

    protected virtual void OnRemove() {
    }

    protected virtual void OnChildRemove(T child) {
    }

    protected virtual void OnActive() {
    }

    protected virtual void OnDeActive() {
    }

    protected virtual void OnDispose() {
    }
}