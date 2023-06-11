using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth.Framework;

internal struct SpaceChunk {
    public readonly int[] Entities;

    public SpaceChunk(int capacity) {
        Entities = new int[capacity];
    }
}

public class Space : Disposable {
    
    public readonly World World;
    
    public readonly int Index;
    
    public readonly Guid Id;
    
    public string Name { get; private set; }
    
    public Space? Parent { get; private set; }

    private readonly Dictionary<Guid, Space> children = new();
    public IReadOnlyDictionary<Guid, Space> Children => children;

    private SpaceChunk[] chunks;

    private const int CHUNK_SIZE = 4096;

    private int reusing = -1;
    
    private int count;
    public int Count => count;

    public int Capacity => chunks.Length * CHUNK_SIZE;

    internal Space(World world, Guid id, int index, string name) {
        World = world;
        Index = index;
        Id = id;
        Name = name;
        Parent = null;
        chunks = new [] { new SpaceChunk(CHUNK_SIZE) };
    }
    
    protected override void OnDispose() {
        foreach (var (_, space) in children) {
            space.Dispose();
        }
        children.Clear();
        ClearEntities();
        Parent?.children.Remove(Id);
        Parent = null;
        World.spaces.Remove(Id);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int AddEntity(int entity_index) {
        count++;
        if (reusing > 0) {
            var space_index = reusing;
            var chunk_index = space_index / CHUNK_SIZE;
            var value_index = space_index % CHUNK_SIZE;
            ref var chunk = ref chunks[chunk_index];
            reusing = chunk.Entities[value_index];
            chunk.Entities[value_index] = entity_index;
            return space_index;
        }
        else {
            var space_index = count;
            var chunk_index = space_index / CHUNK_SIZE;
            var value_index = space_index % CHUNK_SIZE;
            if (chunk_index >= chunks.Length) {
                Array.Resize(ref chunks, chunk_index + 1);
                chunks[chunk_index] = new SpaceChunk(CHUNK_SIZE);
            }
            ref var chunk = ref chunks[chunk_index];
            chunk.Entities[value_index] = entity_index;
            return space_index;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int GetEntity(int space_index) {
        var chunk_index = space_index / CHUNK_SIZE;
        var value_index = space_index % CHUNK_SIZE;
        ref var chunk = ref chunks[chunk_index];
        return chunk.Entities[value_index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void RemoveEntity(int index) {
        var chunk_index = index / CHUNK_SIZE;
        var value_index = index % CHUNK_SIZE;
        ref var chunk = ref chunks[chunk_index];
        chunk.Entities[value_index] = reusing;
        reusing = index;
        count--;
    }

    public Entity CreateEntity() {
        return World.CreateEntity(World.RootArchetype, this);
    }

    public Entity CreateEntity(Archetype archetype) {
        return World.CreateEntity(archetype, this);
    }

    public void CreateEntities(Archetype archetype, Span<Entity> span) {
        World.CreateEntities(archetype, this, span);
    }
    
    public void CreateEntities(Archetype archetype, IList<Entity> list, int length) {
        World.CreateEntities(archetype, this, list, length);
    }
    
    public Entity[] CreateEntities(int length) {
        return World.CreateEntities(World.RootArchetype, this, length);
    }
    
    public void ClearEntities() {
        for (var index = 0; index < count; index++) {
            var chunk_index = index / CHUNK_SIZE;
            var value_index = index % CHUNK_SIZE;
            ref var chunk = ref chunks[chunk_index];
            var entity_index = chunk.Entities[value_index];
            World.DestroyEntity(entity_index);
        }
        count = 0;
    }
}

