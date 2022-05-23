﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coorth.Framework; 

public interface ISystemSubscription {

    Type EventType { get; }
        
    IReadOnlyCollection<Type> IncludeComponents { get; }

    IReadOnlyCollection<Type> ExcludeComponents { get; }

}
    
public sealed partial class SystemSubscription<TEvent> : Disposable, ISystemSubscription where TEvent: notnull {

    private readonly SystemBase system;
        
    private readonly Dispatcher dispatcher;

    private Reaction<TEvent>? reaction;

    public Type EventType => typeof(TEvent);

    private Sandbox Sandbox => system.Sandbox;

    private EntityMatcher? matcher;
        
    private HashSet<Type>? includes;
        
    private HashSet<Type>? excludes;

    public IReadOnlyCollection<Type> IncludeComponents => includes ?? (IReadOnlyCollection<Type>)Array.Empty<Type>();
        
    public IReadOnlyCollection<Type> ExcludeComponents => excludes ?? (IReadOnlyCollection<Type>)Array.Empty<Type>();
        
    public SystemSubscription(SystemBase system, Dispatcher dispatcher) {
        this.system = system;
        this.dispatcher = dispatcher;
    }
        
    private void ValidateReaction() {
        if (reaction != null) {
            throw new InvalidOperationException("[SystemSubscription] Can't add action duplicate.");
        }
    }
        
    public void OnEvent(EventAction<TEvent> action) {
        ValidateReaction();
        reaction = dispatcher.Subscribe(action);
    }

    public void OnEvent(Action<TEvent> action) {
        ValidateReaction();
        reaction = dispatcher.Subscribe(action);
    }
        
    public void OnEvent(Func<TEvent, ValueTask> action) {
        ValidateReaction();
        reaction = dispatcher.Subscribe(action);
    }

    public void OnEvent(EventFunc<TEvent, ValueTask> action) {
        ValidateReaction();
        reaction = dispatcher.Subscribe(action);
    }
        
    public void OnEvent(Func<TEvent, Task> action) {
        ValidateReaction();
        reaction = dispatcher.Subscribe(action);
    }

    public void OnEvent(EventFunc<TEvent, Task> action) {
        ValidateReaction();
        reaction = dispatcher.Subscribe(action);
    }
        
    private void _Include(Type type) {
        includes ??= new HashSet<Type>();
        includes.Add(type);
    }

    private void _Include<T>() where T : IComponent => _Include(typeof(T));

    private void _Exclude(Type type) {
        excludes ??= new HashSet<Type>();
        excludes.Add(type);
    }
        
    private void _Exclude<T>() where T : IComponent => _Exclude(typeof(T));
        
    public SystemSubscription<TEvent> Include<T>() where T : IComponent {
        matcher ??= new EntityMatcher();
        matcher.Include<T>();
        _Include<T>();
        return this;
    }
        
    public SystemSubscription<TEvent> Include<T1, T2>() where T1 : IComponent where T2 : IComponent {
        matcher ??= new EntityMatcher();
        matcher.Include<T1>().Include<T2>();
        _Include<T1>();
        _Include<T2>();
        return this;
    }
        
    public SystemSubscription<TEvent> Include<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent{
        matcher ??= new EntityMatcher();
        matcher.Include<T1>().Include<T2>().Include<T3>();
        _Include<T1>();
        _Include<T2>();
        _Include<T3>();
        return this;
    }
       
    public SystemSubscription<TEvent> Exclude<T>() where T : IComponent {
        matcher ??= new EntityMatcher();
        matcher.Exclude<T>();
        _Exclude<T>();
        return this;
    }
        
    public SystemSubscription<TEvent> Exclude<T1, T2>() where T1 : IComponent where T2 : IComponent {
        matcher ??= new EntityMatcher();
        matcher.Exclude<T1>().Exclude<T2>();
        _Exclude<T1>();
        _Exclude<T2>();
        return this;
    }
        
    public SystemSubscription<TEvent> Exclude<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent{
        matcher ??= new EntityMatcher();
        matcher.Exclude<T1>().Exclude<T2>().Exclude<T3>();
        _Exclude<T1>();
        _Exclude<T2>();
        _Exclude<T3>();
        return this;
    }
        
    protected override void OnDispose(bool dispose) {
        reaction?.Dispose();
        system.RemoveReaction(this);
    }
}