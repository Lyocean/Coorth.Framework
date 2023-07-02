using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Coorth.Tasks;


namespace Coorth.Framework;

[AttributeUsage(AttributeTargets.Class)]
public class SystemAttribute : Attribute {
}

public abstract partial class SystemBase : ServiceNode<SystemBase> {

    #region Common

    private World? world;
    public World World => world ?? throw new NullReferenceException();

    public new SystemRoot Root => root as SystemRoot ?? throw new NullReferenceException();
    
    protected override Dispatcher Dispatcher => World.Dispatcher;

    protected override IServiceLocator Services => World.Services;

    private readonly List<ISystemSubscription> subscriptions = new();

    public IReadOnlyList<ISystemSubscription> Subscriptions => subscriptions;

    protected T Singleton<T>() where T : IComponent => World.Singleton<T>();

    protected T Singleton<T>(Func<Entity, T> provider) where T : IComponent, new() => World.Singleton().Offer(provider);

    public void Setup(World value) => world = value;
    
    protected ComponentGroup<T> BindComponent<T>() where T : IComponent => World.BindComponent<T>();

    protected ComponentGroup<T> BindComponent<T>(IComponentFactory<T> factory) where T : IComponent {
        var binding = World.BindComponent<T>();
        binding.SetFactory(factory);
        return binding;
    }

    protected T GetService<T>() where T : class => World.GetService<T>();
    
    #endregion

    
    #region Hierarchy
    
    protected sealed override void OnChildAdd(SystemBase child) {
        child.Setup(World);
        World.OnSystemAdd(child.Key, child);
    }
    
    protected sealed override void OnChildRemove(SystemBase child) {
        World.OnSystemRemove(child.Key);
    }

    #endregion
    
    
    #region Subscribe

    /// <summary>
    /// 订阅事件
    /// </summary>
    /// <typeparam name="TEvent">事件</typeparam>
    /// <returns>订阅</returns>
    protected SystemSubscription<TEvent> Subscribe<TEvent>() where TEvent : notnull {
        var subscription = new SystemSubscription<TEvent>(this, Dispatcher, null);
        subscriptions.Add(subscription);
        Collector.Add(subscription);
        return subscription;
    }

    protected SystemSubscription<TEvent> Subscribe<TEvent>(TaskExecutor executor) where TEvent : notnull {
        var subscription = new SystemSubscription<TEvent>(this, Dispatcher, executor);
        subscriptions.Add(subscription);
        Collector.Add(subscription);
        return subscription;
    }

    public void RemoveReaction<T>(SystemSubscription<T> subscription) where T : notnull {
        subscriptions.Remove(subscription);
    }

