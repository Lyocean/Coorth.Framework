﻿using System;
using System.Collections.Generic;

namespace Coorth.Framework;

public partial class Module  {
        
    private static readonly IReadOnlyDictionary<Type, Module> empty = new Dictionary<Type, Module>();

    public Module? Parent { get; private set; }

    private Dictionary<Type, Module>? children;
    public IReadOnlyDictionary<Type, Module> Children => children ?? empty;
    
    public int ChildCount => children?.Count ?? 0;

    private bool isSetup;
        
    public bool IsSelfActive { get; private set; }

    public bool IsParentActive => Parent == null || Parent.IsHierarchyActive;

    public bool IsHierarchyActive => IsParentActive && IsSelfActive;

    public bool IsActive => IsHierarchyActive;

    private void AddChild(Type key, Module child) {
        children ??= new Dictionary<Type, Module>();
        children.Add(key, child);
        
        child.Key = key;
        child.Parent = this;
        child.root = Root;
        App.OnAddModule(key, child);
        child.OnAdd();
        if (child.isSetup) {
            return;
        }
        child.isSetup = true;
        child.OnSetup();
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
        child.RemoveChildren();
        App.OnRemoveModule(key, child);
        
        child.SetActive(false);
        if (child.isSetup) {
            child.isSetup = false;
            child.OnClear();
        }
        child.OnRemove();
        
        child.SetActive(false);

        child.Parent = null;
        return children.Remove(key);
    }

    private void RemoveChildren() {
        if (children == null) {
            return;
        }
        foreach (var (key, _) in children) {
            RemoveChild(key);
        }
        children.Clear();
    }
        
    public void Dispose() {
        Managed.Clear();
        Parent?.RemoveChild(Key);
    }
        
    protected virtual void OnAdd() { }

    protected virtual void OnSetup() { }
            
    protected virtual void OnActive() { }
            
    protected virtual void OnDeActive() { }

    protected virtual void OnClear() { }

    protected virtual void OnRemove() {  }

}