using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coorth {
    public abstract partial class SystemBase {

        #region Fields
        
        public EventId ProcessId { get; } = EventId.New();

        public Sandbox Sandbox { get; private set; }
        
        public Type Key { get; private set; }
        
        protected Disposables Managed;
        
        protected EventDispatcher Dispatcher => Sandbox.Dispatcher;

        public SystemBase Parent { get; private set; }

        private readonly Dictionary<Type, SystemBase> children = new Dictionary<Type, SystemBase>();

        public IDictionary<Type, SystemBase> Children => children;
        
        private readonly List<ISystemReaction> reactions = new List<ISystemReaction>();

        public IReadOnlyList<ISystemReaction> Reactions => reactions;
        
        #endregion

        #region Hierarchy

        internal void OnChildAdd<T>(Type key, SystemBase child) {
            child.Parent = this;
            children.Add(key, child);
        }
        
        internal void OnChildRemove(SystemBase child) {
            child.Parent = null;
            children.Remove(child.Key);
        }
        
        public T AddSystem<T>(T system) where T : SystemBase {
            Sandbox.OnSystemAdd<T>(typeof(T), system, this);
            return system;
        }
        
        public T AddSystem<T>() where T : SystemBase, new() {
            var system = Activator.CreateInstance<T>();
            return AddSystem(system);
        }

        public T OfferSystem<T>() where T : SystemBase, new() {
            return HasSystem<T>() ? GetSystem<T>() : AddSystem<T>();
        }

        public bool HasSystem<T>() where T : SystemBase => HasSystem(typeof(T));

        public bool HasSystem(Type type) => children.ContainsKey(type);
        
        public T GetSystem<T>() where T : SystemBase => children.TryGetValue(typeof(T), out var system) ? (T)system : default;
        
        public SystemBase[] GetSystems() => children.Values.ToArray();

        public bool RemoveSystem<T>() where T : SystemBase {
            var childKey = typeof(T);
            return Sandbox.OnSystemRemove<T>(childKey);
        }

        #endregion

        #region Lifecycle

        protected T Singleton<T>() where T : IComponent, new() => Sandbox.Singleton<T>();
        
        protected T Singleton<T>(Func<EntityId, Entity, T> provider) where T : IComponent, new() => Sandbox.Singleton().Offer<T>(provider);
        
        public void SystemAdd(Sandbox sandbox, Type key, SystemBase parent) {
            this.Sandbox = sandbox;
            this.Key = key;
            this.Parent = parent;
            this.OnAdd();
        }

        public void SystemActive() {
            
        }

        public void SystemDeActive() {
            
        }

        public void SystemRemove() {
            this.OnRemove();
            reactions.Clear();
            this.Managed.Clear();
            this.Sandbox = null;
        }

        protected virtual void OnAdd() { }
        
        protected virtual void OnActive() { }

        protected virtual void OnRemove() { }
        
        protected virtual void OnDeActive() { }
        
        #endregion

        #region Subscribe

        internal SystemReaction<T> CreateReaction<T>() where T : IEvent {
            var reaction = new SystemReaction<T>(this);
            reactions.Add(reaction);
            Dispatcher.Subscribe(reaction);
            return reaction;
        }

        internal void RemoveReaction<T>(SystemReaction<T> reaction) where T : IEvent {
            reactions.Remove(reaction);
        }
        
        protected SystemSubscription<T> Subscribe<T>() where T : IEvent {
            return new SystemSubscription<T>(this);
        }

        protected void Subscribe<T>(Action<T> action)  where T: IEvent {
            Subscribe<T>().OnEvent(action);
        }
        
        protected void Subscribe<T>(Func<T, ValueTask> action)  where T: IEvent {
            Subscribe<T>().OnEvent(action);
        }
 
        protected void ForEach<TEvent, TComponent>(Action<TEvent, TComponent> action) where TEvent: IEvent where TComponent: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        protected void ForEach<TEvent, TComponent>(Action<TEvent, Entity, TComponent> action) where TEvent: IEvent where TComponent : IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        protected void ForEach<TEvent, TComponent1, TComponent2>(Action<TEvent, TComponent1, TComponent2> action) where TEvent: IEvent where TComponent1 : IComponent where TComponent2: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        protected void ForEach<TEvent, TComponent1, TComponent2>(Action<TEvent, Entity, TComponent1, TComponent2> action) where TEvent: IEvent where TComponent1 : IComponent where TComponent2: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }

        protected void ForEach<TEvent, TComponent1, TComponent2, TComponent3>(Action<TEvent, TComponent1, TComponent2, TComponent3> action) where TEvent: IEvent where TComponent1 : IComponent where TComponent2: IComponent where TComponent3: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }
        
        protected void ForEach<TEvent, TComponent1, TComponent2, TComponent3>(Action<TEvent, Entity, TComponent1, TComponent2, TComponent3> action) where TEvent: IEvent where TComponent1 : class, IComponent where TComponent2: class, IComponent where TComponent3: IComponent {
            Subscribe<TEvent>().ForEach(action);
        }

        #endregion
    }
    
    public sealed class SystemRoot : SystemBase {
        
    }
    
    public class ActionsSystem : SystemBase {
        
        public void Add<T>(Action<T> action) {
                    
        }
                
        public void Remove<T>(Action<T> action) {
                    
        }
    }
    

}