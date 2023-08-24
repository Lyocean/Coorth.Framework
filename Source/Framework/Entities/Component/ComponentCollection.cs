#if NET7_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Coorth.Framework; 

public readonly struct ComponentCollection<T> where T: IComponent {
    private readonly ComponentGroup<T> group;
    
    internal ComponentCollection(World world) {
        group = world.GetComponentGroup<T>();
    }
    
    public void ForEach(ActionR1<T> action) {
        for (var i = 0; i < group.ChunkCount; i++) {
            ref var chunk = ref group.GetChunk(i);
            for (var j = chunk.Enable; j < chunk.Count; j++) {
                ref var value = ref chunk.Value[j];
                if (!EntityFlags.IsComponentEnable(chunk.Flags[j])) {
                    continue;
                }
                action(ref value);
            }
        }
    }
    
    public void ForEach(ActionI1<T> action) {
        for (var i = 0; i < group.ChunkCount; i++) {
            ref var chunk = ref group.GetChunk(i);
            for (var j = chunk.Enable; j < chunk.Count; j++) {
                ref var value = ref chunk.Value[j];
                if (!EntityFlags.IsComponentEnable(chunk.Flags[j])) {
                    continue;
                }
                action(in value);
            }
        }
    }
    
    public void ForEach<TState>(in TState state, ActionI1R1<TState, T> action) {
        for (var i = 0; i < group.ChunkCount; i++) {
            ref var chunk = ref group.GetChunk(i);
            for (var j = chunk.Enable; j < chunk.Count; j++) {
                ref var value = ref chunk.Value[j];
                if (!EntityFlags.IsComponentEnable(chunk.Flags[j])) {
                    continue;
                }
                action(in state, ref value);
            }
        }
    }
    
    public void ForEach(ActionI1R1<Entity, T> action) {
        var world = group.World;
        for (var i = 0; i < group.ChunkCount; i++) {
            ref var chunk = ref group.GetChunk(i);
            for (var j = chunk.Enable; j < chunk.Count; j++) {
                ref var value = ref chunk.Value[j];
                if (!EntityFlags.IsComponentEnable(chunk.Flags[j])) {
                    continue;
                }
                var entity = world.GetEntity(chunk.Index[j]);
                action(in entity, ref value);
            }
        }
    }
    
    public void ForEach<TState>(in TState state, ActionI2R1<TState, Entity, T> action) {
        var world = group.World;
        for (var i = 0; i < group.ChunkCount; i++) {
            ref var chunk = ref group.GetChunk(i);
            for (var j = chunk.Enable; j < chunk.Count; j++) {
                ref var value = ref chunk.Value[j];
                if (!EntityFlags.IsComponentEnable(chunk.Flags[j])) {
                    continue;
                }
                var entity = world.GetEntity(chunk.Index[j]);
                action(in state, in entity, ref value);
            }
        }
    }
    
    public Enumerator GetEnumerator() => new(group);

    public ref struct Enumerator {
        private readonly ComponentGroup<T> group;
        
#if NET7_0_OR_GREATER
        private ref T current;
        public ref T Current => ref current;
#else
        private Ref<T> current;
        public Ref<T> Current => current;
#endif

        private int index;
        
        public Enumerator(ComponentGroup<T> value) {
            group = value;
#if NET7_0_OR_GREATER
            current = Unsafe.NullRef<T>();
#else
            current = default;
#endif
        }
        
        public bool MoveNext() {
            while (index < group.Count) {
                var chunk_index = index / group.ChunkSize;
                var value_index = index % group.ChunkSize;
                ref var chunk = ref group.GetChunk(chunk_index);
                if (!EntityFlags.IsComponentEnable(chunk.Flags[value_index])) {
                    continue;
                }
#if NET7_0_OR_GREATER
                current = ref chunk.Value[value_index];
#else
                current = new Ref<T>(ref chunk.Value[value_index]);
#endif
                index++;
                return true;
            }

            return false;
        }
        
        public void Reset() {
            index = 0;
#if NET7_0_OR_GREATER
            current = Unsafe.NullRef<T>();
#else
            current = default;
#endif
        }
    }

    public EntityIter GetIter() => new(group);
    
    public readonly ref struct EntityIter {
        private readonly ComponentGroup<T> group;
        public EntityIter(ComponentGroup<T> value) {
            group = value;
        }
        
        public EntityEnumerator GetEnumerator() => new(group);

    }

    public ref struct EntityEnumerator {
        private readonly ComponentGroup<T> group;

        private EntityComponents<T> current;
        public EntityComponents<T> Current => current;

        private int index;
        
        public EntityEnumerator(ComponentGroup<T> value) {
            group = value;
            current = default;
        }
        
        public bool MoveNext() {
            while (index < group.Count) {
                var chunk_index = index / group.ChunkSize;
                var value_index = index % group.ChunkSize;
                ref var chunk = ref group.GetChunk(chunk_index);
                if (!EntityFlags.IsComponentEnable(chunk.Flags[value_index])) {
                    continue;
                }
                var entity = group.World.GetEntity(chunk.Index[value_index]);
                current = new EntityComponents<T>(entity, ref chunk.Value[value_index]);
                index++;
                return true;
            }
            return false;
        }
        
        public void Reset() {
            index = 0;
            current = default;
        }
    }
}
