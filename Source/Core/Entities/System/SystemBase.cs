using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Coorth {
    public abstract partial class SystemBase : IDisposable {

        #region Fields

        public Sandbox Sandbox { get; private set; }
        
        protected SystemBinding Binding { get; private set; }

        public bool IsDisposed { get; private set; }

        protected EventDispatcher Dispatcher => Sandbox.Dispatcher;
        
        private readonly List<ISystemReaction> reactions = new List<ISystemReaction>();

        public IReadOnlyList<ISystemReaction> Reactions => reactions;
        
        protected T Singleton<T>() where T : IComponent, new() => Sandbox.Singleton<T>();
        
        protected T Singleton<T>(Func<Entity, T> provider) where T : IComponent, new() => Sandbox.Singleton().Offer<T>(provider);

        public void Setup(Sandbox sandbox, SystemBinding binding) {
            this.Sandbox = sandbox;
            this.Binding = binding;
        }
        
        public void Dispose() {
            if (IsDisposed) {
                return;
            }
            OnDispose(true);
        }
        
        protected virtual void OnDispose(bool dispose) {
            IsDisposed = true;
            ClearSystems();
            Parent?.RemoveSystem(Key);
        }

        #endregion

        #region Hierarchy

        public SystemBase AddSystem(Type type, bool reflection = false) {
            var binding = Sandbox.GetSystemBinding(type, reflection);
            if (binding == null) {
                throw new NotBindException(type);
            }
            return binding.AddSystem(this);
        }
        
        public T AddSystem<T>() where T : SystemBase, new() {
            var system = Activator.CreateInstance<T>();
            return AddSystem(system);
        }
        
        public T AddSystem<T>(T system) where T : SystemBase {
            var binding = Sandbox.BindSystem<T>();
            var key = typeof(T);
            system.Setup(Sandbox, binding);
            AddChild(key, system);
            Sandbox.OnSystemAdd<T>(key, system);
            system.SetActive(true);
            return system;
        }
        
        /// <summary>
        /// 移除子系统
        /// </summary>
        /// <param name="type">子系统类型</param>
        /// <returns></returns>
        public bool RemoveSystem(Type type) {
            var binding = Sandbox.GetSystemBinding(type, false);
            return binding != null && binding.RemoveSystem(this);
        }
        
        /// <summary>
        /// 移除子系统
        /// </summary>
        /// <typeparam name="T">子系统类</typeparam>
        /// <returns>是否成功移除</returns>
        public bool RemoveSystem<T>() where T : SystemBase {
            var key = typeof(T);
            if (Children.TryGetValue(key, out var child)) {
                child.SetActive(false);
            }
            var result = Sandbox.OnSystemRemove<T>(key);
            return result && RemoveChild(key);
        }
        
        /// <summary>
        /// 清理所有子系统
        /// </summary>
        public void ClearSystems() {
            if (children == null) {
                return;
            }
            var keys = children.Keys.ToList();
            foreach (var key in keys) {
                RemoveSystem(key);
            }
            children.Clear();
        }
        
        /// <summary>
        /// 是否存在子系统
        /// </summary>
        /// <typeparam name="T">子系统类</typeparam>
        /// <returns>结果true:存在/false:不存在</returns>
        public bool HasSystem<T>() where T : SystemBase => HasSystem(typeof(T));

        /// <summary>
        /// 是否存在子系统
        /// </summary>
        /// <param name="type">子系统类型</param>
        /// <returns>结果true:存在/false:不存在</returns>
        public bool HasSystem(Type type) => Children.ContainsKey(type);
        
        /// <summary>
        /// 获取子系统，如果不存在会返回null
        /// </summary>
        /// <typeparam name="T">子系统类</typeparam>
        /// <returns>子系统实例</returns>
        public T GetSystem<T>() where T : SystemBase => Children.TryGetValue(typeof(T), out var system) ? (T)system : default;

        /// <summary>
        /// 获取所有子系统
        /// </summary>
        /// <returns>子系统数组</returns>
        public SystemBase[] GetSystems() => Children.Values.ToArray();

        /// <summary>
        /// 添加子系统
        /// </summary>
        /// <typeparam name="T">子系统类</typeparam>
        /// <returns>自身实例</returns>
        public SystemBase WithSystem<T>() where T : SystemBase, new() {
            AddSystem<T>();
            return this;
        }

        /// <summary>
        /// 如果存在就获取子系统，不存在就添加子系统
        /// </summary>
        /// <typeparam name="T">子系统类</typeparam>
        /// <returns>子系统实例</returns>
        public T OfferSystem<T>() where T : SystemBase, new() {
            return HasSystem<T>() ? GetSystem<T>() : AddSystem<T>();
        }

        #endregion

        #region Subscribe

        internal SystemReaction<T> CreateReaction<T>() where T : IEvent {
            var reaction = new SystemReaction<T>(this);
            reactions.Add(reaction);
            Dispatcher.Subscribe(reaction);
            Collector.Add(reaction);
            return reaction;
        }

        internal void RemoveReaction<T>(SystemReaction<T> reaction) where T : IEvent {
            reactions.Remove(reaction);
        }

        protected SystemSubscription<TEvent> Subscribe<TEvent>() where TEvent : IEvent {
            return new SystemSubscription<TEvent>(this);
        }

        #endregion
    }
}