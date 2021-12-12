using System;
using System.Threading.Tasks;

namespace Coorth {
    public abstract partial class SystemBase {
        
        protected void OnComponentAdd<T>(EventActionR<Entity, T> action) where T : IComponent {
            Subscribe<EventComponentAdd<T>>(_ => action(_.Entity, ref _.Get()));
        }
        
        protected void OnComponentAdd<T>(EventActionR<T> action) where T : IComponent {
            Subscribe<EventComponentAdd<T>>(_ => action(ref _.Get()));
        }

        protected void OnComponentRemove<T>(EventActionR<Entity, T> action) where T : IComponent {
            Subscribe<EventComponentRemove<T>>(_ => action(_.Entity, ref _.Get()));
        }
        
        protected void OnComponentRemove<T>(EventActionR<T> action) where T : IComponent {
            Subscribe<EventComponentRemove<T>>(_ => action(ref _.Get()));
        }
        
        protected void OnComponentModify<T>(EventActionR<Entity, T> action) where T : IComponent {
            Subscribe<EventComponentModify<T>>(_ => action(_.Entity, ref _.Get()));
        }
        
        protected void OnComponentModify<T>(EventActionR<T> action) where T : IComponent {
            Subscribe<EventComponentModify<T>>(_ => action(ref _.Get()));
        }
        
        protected void OnComponentEnable<T>(EventActionR<Entity, bool, T> action) where T : IComponent {
            Subscribe<EventComponentEnable<T>>(_ => action(_.Entity, _.IsEnable, ref _.Get()));
        }
        
        protected void OnComponentEnable<T>(EventActionR<Entity, T> action, bool isEnable) where T : IComponent {
            Subscribe<EventComponentEnable<T>>(_ => {
                if (_.IsEnable == isEnable) {
                    action(_.Entity, ref _.Get());
                }
            });
        }
        
        /// <summary>
        /// 订阅事件TEvent
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <param name="action">响应函数</param>
        public void Subscribe<TEvent>(Action<TEvent> action)  where TEvent: IEvent {
            Subscribe<TEvent>().OnEvent(action);
        }

        /// <summary>
        /// 订阅事件TEvent
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <param name="action">响应函数</param>
        public void Subscribe<TEvent>(EventAction<TEvent> action)  where TEvent: IEvent {
            Subscribe<TEvent>().OnEvent(action);
        }

        /// <summary>
        /// 订阅事件TEvent
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <param name="action">响应函数</param>
        public void Subscribe<TEvent>(Func<TEvent, ValueTask> action)  where TEvent: IEvent {
            Subscribe<TEvent>().OnEvent(action);
        }

        /// <summary>
        /// 订阅事件TEvent，并在收到事件后遍历所有TComponent组件
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <typeparam name="TComponent">组件</typeparam>
        /// <param name="action">响应函数</param>
        protected  void ForEach<TEvent, TComponent>(Action<TEvent, TComponent> action) where TEvent: IEvent where TComponent: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        /// <summary>
        /// 订阅事件TEvent，并在收到事件后遍历所有带有TComponent组件的实体
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <typeparam name="TComponent">组件</typeparam>
        /// <param name="action">响应函数</param>
        protected void ForEach<TEvent, TComponent>(Action<TEvent, Entity, TComponent> action) where TEvent: IEvent where TComponent : IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        /// <summary>
        /// 订阅事件TEvent，并在收到事件后遍历所有带有TComponent1组件和TComponent2的实体
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <typeparam name="TComponent1">组件1</typeparam>
        /// <typeparam name="TComponent2">组件2</typeparam>
        /// <param name="action">响应函数</param>
        protected void ForEach<TEvent, TComponent1, TComponent2>(Action<TEvent, TComponent1, TComponent2> action) where TEvent: IEvent where TComponent1 : IComponent where TComponent2: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        /// <summary>
        /// 订阅事件TEvent，并在收到事件后遍历所有带有TComponent1组件和TComponent2的实体
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <typeparam name="TComponent1">组件1</typeparam>
        /// <typeparam name="TComponent2">组件2</typeparam>
        /// <param name="action">响应函数</param>
        protected void ForEach<TEvent, TComponent1, TComponent2>(Action<TEvent, Entity, TComponent1, TComponent2> action) where TEvent: IEvent where TComponent1 : IComponent where TComponent2: IComponent {
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
        protected void ForEach<TEvent, TComponent1, TComponent2, TComponent3>(Action<TEvent, TComponent1, TComponent2, TComponent3> action) where TEvent: IEvent where TComponent1 : IComponent where TComponent2: IComponent where TComponent3: IComponent {
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
        protected void ForEach<TEvent, TComponent1, TComponent2, TComponent3>(Action<TEvent, Entity, TComponent1, TComponent2, TComponent3> action) where TEvent: IEvent where TComponent1 : class, IComponent where TComponent2: class, IComponent where TComponent3: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        protected void OnMatch(Action<Entity> onAdd, Action<Entity> onRemove) {
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

        protected void MatchSystem<T>(Action<T> onAdd, Action<T> onRemove) where T : SystemBase {
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
        
        protected void MatchComponent<T>(Action<T> onAdd, Action<T> onRemove) where T : IComponent {
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

        protected void MatchComponents<T1, T2>(Action<T1, T2> action) where T1: IComponent where T2: IComponent {
            var components = Sandbox.GetComponents<T1, T2>();
            foreach (var (_, component1, component2) in components) {
                action(component1, component2);
            }
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

        /// <summary>
        /// 关联两个系统，当TSystem被添加时候自动添加TChild系统为TSystem系统的子系统，当TSystem被移除时候，TChild也会被动态移除
        /// </summary>
        /// <typeparam name="TSystem">目标系统</typeparam>
        /// <typeparam name="TChild">子系统</typeparam>
        public void Associate<TSystem, TChild>() where  TSystem : SystemBase  where TChild : SystemBase, new() {
            MatchSystem<TSystem>(s=> s.AddSystem<TChild>(), null);
        }

        public delegate bool RemoveCondition<TEvent, TComponent>(in TEvent e, in Entity entity, in TComponent component);

    }
}