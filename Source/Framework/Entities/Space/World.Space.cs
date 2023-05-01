using System;
using System.Collections.Generic;
using System.Linq;
using Coorth.Collections;

namespace Coorth.Framework;

public struct SpaceContext {
    public int Id;
    public int Version;
    public HashSet<EntityId> Entities;
}

public partial class World {

    private readonly Stack<int> spaceIds = new();

    private ValueList<SpaceContext> spaces = new(1);

    private readonly Space defaultSpace;

    private void InitSpaces(out Space space) {
        space = CreateSpace();
    }

    private void ClearSpaces() {
        spaces.Clear();
    }
    
    private ref SpaceContext _CreateSpace() {
        if (spaceIds.Count > 0) {
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

    public Space GetSpace(EntityId id) {
        if (id.Index >= contexts.Count) {
            throw new ArgumentException("EntityId is invalid.");
        }
        ref var context = ref contexts.Ref(id.Index);
        if (context.Index != id.Index || context.Version != id.Version) {
            throw new ArgumentException("EntityId is invalid.");
        }
        return GetSpace(context.SpaceIndex);
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

    public bool HasSpace(in Space space) {
        if(spaces.Count <= space.Id) {
            return false;
        }
        ref var context = ref spaces[space.Id];
        return context.Id == space.Id && context.Version == space.Version;
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
        context.Entities.Clear();
        return true;
    }
}