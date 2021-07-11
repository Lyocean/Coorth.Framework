using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Coorth {
    internal struct Entities {
        
        // private int currentId;
        //
        // private ChunkList<EntityContext> contexts;
        //
        // private readonly Stack<int> resumeIds;
        //
        // private int count;
        //
        // public int Count => count;
        //
        // private Entity singleton;
        //
        // private readonly Dictionary<int, RawList<Archetype>> archetypes;
        //
        // public readonly Archetype EmptyArchetype;
        //
        // public Entities(Sandbox sandbox, int listCapacity, int chunkCapacity) {
        //
        //     currentId = 0;
        //     count = 0;
        //     
        //     contexts = new ChunkList<EntityContext>(listCapacity, chunkCapacity);
        //     resumeIds = new Stack<int>(chunkCapacity);
        //     archetypes = new Dictionary<int, RawList<Archetype>>();
        //     EmptyArchetype = new Archetype(sandbox);
        //
        //     singleton = new Entity(null, EntityId.Null);
        // }

        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // private void OnCreate(ref EntityContext context, int index, Archetype group) {
        //     Interlocked.Increment(ref count);
        //     context.Archetype = group;
        //     context.GroupIndex = group.AddEntity(index);
        //     context.Index = index;
        // }
        //
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public ref EntityContext Create(Archetype group) {
        //     if (resumeIds.Count > 0) {
        //         var index = resumeIds.Pop();
        //         ref var context = ref contexts.Alloc(index);
        //         OnCreate(ref context, index, group);
        //         if (context.Components.Length < group.ComponentCount) {
        //             context.Components = new int[group.ComponentCapacity];
        //         }
        //         return ref context;
        //     }
        //     else {
        //         var index = Interlocked.Increment(ref currentId);
        //         ref var context = ref contexts.Alloc(index);
        //         OnCreate(ref context, index, group);
        //         context.Version = 1;
        //         context.Components = new int[EmptyArchetype.ComponentCapacity];
        //         return ref context;
        //     }
        // }
        
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public bool Singleton(Sandbox sandbox, out Entity entity) {
        //     if (singleton.Sandbox == null) {
        //         ref var context = ref contexts.Ref(0);
        //         context.Archetype = EmptyArchetype;
        //         context.Index = 0;
        //         context.Version = 1;
        //         singleton = context.GetEntity(sandbox);
        //
        //         entity = singleton;
        //         return true;
        //     }
        //     entity = singleton;
        //     return false;
        // }

        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public bool Has(EntityId id) {
        //     if (id.Index >= contexts.Count) {
        //         return false;
        //     }
        //
        //     ref var context = ref contexts.Ref(id.Index);
        //     return context.Index == id.Index && context.Version == id.Version;
        // }
        //
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public ref EntityContext GetContext(in int index) {
        //     return ref contexts.Ref(index);
        // }
        //
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public bool Destroy(Sandbox sandbox, in EntityId id) {
        //     if (id.Index >= contexts.Count) {
        //         return false;
        //     }
        //     ref var context = ref contexts.Ref(id.Index);
        //     if (context.Index == id.Index && context.Version == id.Version) {
        //         context.Version++;
        //         context.Archetype.RemoveEntity(context.GroupIndex);
        //         context.Archetype = null;
        //         resumeIds.Push(id.Index);
        //         Interlocked.Decrement(ref count);
        //         return true;
        //     }
        //     else {
        //         return false;
        //     }
        // }
        //
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public IEnumerable<EntityId> GetEntityIds() {
        //     for (var i = 0; i < contexts.ChunkCount; i++) {
        //         var list = contexts.GetChunk(i);
        //         for (var j = 0; j < list.Count; j++) {
        //             var context = list.Get(j);
        //             if (context.Version > 0) {
        //                 yield return context.Id;
        //             }
        //         }
        //     }
        // }

        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // private Archetype OnGroupAdd(Archetype archetype) {
        //     if (!archetypes.TryGetValue(archetype.Flag, out var list)) {
        //         list = new RawList<Archetype>();
        //         list.Add(archetype);
        //         archetypes.Add(archetype.Flag, list);
        //         archetype.Sandbox.OnEntityGroupAdd(archetype);
        //         return archetype;
        //     }
        //     for (var i = 0; i < list.Count; i++) {
        //         var itemGroup = list.Get(i);
        //         if (itemGroup.ComponentCount != archetype.ComponentCount) {
        //             continue;
        //         }
        //         if (archetype.ComponentsEquals(itemGroup)) {
        //             return itemGroup;
        //         }
        //     }
        //     list.Add(archetype);
        //     archetype.Sandbox.OnEntityGroupAdd(archetype);
        //     return archetype;
        // }
        //
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public Archetype AddComponent(Archetype archetype, int componentType) {
        //     if (archetype.AddComponent(componentType, out var entityGroup)) {
        //         return OnGroupAdd(entityGroup);
        //     }
        //     return entityGroup;
        // }
        //
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public Archetype RemoveComponent(Archetype archetype, int componentType) {
        //     if (archetype.RemoveComponent(componentType, out var entityGroup)) {
        //         return OnGroupAdd(entityGroup);
        //     }
        //     return entityGroup; 
        // }
        //
        // public IEnumerable<Archetype> GetEntityGroups() {
        //     foreach (var pair in archetypes) {
        //         var list = pair.Value;
        //         if (list.Values != null) {
        //             for (var i = 0; i < list.Count; i++) {
        //                 var item = list.Values[i];
        //                 if (item != null) {
        //                     yield return item;
        //                 }
        //             }
        //         }
        //     }
        // }
    }
}