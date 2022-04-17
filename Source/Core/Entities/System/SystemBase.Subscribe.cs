using System;
using System.Threading.Tasks;

namespace Coorth {
    public abstract partial class SystemBase {

        #region Subscribe

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <returns>订阅</returns>
        protected SystemSubscription<TEvent> Subscribe<TEvent>() {
            var subscription = new SystemSubscription<TEvent>(this, Dispatcher);
            subscriptions.Add(subscription);
            Collector.Add(subscription);
            return subscription;
        }
        
        internal void RemoveReaction<T>(SystemSubscription<T> subscription) {
            subscriptions.Remove(subscription);
        }

        /// <summary>
        /// 订阅事件TEvent
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <param name="action">响应函数</param>
        protected void Subscribe<TEvent>(Action<TEvent> action) {
            Subscribe<TEvent>().OnEvent(action);
        }
        
        /// <summary>
        /// 订阅事件TEvent
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <param name="action">响应函数</param>
        protected void Subscribe<TEvent>(EventAction<TEvent> action) {
            Subscribe<TEvent>().OnEvent(action);
        }

        /// <summary>
        /// 订阅事件TEvent
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <param name="action">响应函数</param>
        protected void Subscribe<TEvent>(Func<TEvent, ValueTask> action) {
            Subscribe<TEvent>().OnEvent(action);
        }

        #endregion
        
        
        #region Lifecycle
        
        /// <summary>
        /// 当添加组件时
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentAdd<T>(EventActionR<Entity, T> action) where T : IComponent {
            Subscribe<EventComponentAdd<T>>(_ => action(_.Entity, ref _.Get()));
        }
        
        /// <summary>
        /// 当添加组件时
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentAdd<T>(EventActionR<T> action) where T : IComponent {
            Subscribe<EventComponentAdd<T>>(_ => action(ref _.Get()));
        }

        /// <summary>
        /// 组件初始化
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentSetup<T>(EventActionR<Entity, T> action) where T : IComponent {
            OnComponentAdd(action);
            foreach (var (entity, _) in Sandbox.GetComponents<T>()) {
                action(entity, ref entity.Get<T>());
            }
        }
        
        /// <summary>
        /// 组件初始化
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentSetup<T>(EventActionR<T> action) where T : IComponent {
            OnComponentAdd(action);
            foreach (var (entity, _) in Sandbox.GetComponents<T>()) {
                action(ref entity.Get<T>());
            }
        }
        
        /// <summary>
        /// 组件清理
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentClear<T>(EventActionR<Entity, T> action) where T : IComponent {
             OnComponentRemove(action);
             Managed.Add(new DisposeAction(() => {
                 foreach (var (entity, _) in Sandbox.GetComponents<T>()) {
                    action(entity, ref entity.Get<T>());
                 }
             }));
        }

        /// <summary>
        /// 组件清理
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentClear<T>(EventActionR<T> action) where T : IComponent {
            OnComponentRemove(action);
            Managed.Add(new DisposeAction(() => {
                foreach (var (entity, _) in Sandbox.GetComponents<T>()) {
                    action(ref entity.Get<T>());
                }
            }));
        }
        
        /// <summary>
        /// 当组件移除时
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentRemove<T>(EventActionR<Entity, T> action) where T : IComponent {
            Subscribe<EventComponentRemove<T>>(_ => action(_.Entity, ref _.Get()));
        }
        
        /// <summary>
        /// 当组件移除时
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentRemove<T>(EventActionR<T> action) where T : IComponent {
            Subscribe<EventComponentRemove<T>>(_ => action(ref _.Get()));
        }

        /// <summary>
        /// 当组件变更时
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentModify<T>(EventActionR<Entity, T> action) where T : IComponent {
            Subscribe<EventComponentModify<T>>(_ => action(_.Entity, ref _.Get()));
        }
        
        /// <summary>
        /// 当组件变更时
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentModify<T>(EventActionR<T> action) where T : IComponent {
            Subscribe<EventComponentModify<T>>(_ => action(ref _.Get()));
        }
        
        /// <summary>
        /// 当组件Enable变化时
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentEnable<T>(EventActionR<Entity, bool, T> action) where T : IComponent {
            Subscribe<EventComponentEnable<T>>(_ => action(_.Entity, _.IsEnable, ref _.Get()));
        }
        
