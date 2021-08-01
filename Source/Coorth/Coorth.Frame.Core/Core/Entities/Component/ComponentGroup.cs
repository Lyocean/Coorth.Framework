using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;


namespace Coorth {
    public delegate void ComponentCreator<T>(Entity entity, ref T component);

    public delegate T ComponentCloner<T>(Entity entity, ref T component);

    public delegate void ComponentRecycler<T>(Entity entity, ref T component);
    
    public delegate void OnComponentAttach<T>(Entity entity, ref T component);
    
    public delegate void OnComponentDetach<T>(Entity entity, ref T component);

    
    internal interface IComponentGroup {

        Sandbox Sandbox { get; }

        int Count { get; }
        
        Type Type { get; }

        int Id { get; }

        int AddComponent(Entity entity);
        void OnComponentAdd(in EntityId id, int componentIndex);
        
        IComponent Get(int index);
        
        void RemoveComponent(Entity entity, int componentIndex);
        void OnRemoveComponent(in EntityId id, int componentIndex);

        int CloneComponent(Entity entity, int componentIndex);

        void ReadComponent(IComponentSerializer serializer, int componentIndex);

        void WriteComponent(IComponentSerializer serializer, int componentIndex);

        ComponentAsset PackComponent(Entity entity, int componentIndex);

    }
    
    internal class ComponentGroup<T> : IComponentGroup where T : IComponent {

        #region Static

        public static readonly Type ComponentType;

        public static readonly ComponentAttribute Attribute;
        
        public static readonly int TypeId;
        
        public static readonly bool IsValueType;

        public static event OnComponentAttach<T> OnAttach;
        
        public static event OnComponentDetach<T> OnDetach;

        public ComponentCreator<T> Creator;

        public ComponentRecycler<T> Recycler;

        public ComponentCloner<T> Cloner;

        static ComponentGroup() {
            ComponentType = typeof(T);
            Attribute = ComponentType.GetCustomAttribute<ComponentAttribute>();
            TypeId = Interlocked.Increment(ref Sandbox.ComponentTypeCount);
            Sandbox.ComponentTypeIds[ComponentType] = TypeId;
            IsValueType = typeof(T).IsValueType;
            if ((typeof(IRefComponent)).IsAssignableFrom(ComponentType)) {
                OnAttach += OnRefComponentAttach;
                OnDetach += OnRefComponentDetach;
            }
        }

        private static void OnRefComponentAttach(Entity entity, ref T component) {
            ((IRefComponent) component).Entity = entity;
        }
        
        private static void OnRefComponentDetach(Entity entity, ref T component) {
            ((IRefComponent) component).Entity = default;
        }
        
        #endregion

        #region Fields

        protected readonly Sandbox sandbox;

        public Sandbox Sandbox => sandbox;

        internal ChunkList<T> components;
        
        internal ChunkList<int> mapping;
        
        public int Count => components.Count;

        public Type Type => ComponentType;

        public int Id => TypeId;
        
        private RawList<IComponentGroup> dependency;

        public ComponentGroup(Sandbox sandbox, int indexCapacity, int chunkCapacity) {
            this.sandbox = sandbox;
            if (Attribute != null) {
                switch (Attribute.Scale) {
                    case ComponentScale.Singleton:
                        this.components = new ChunkList<T>(1, 1);
                        this.mapping = new ChunkList<int>(1, 1); 
                        break;
                    case ComponentScale.Small:
                        this.components = new ChunkList<T>(1, Attribute.Capacity);
                        this.mapping = new ChunkList<int>(1, Attribute.Capacity); 
                        break;
                    case ComponentScale.Large:
                        this.components = new ChunkList<T>(indexCapacity, chunkCapacity);
                        this.mapping = new ChunkList<int>(indexCapacity, chunkCapacity); 
                        break;
                    default:
                        this.components = new ChunkList<T>(indexCapacity, chunkCapacity);
                        this.mapping = new ChunkList<int>(indexCapacity, chunkCapacity); 
                        break;
                }
            } else {
                this.components = new ChunkList<T>(indexCapacity, chunkCapacity);
                this.mapping = new ChunkList<int>(indexCapacity, chunkCapacity);  
            }
        }

        #endregion

        #region Binding

