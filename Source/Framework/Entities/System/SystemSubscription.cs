using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coorth.Tasks;

namespace Coorth.Framework;

public interface ISystemSubscription {
    
    Type EventType { get; }

    IReadOnlyCollection<Type> AllTypes { get; }

    IReadOnlyCollection<Type> NotTypes { get; }
    
    IReadOnlyCollection<Type> AnyTypes { get; }
}

public sealed partial class SystemSubscription<TEvent> : Disposable, ISystemSubscription where TEvent : notnull {
    private readonly SystemBase system;

    private readonly Dispatcher dispatcher;

    private readonly TaskExecutor? executor;

    private Reaction<TEvent>? reaction;

    public Type EventType => typeof(TEvent);

    private World World => system.World;

    private Matcher? Matcher { get; set; }

    private HashSet<Type>? allTypes;

    private HashSet<Type>? notTypes;

    private HashSet<Type>? anyTypes;

    public IReadOnlyCollection<Type> AllTypes => allTypes ?? (IReadOnlyCollection<Type>)Array.Empty<Type>();

    public IReadOnlyCollection<Type> NotTypes => notTypes ?? (IReadOnlyCollection<Type>)Array.Empty<Type>();

    public IReadOnlyCollection<Type> AnyTypes => anyTypes ?? (IReadOnlyCollection<Type>)Array.Empty<Type>();

    public SystemSubscription(SystemBase system, Dispatcher dispatcher, TaskExecutor? executor) {
        this.system = system;
        this.dispatcher = dispatcher;
        this.executor = executor;
    }

    protected override void OnDispose() {
        reaction?.Dispose();
        system.RemoveReaction(this);
    }

    private void ValidateReaction() {
        if (reaction != null) {
            throw new InvalidOperationException("[SystemSubscription] Can't add action duplicate.");
        }
    }

    public void OnEvent(ActionI1<TEvent> action) {
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

    public void OnEvent(FuncI1<TEvent, ValueTask> action) {
        ValidateReaction();
        reaction = dispatcher.Subscribe(action);
    }

    public void OnEvent(Func<TEvent, Task> action) {
        ValidateReaction();
        reaction = dispatcher.Subscribe(action);
    }

    public void OnEvent(FuncI1<TEvent, Task> action) {
        ValidateReaction();
        reaction = dispatcher.Subscribe(action);
    }

    private void _OnMatch(Matcher matcher) {
        Matcher = matcher;

        allTypes = matcher.AllTypes.Select(_ => _.Type).ToHashSet();
        notTypes = matcher.NotTypes.Select(_ => _.Type).ToHashSet();
        anyTypes = matcher.AnyTypes.Select(_ => _.Type).ToHashSet();
    }

    public void OnMatch(Matcher matcher, ActionI2<TEvent, Entity> action) {
        _OnMatch(matcher);
        if (executor == null) {
            OnEvent((in TEvent e) => {
                var query = World.Query(matcher);
                query.Execute(in e, action);
            }); 
        }
        else {
            OnEvent((in TEvent e) => {
                var query = World.Query(matcher);
                query.Execute(in e, executor, action);
            }); 
        }
    }
    
    public void OnMatch(Matcher matcher, Action<TEvent, Entity> action) {
        var delegates = (ActionI2<TEvent, Entity>)((in TEvent e, in Entity entity) => action(e, entity));
        OnMatch(matcher, delegates);
    }
    
    public void ForEach<T>(ActionR1<T> action) where T : IComponent {
        OnEvent((in TEvent _) => {
            var collection = World.GetComponents<T>();
            collection.ForEach(action);
        });
    }
    
    public void ForEach<T>(ActionI1<T> action) where T : IComponent {
        OnEvent((in TEvent _) => {
            var collection = World.GetComponents<T>();
            collection.ForEach((ref T component) => action(in component));
        });
    }
    
    public void ForEach<T>(Action<T> action) where T : IComponent {
        OnEvent((in TEvent _) => {
            var collection = World.GetComponents<T>();
            collection.ForEach((ref T component) => action(component));
        });
    }
    
    public void ForEach<T>(ActionI1R1<Entity, T> action) where T : IComponent {
        OnEvent((in TEvent _) => {
            var collection = World.GetComponents<T>();
            collection.ForEach(action);
        });
    }
    
    public void ForEach<T>(ActionI2<Entity, T> action) where T : IComponent {
        OnEvent((in TEvent _) => {
            var collection = World.GetComponents<T>();
            collection.ForEach((in Entity entity, ref T component) => action(in entity, in component));
        });
    }

    public void ForEach<T>(Action<Entity, T> action) where T : IComponent {
        OnEvent((in TEvent _) => {
            var collection = World.GetComponents<T>();
            collection.ForEach((in Entity entity, ref T component) => action(entity, component));
        });
    }

    public void ForEach<T>(ActionI2<TEvent, T> action) where T : IComponent {
        OnEvent(e => {
            var collection = World.GetComponents<T>();
            collection.ForEach((ref T component) => action(in e, in component));
        });
    }
    
    public void ForEach<T>(ActionI1R1<TEvent, T> action) where T : IComponent {
        OnEvent(e => {
            var collection = World.GetComponents<T>();
            collection.ForEach(in e, action);
        });
    }
    
    public void ForEach<T>(Action<TEvent, T> action) where T : IComponent {
        OnEvent(e => {
            var collection = World.GetComponents<T>();
            collection.ForEach((ref T component) => action(e, component));
        });
    }
    
    public void ForEach<T>(ActionI2R1<TEvent, Entity, T> action) where T : IComponent {
        OnEvent(e => {
            var collection = World.GetComponents<T>();
            collection.ForEach(in e, action);
        });
    }
    
    public void ForEach<T>(ActionI3<TEvent, Entity, T> action) where T : IComponent {
        OnEvent(e => {
            var collection = World.GetComponents<T>();
            collection.ForEach((in Entity entity, ref T component) => action(in e, in entity, in component));
        });
    }

    public void ForEach<T>(Action<TEvent, Entity, T> action) where T : IComponent {
        OnEvent(e => {
            var collection = World.GetComponents<T>();
            collection.ForEach((in Entity entity, ref T component) => action(e, entity, component));
        });
    }
}