        /// <summary>
        /// 当组件Enable变化时
        /// </summary>
        /// <param name="action">响应函数</param>
        /// <param name="isEnable">Enable</param>
        /// <typeparam name="T">组件</typeparam>
        protected void OnComponentEnable<T>(EventActionR<Entity, T> action, bool isEnable) where T : IComponent {
            Subscribe<EventComponentEnable<T>>(_ => {
                if (_.IsEnable == isEnable) {
                    action(_.Entity, ref _.Get());
                }
            });
        }
        
        #endregion

        
        #region ForEach

        /// <summary>
        /// 订阅事件TEvent，并在收到事件后遍历所有TComponent组件
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <typeparam name="TComponent">组件</typeparam>
        /// <param name="action">响应函数</param>
        protected void ForEach<TEvent, TComponent>(Action<TEvent, TComponent> action) where TComponent: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        /// <summary>
        /// 订阅事件TEvent，并在收到事件后遍历所有带有TComponent组件的实体
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <typeparam name="TComponent">组件</typeparam>
        /// <param name="action">响应函数</param>
        protected void ForEach<TEvent, TComponent>(Action<TEvent, Entity, TComponent> action) where TComponent : IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        /// <summary>
        /// 订阅事件TEvent，并在收到事件后遍历所有带有TComponent1组件和TComponent2的实体
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <typeparam name="TComponent1">组件1</typeparam>
        /// <typeparam name="TComponent2">组件2</typeparam>
        /// <param name="action">响应函数</param>
        protected void ForEach<TEvent, TComponent1, TComponent2>(Action<TEvent, TComponent1, TComponent2> action) where TComponent1 : IComponent where TComponent2: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        /// <summary>
        /// 订阅事件TEvent，并在收到事件后遍历所有带有TComponent1组件和TComponent2的实体
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <typeparam name="TComponent1">组件1</typeparam>
        /// <typeparam name="TComponent2">组件2</typeparam>
        /// <param name="action">响应函数</param>
        protected void ForEach<TEvent, TComponent1, TComponent2>(Action<TEvent, Entity, TComponent1, TComponent2> action) where TComponent1 : IComponent where TComponent2: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }

