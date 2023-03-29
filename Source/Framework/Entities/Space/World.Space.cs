using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using Coorth.Collections;

namespace Coorth.Framework;

public readonly record struct Space(World World, int Id, int Version) {
    public readonly World World = World;
    public readonly int Id = Id;
    public readonly int Version = Version;

    public void Destroy() {
        World.DestroySpace(this);
    }
}

public struct SpaceContext {
    public int Id;
    public int Version;
    public HashSet<EntityId> Entities;
}

public partial class World {

    private readonly Stack<int> spaceIds = new();

    private ValueList<SpaceContext> spaces = new(1);

    private readonly Space defaultSpace;

    private void InitSpace(out Space space) {
        space = CreateSpace();
    }

    private ref SpaceContext _CreateSpace() {
        if (spaces.Count > 0) {
            var index = spaceIds.Pop();
            ref var context = ref spaces[index];
            context.Version = -context.Version;
            context.Id = index;
            return ref context;
        }
        else {
            var index = spaces.Count;
            ref var context = ref spaces.Add();
            context.Version = 1;
            context.Id = index;
            context.Entities = new HashSet<EntityId>();
            return ref context;
        }
    }
    
    public Space CreateSpace() {
        var context = _CreateSpace();
        return new Space(this, context.Id, context.Version);
    }

    private ref SpaceContext _GetSpace(in Space space) {
        if (space.World != this || space.Id > spaces.Count ) {
            throw new ArgumentException();
        }
        ref var context = ref spaces[space.Id];
        if (space.Version != context.Version) {
            throw new ArgumentException();
        }
        return ref context;
    }

    private Space GetSpace(int id) {
        if (id > spaces.Count) {
            throw new ArgumentException();
        }
        ref var context = ref spaces[id];
        return new Space(this, context.Id, context.Version);
    }
    
    private void AddEntityToSpace(int spaceId, in EntityId entityId) {
        ref var context = ref spaces[spaceId];
        context.Entities.Add(entityId);
    }
    
    private void RemoveEntityFromSpace(int spaceId, in EntityId entityId) {
        ref var context = ref spaces[spaceId];
        context.Entities.Remove(entityId);
    }
    
    public bool DestroySpace(in Space space) {
        ref var context = ref _GetSpace(space);
        if (space.Version != context.Version) {
            return false;
        }

        while (context.Entities.Count > 0) {
            var entityId = context.Entities.First();
            DestroyEntity(entityId);
        }
        return true;
    }
}