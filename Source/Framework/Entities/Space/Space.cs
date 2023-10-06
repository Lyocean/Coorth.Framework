﻿using System;
using System.Collections;
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

    #region Common
    
    public readonly World World;
    
    public readonly int Index;
    
    public readonly Guid Id;
    
    public string Name { get; private set; }
    
    public Space? Parent { get; private set; }

    private readonly Dictionary<Guid, Space> children = new();
    public IReadOnlyDictionary<Guid, Space> Children => children;

    private SpaceChunk[] chunks;

    private const int CHUNK_SIZE = 4096;

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
        var index = 0;
        var values = new Space[children.Count];
        foreach (var (_, space) in children) {
            values[index] = space;
            index++;
        }
        foreach (var space in values) {
            space.Dispose();
        }
        
        children.Clear();
        ClearEntities();
        Parent?.children.Remove(Id);
        Parent = null;
        World.spaces.Remove(Id);
    }
    
    #endregion

    #region Entity

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int AddEntity(int entity_index) {
        var space_index = count;
        count++;

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int GetEntity(int space_index) {
        var chunk_index = space_index / CHUNK_SIZE;
        var value_index = space_index % CHUNK_SIZE;
        ref var chunk = ref chunks[chunk_index];
        return chunk.Entities[value_index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void RemoveEntity(int space_index) {
        var chunk_index = space_index / CHUNK_SIZE;
        var value_index = space_index % CHUNK_SIZE;
        ref var chunk = ref chunks[chunk_index];
        chunk.Entities[value_index] = 0;
        count--;
        if (space_index == count) {
            return;
        }
        chunk.Entities[value_index] = Move(count, space_index);
    }

    #endregion

    private int Move(int space_index, int target_index) {
        var chunk_index = space_index / CHUNK_SIZE;
        var value_index = space_index % CHUNK_SIZE;
        ref var chunk = ref chunks[chunk_index];
        var entity_index = chunk.Entities[value_index];
        ref var context = ref World.GetContext(entity_index);
        context.SpaceIndex = target_index;
        return entity_index;
    }

    public Space CreateSpace(string name) {
        var space = World.CreateSpace(name);
        space.Parent = this;
        children.Add(space.Id, space);
        return space;
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
        for (var index = count - 1; index >= 0; index--) {
            var chunk_index = index / CHUNK_SIZE;
            var value_index = index % CHUNK_SIZE;
            ref var chunk = ref chunks[chunk_index];
            var entity_index = chunk.Entities[value_index];
            World.DestroyEntity(entity_index);
        }
        count = 0;
    }

    public SpaceEntities GetEntities() {
        return new SpaceEntities(this);
    }
    
    public readonly struct SpaceEntities {

        public readonly Space Space;

        public SpaceEntities(Space space) {
            Space = space;
        }
        
        public Enumerator GetEnumerator() => new(Space);
    }
    
    public struct Enumerator : IEnumerator<Entity> {
        public readonly Space Space;
        public Entity Current { get; private set; }
        object IEnumerator.Current => Current;

        public Enumerator(Space space) {
            Space = space;
        }

        public bool MoveNext() {
            return false;
        }

        public void Reset() {
            
        }


        public void Dispose() {
            
        }
    }
    
}