        /// <summary>
        /// 订阅事件TEvent，并在收到事件后遍历所有带有TComponent1组件，TComponent2组件和TComponent3组件的实体
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <typeparam name="TComponent1">组件1</typeparam>
        /// <typeparam name="TComponent2">组件2</typeparam>
        /// <typeparam name="TComponent3">组件3</typeparam>
        /// <param name="action">响应函数</param>
        protected void ForEach<TEvent, TComponent1, TComponent2, TComponent3>(Action<TEvent, TComponent1, TComponent2, TComponent3> action) where TComponent1 : IComponent where TComponent2: IComponent where TComponent3: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        /// <summary>
        /// 订阅事件TEvent，并在收到事件后遍历所有带有TComponent1组件，TComponent2组件和TComponent3组件的实体
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <typeparam name="TComponent1">组件1</typeparam>
        /// <typeparam name="TComponent2">组件2</typeparam>
        /// <typeparam name="TComponent3">组件3</typeparam>
        /// <param name="action">响应函数</param>
        protected void ForEach<TEvent, TComponent1, TComponent2, TComponent3>(Action<TEvent, Entity, TComponent1, TComponent2, TComponent3> action) where TComponent1 : class, IComponent where TComponent2: class, IComponent where TComponent3: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }

        #endregion

        
        #region System

        private void MatchSystem<T>(Action<T>? onAdd, Action<T>? onRemove) where T : SystemBase {
            if (onAdd != null) {
                var system = Sandbox.GetSystem<T>();
                if (system != null) {
                    onAdd(system);
                } 
                Subscribe<EventSystemAdd<T>>(_ => onAdd((T)_.System));
            }

            if (onRemove != null) {
                Subscribe<EventSystemRemove<T>>(_ => onRemove((T)_.System));
            }
        }
        
        /// <summary>
        /// 关联两个系统，当TSystem被添加时候自动添加TChild系统为TSystem系统的父节点的子系统，当TSystem被移除时候，TChild也会被动态移除
        /// </summary>
        /// <typeparam name="TSystem">目标系统</typeparam>
        /// <typeparam name="TChild">子系统</typeparam>
        protected void Associate<TSystem, TChild>() where  TSystem : SystemBase  where TChild : SystemBase, new() {
            var system = Sandbox.GetSystem<TSystem>();
            if (system != null) {
                system.Parent?.AddSystem<TChild>();
            }
            Subscribe<EventSystemAdd<TChild>>(s => s.System.Parent?.AddSystem<TChild>());
            Subscribe<EventSystemRemove<TChild>>(s => s.System.Parent?.RemoveSystem<TChild>());
        }
        
        protected void Associate<TSystem1, TSystem2, TChild>() where TSystem1 : SystemBase where TSystem2 : SystemBase where TChild : SystemBase, new() {
            var system1 = Sandbox.GetSystem<TSystem1>();
            var system2 = Sandbox.GetSystem<TSystem2>();

            if (system1 != null && system2 != null) {
                system2.Parent?.AddSystem<TChild>();
            }
            Subscribe<EventSystemAdd<TSystem1>>(_ => {
                var system = Sandbox.GetSystem<TSystem2>();
                if (system !=null) {
                    system.Parent?.AddSystem<TChild>();
                }
            });
            Subscribe<EventSystemAdd<TSystem2>>(e => {
                var system = e.System;
                if (Sandbox.HasSystem<TSystem1>()) {
                    system.Parent?.AddSystem<TChild>();
                }
            });
            Subscribe<EventSystemRemove<TSystem1>>(e => e.Sandbox.RemoveSystem<TChild>());
            Subscribe<EventSystemRemove<TSystem2>>(e => e.Sandbox.RemoveSystem<TChild>());
        }


        #endregion

        
        #region Other

        protected void MatchComponents<T1, T2>(Action<T1, T2> action) where T1: IComponent where T2: IComponent {
            var components = Sandbox.GetComponents<T1, T2>();
            components.ForEach(action);
            Subscribe<EventComponentAdd<T1>>(e => {
                if (e.Entity.TryGet(out T2 component)) {
                    action(e.Component, component);
                }
            });
            Subscribe<EventComponentAdd<T2>>(e => {
                if (e.Entity.TryGet(out T1 component)) {
                    action(component, e.Component);
                }
            });
        }
        
        protected void MatchComponents<T1, T2>(EventActionR2<T1, T2> action) where T1: IComponent where T2: IComponent {
            var components = Sandbox.GetComponents<T1, T2>();
            components.ForEach(action);
            Subscribe<EventComponentAdd<T1>>(e => {
                if (e.Entity.Has<T2>()) {
                    action(ref e.Get(), ref e.Entity.Get<T2>());
                }
            });
            Subscribe<EventComponentAdd<T2>>(e => {
                if (e.Entity.Has<T1>()) {
                    action(ref e.Entity.Get<T1>(), ref e.Get());
                }
            });
        }
        
        protected void MatchComponent<T>(Action<T>? onAdd, Action<T>? onRemove) where T : IComponent {
            if (onAdd != null) {
                var components = Sandbox.GetComponents<T>();
                foreach (var (_, component) in components) {
                    onAdd(component);
                }
                Subscribe<EventComponentAdd<T>>(_ => onAdd(_.Component));
            }

            if (onRemove != null) {
                Subscribe<EventComponentRemove<T>>(_ => onRemove(_.Component));
            }
        }
        
        protected void OnMatch(Action<Entity>? onAdd, Action<Entity>? onRemove) {
            if (onAdd != null) {
                var entities = Sandbox.GetEntities();
                foreach (var entity in entities) {
                    onAdd(entity);
                }
                Subscribe<EventEntityAdd>(_ => onAdd(_.Entity));
            }

            if (onRemove != null) {
                Subscribe<EventEntityRemove>(_ => onRemove(_.Entity));
            }
        }
        
        protected void OnMatch(Action<Entity, bool> action) {
            var entities = Sandbox.GetEntities();
            foreach (var entity in entities) {
                action(entity, true);
            }
            Subscribe<EventEntityAdd>(e => action(e.Entity, true));
            Subscribe<EventEntityRemove>(e => action(e.Entity, true));
        }
        

        #endregion

    }
}