    #endregion

    
    #region OnReceive

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnReceive<T>(Action<T> action) where T : notnull {
        var reaction = World.Router.Subscribe<T>((_, msg) => action(msg));
        Collector.Add(reaction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnReceive<T>(Action<MessageContext, T> action) where T : notnull {
        var reaction = World.Router.Subscribe(action);
        Collector.Add(reaction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnReceive<T>(Func<MessageContext, T, bool> action) where T : notnull {
        var reaction = World.Router.Subscribe<T>((context, msg) => action(context, msg));
        Collector.Add(reaction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnReceive<T>(Func<MessageContext, T, Result> action) where T : notnull {
        var reaction = World.Router.Subscribe<T>((context, msg) => action(context, msg));
        Collector.Add(reaction);
    }

    #endregion

    
    #region Lifecycle

    /// <summary>
    /// 当添加组件时
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentAdd<T>(ActionI1R1<Entity, T> action) where T : IComponent {
        Subscribe<ComponentAddEvent<T>>().OnEvent(_ => action(_.Entity, ref _.Get()));
    }

    /// <summary>
    /// 当添加组件时
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentAdd<T>(ActionR1<T> action) where T : IComponent {
        Subscribe<ComponentAddEvent<T>>().OnEvent(_ => action(ref _.Get()));
    }

    /// <summary>
    /// 组件初始化
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentSetup<T>(ActionI1R1<Entity, T> action) where T : IComponent {
        Subscribe<ComponentAddEvent<T>>().OnEvent(_ => action(_.Entity, ref _.Get()));
        World.GetComponents<T>().ForEach(action);
    }

    /// <summary>
    /// 组件初始化
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentSetup<T>(ActionR1<T> action) where T : IComponent {
        Subscribe<ComponentAddEvent<T>>().OnEvent(_ => action(ref _.Get()));
        World.GetComponents<T>().ForEach(action);
    }

    /// <summary>
    /// 组件清理
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentClear<T>(ActionI1R1<Entity, T> action) where T : IComponent {
        Subscribe<ComponentRemoveEvent<T>>().OnEvent(_ => action(_.Entity, ref _.Get()));
        Managed.Add(new DisposeAction(() => {
            World.GetComponents<T>().ForEach(action);
        }));
    }

    /// <summary>
    /// 组件清理
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentClear<T>(ActionR1<T> action) where T : IComponent {
        Subscribe<ComponentRemoveEvent<T>>().OnEvent(_ => action(ref _.Get()));
        Managed.Add(new DisposeAction(() => {
            World.GetComponents<T>().ForEach(action);
        }));
    }

    /// <summary>
    /// 当组件移除时
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentRemove<T>(ActionI1R1<Entity, T> action) where T : IComponent {
        Subscribe<ComponentRemoveEvent<T>>().OnEvent(_ => action(_.Entity, ref _.Get()));
    }

    /// <summary>
    /// 当组件移除时
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentRemove<T>(ActionR1<T> action) where T : IComponent {
        Subscribe<ComponentRemoveEvent<T>>().OnEvent(_ => action(ref _.Get()));
    }

    /// <summary>
    /// 当组件变更时
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentModify<T>(ActionI1R1<Entity, T> action) where T : IComponent {
        Subscribe<ComponentModifyEvent<T>>().OnEvent(_ => action(_.Entity, ref _.Get()));
    }

    /// <summary>
    /// 当组件变更时
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentModify<T>(ActionR1<T> action) where T : IComponent {
        Subscribe<ComponentModifyEvent<T>>().OnEvent(_ => action(ref _.Get()));
    }

    /// <summary>
    /// 当组件Enable变化时
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentEnable<T>(ActionI2R1<Entity, bool, T> action) where T : IComponent {
        Subscribe<ComponentEnableEvent<T>>().OnEvent(_ => action(_.Entity, _.IsEnable, ref _.Get()));
    }

    /// <summary>
    /// 当组件Enable变化时
    /// </summary>
    /// <param name="action">响应函数</param>
    /// <param name="isEnable">Enable</param>
    /// <typeparam name="T">组件</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void OnComponentEnable<T>(ActionI1R1<Entity, T> action, bool isEnable) where T : IComponent {
        Subscribe<ComponentEnableEvent<T>>().OnEvent(_ => {
            if (_.IsEnable == isEnable) {
                action(_.Entity, ref _.Get());
            }
        });
    }

    #endregion

    
    #region Other

    protected void MatchComponents<T1, T2>(ActionR2<T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var components = World.GetComponents<T1>();
        components.ForEach((in Entity entity, ref T1 component1) => {
            if (entity.Has<T2>()) {
                ref var component2 = ref entity.Get<T2>();
                action(ref component1, ref component2);
            }
        });
        Subscribe<ComponentAddEvent<T1>>().OnEvent(e => {
            if (e.Entity.Has<T2>()) {
                action(ref e.Get(), ref e.Entity.Get<T2>());
            }
        });
        Subscribe<ComponentAddEvent<T2>>().OnEvent(e => {
            if (e.Entity.Has<T1>()) {
                action(ref e.Entity.Get<T1>(), ref e.Get());
            }
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void MatchComponents<T1, T2>(ActionI2<T1, T2> action) where T1 : IComponent where T2 : IComponent {
        MatchComponents((ref T1 v1, ref T2 v2) => action(in v1, in v2));
    }

    #endregion


    #region Obstacle
    
    // protected void MatchComponent<T>(Action<T>? onAdd, Action<T>? onRemove) where T : IComponent {
    //     if (onAdd != null) {
    //         var components = World.GetComponents<T>();
    //         foreach (var (_, component) in components) {
    //             onAdd(component);
    //         }
    //         Subscribe<ComponentAddEvent<T>>().OnEvent(_ => onAdd(_.Component));
    //     }
    //
    //     if (onRemove != null) {
    //         Subscribe<ComponentRemoveEvent<T>>().OnEvent(_ => onRemove(_.Component));
    //     }
    // }
    //
    // protected void OnMatch(Action<Entity>? onAdd, Action<Entity>? onRemove) {
    //     if (onAdd != null) {
    //         var entities = World.GetEntities();
    //         foreach (var entity in entities) {
    //             onAdd(entity);
    //         }
    //
    //         Subscribe<EntityCreateEvent>().OnEvent(_ => onAdd(_.Entity));
    //     }
    //
    //     if (onRemove != null) {
    //         Subscribe<EntityRemoveEvent>().OnEvent(_ => onRemove(_.Entity));
    //     }
    // }
    //
    // protected void OnMatch(Action<Entity, bool> action) {
    //     var entities = World.GetEntities();
    //     foreach (var entity in entities) {
    //         action(entity, true);
    //     }
    //
    //     Subscribe<EntityCreateEvent>().OnEvent(e => action(e.Entity, true));
    //     Subscribe<EntityRemoveEvent>().OnEvent(e => action(e.Entity, true));
    // }

    
    // protected void MatchSystem<T>(Action<T>? onAdd, Action<T>? onRemove) where T : SystemBase {
    //     if (onAdd != null) {
    //         Subscribe<EventSystemAdd<T>>().OnEvent(_ => onAdd((T)_.System));
    //     }
    //
    //     if (onRemove != null) {
    //         Subscribe<EventSystemRemove<T>>().OnEvent(_ => onRemove((T)_.System));
    //     }
    // }

    // /// <summary>
    // /// 关联两个系统，当TSystem被添加时候自动添加TChild系统为TSystem系统的父节点的子系统，当TSystem被移除时候，TChild也会被动态移除
    // /// </summary>
    // /// <typeparam name="TSystem">目标系统</typeparam>
    // /// <typeparam name="TChild">子系统</typeparam>
    // protected void Associate<TSystem, TChild>() where  TSystem : SystemBase  where TChild : SystemBase, new() {
    //     var system = Worlds.GetChild<TChild>();
    //     system.Parent.AddChild<TChild>();
    //     Subscribe<EventSystemAdd<TChild>>().OnEvent(s => s.System.Parent.AddChild<TChild>());
    //     Subscribe<EventSystemRemove<TChild>>().OnEvent(s => s.System.Parent.RemoveChild<TChild>());
    // }
    //
    // protected void Associate<TSystem1, TSystem2, TChild>() where TSystem1 : SystemBase where TSystem2 : SystemBase where TChild : SystemBase, new() {
    //     var system2 = Worlds.GetChild<TSystem2>();
    //     system2.Parent.AddChild<TChild>();
    //     Subscribe<EventSystemAdd<TSystem1>>().OnEvent(_ => {
    //         var system = Worlds.GetChild<TSystem2>();
    //         system.Parent.AddChild<TChild>();
    //     });
    //     Subscribe<EventSystemAdd<TSystem2>>().OnEvent(e => {
    //         var system = e.System;
    //         if (Worlds.HasChild<TSystem1>()) {
    //             system.Parent.AddChild<TChild>();
    //         }
    //     });
    //     Subscribe<EventSystemRemove<TSystem1>>().OnEvent(e => e.Worlds.RemoveChild<TChild>());
    //     Subscribe<EventSystemRemove<TSystem2>>().OnEvent(e => e.Worlds.RemoveChild<TChild>());
    // }

    #endregion
}
