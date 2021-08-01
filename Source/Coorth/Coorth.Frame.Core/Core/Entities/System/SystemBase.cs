using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coorth {
    
    [AttributeUsage(AttributeTargets.Class)]
    public class SystemAttribute : Attribute {
        
    }

    public abstract class SystemBase : Disposable {

        #region Fields

        public Sandbox Sandbox { get; private set; }

        protected World World => Sandbox.World;
        
        public Type Key { get; private set; }
        
        protected Disposables Managed;

        protected SystemBinding Binding { get; private set; }

        protected EventDispatcher Dispatcher => Sandbox.Dispatcher;

        public SystemBase Parent { get; private set; }

        private readonly Dictionary<Type, SystemBase> children = new Dictionary<Type, SystemBase>();

        public IDictionary<Type, SystemBase> Children => children;
        
        private readonly List<ISystemReaction> reactions = new List<ISystemReaction>();

        public IReadOnlyList<ISystemReaction> Reactions => reactions;

        public int ChildCount => children.Count;

        public bool IsActive { get; private set; }

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
            var type = typeof(T);
            var binding = Sandbox.GetSystemBinding(type, false);
            Sandbox.OnSystemAdd<T>(type, system, this, binding);
            return system;
        }

        public SystemBase AddSystem(Type type, bool reflection = false) {
            var binding = Sandbox.GetSystemBinding(type, reflection);
            if (binding == null) {
                throw new NotBindException(type);
            }
            return binding.AddSystem(Sandbox, this);
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
        
        public bool RemoveSystem(Type type) {
            var binding = Sandbox.GetSystemBinding(type, false);
            if (binding == null) {
                return false;
            }
            return binding.RemoveSystem(Sandbox);
        }

        public void ClearSystems() {
            var keys = children.Keys.ToList();
            foreach (var key in keys) {
                RemoveSystem(key);
            }
        }
        
        #endregion

        #region Lifecycle

        protected T Singleton<T>() where T : IComponent, new() => Sandbox.Singleton<T>();
        
        protected T Singleton<T>(Func<EntityId, Entity, T> provider) where T : IComponent, new() => Sandbox.Singleton().Offer<T>(provider);

        public void Setup(Sandbox sandbox, Type key, SystemBinding binding) {
            this.Sandbox = sandbox;
            this.Key = key;
            this.Binding = binding;
        }
        
        public void SystemAdd(SystemBase parent) {

            this.Parent = parent;
            this.OnAdd();
        }

        public void SystemActive() {
            if (IsActive) {
                return;
            }
            IsActive = true;
            foreach (var pair in children) {
                pair.Value.SystemActive();
            }
            OnActive();
        }

        public void SystemDeActive() {
            if (!IsActive) {
                return;
            }
            IsActive = false;
            foreach (var pair in children) {
                pair.Value.SystemDeActive();
            }
            OnDeActive();
        }

        public void SystemRemove() {
            this.OnRemove();
            ClearSystems();
            foreach (var reaction in reactions) {
                reaction.Dispose();
            }
            reactions.Clear();
            this.Managed.Dispose();
            this.Sandbox = null;
        }

        protected virtual void OnAdd() { }
        
        protected virtual void OnActive() { }

        protected virtual void OnRemove() { }
        
        protected virtual void OnDeActive() { }

        protected override void Dispose(bool dispose) {
            Sandbox.RemoveSystem(this);
        }

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
        
        protected void OnMatch(Action<Entity> onAdd, Action<Entity> onRemove) {
            if (onAdd != null) {
                var entities = Sandbox.GetEntities();
                foreach (var entity in entities) {
                    onAdd(entity);
                }
                Subscribe<EventEntityAdd>(e => onAdd(e.Entity));
            }

            if (onRemove != null) {
                Subscribe<EventEntityRemove>(e => onAdd(e.Entity));
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
    
    public sealed class SystemRoot : SystemBase {
        
    }
    
    public class ActionsSystem : SystemBase {

        private readonly Dictionary<EventId, ISystemReaction> id2Reactions = new Dictionary<EventId, ISystemReaction>();
        
        public EventId Add<TEvent>(Action<TEvent> action) where TEvent: IEvent {
            var reaction = CreateReaction<TEvent>();
            reaction.OnEvent(action);
            id2Reactions.Add(reaction.ProcessId, reaction);
            return reaction.ProcessId;
        }
                
        public bool Remove(EventId id) {
            if (id2Reactions.TryGetValue(id, out var reaction)) {
                reaction.Dispose();
                return true;
            }
            return false;
        }
    }
    

}