        public void AddDependency(IComponentGroup componentGroup) {
            if (dependency.Values == null) {
                dependency = new RawList<IComponentGroup>(1);
            }
            if (!dependency.Values.Contains(componentGroup)) {
                dependency.Add(componentGroup);
            }
        }
        
        public bool HasDependency(Type otherType) {
            if (dependency.IsNull) {
                return false;
            }
            for (var i = 0; i < dependency.Count; i++) {
                if (dependency[i].Type == otherType) {
                    return true;
                } 
            }
            return false;
        }

        #endregion

        #region Add
        
        public int AddComponent(Entity entity) {
            var componentIndex = components.Count;
            ref var component = ref components.Add();
            mapping.Add(entity.Id.Index);
            
            if (Creator != null) {
                Creator.Invoke(entity, ref component);
            }else if (!IsValueType) {
                component = Activator.CreateInstance<T>();
            }
            OnAttach?.Invoke(entity, ref component);
            return componentIndex;
        }
        
        public int AddComponent(Entity entity, ref T componentValue) {
            var componentIndex = components.Count;
            ref var component = ref components.Add();
            mapping.Add(entity.Id.Index);
            
            component = componentValue;
            
            OnAttach?.Invoke(entity, ref componentValue);
            return componentIndex;
        }

        public void OnComponentAdd(in EntityId id, int componentIndex) {
            Sandbox._Execute(new EventComponentAdd(id, this, componentIndex));
            Sandbox._Execute(new EventComponentAdd<T>(id, this, componentIndex));
        }

        #endregion

        #region Get & Ref

        IComponent IComponentGroup.Get(int index) => components[index];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(int index) => components[index];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Ref(int index) => ref components.Ref(index);

        public T this[int index] { get => components[index]; set => components[index] = value; }

        public void OnComponentModify(in EntityId id, int componentIndex) {
            Sandbox._Execute(new EventComponentModify(id, this, componentIndex));
            Sandbox._Execute(new EventComponentModify<T>(id, this, componentIndex));
        }
        
        #endregion

        #region Remove
        
        public void RemoveComponent(Entity entity, int componentIndex) {
            ref var component = ref components.Ref(componentIndex);
            
            OnDetach?.Invoke(entity, ref component);
            
            var tailIndex = Count-1;
            
            
            if (componentIndex != tailIndex) {
                component = components[tailIndex];
                mapping[componentIndex] = mapping[tailIndex];
                
                ref var tailContext = ref sandbox.GetContext(mapping[componentIndex]);
                tailContext[TypeId] = componentIndex;
            }
            components.RemoveLast();
            mapping.RemoveLast();
        }

        public void OnRemoveComponent(in EntityId id, int componentIndex) {
            Sandbox._Execute(new EventComponentRemove(id, this, componentIndex));
            Sandbox._Execute(new EventComponentRemove<T>(id, this, componentIndex));
        }

        #endregion

        #region Clone

        public int CloneComponent(Entity entity, int componentIndex) {
            ref var sourceComponent = ref components.Ref(componentIndex);
            _Clone(entity, ref sourceComponent, out T targetComponent);
            return AddComponent(entity, ref targetComponent);
        }

        public void ReadComponent(IComponentSerializer serializer, int componentIndex) {
            ref var component = ref components[componentIndex];
            serializer.ReadComponent(out component);
        }

        public void WriteComponent(IComponentSerializer serializer, int componentIndex) {
            ref var component = ref components[componentIndex];
            serializer.WriteComponent(ref component);
        }

        internal void _Clone(Entity entity, ref T sourceComponent, out T targetComponent) {
            if (Cloner != null) {
                targetComponent =  Cloner.Invoke(entity, ref sourceComponent);
            } else if (IsValueType) {
                targetComponent = sourceComponent;
            } else if(sourceComponent is ICloneable cloneable) {
                targetComponent = (T)cloneable.Clone();
            } else {
                targetComponent = Activator.CreateInstance<T>();
            }
        }

        public ComponentAsset PackComponent(Entity entity, int componentIndex) {
            ref var component = ref components[componentIndex];
            var asset = new ComponentAsset<T>();
            asset.Pack(sandbox, entity, ref component);
            return asset;
        }

        #endregion
    }
}