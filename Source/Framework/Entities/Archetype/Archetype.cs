using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth.Framework;

public readonly record struct Archetype {
    private readonly ArchetypeDefinition definition;

    public readonly World World;

    public bool IsNull => World == null || definition == null;

    public int Count => definition?.ComponentCount ?? 0;

    public bool Has<T>() where T : IComponent => definition.HasComponent(ComponentType<T>.TypeId);

    internal Archetype(World world, ArchetypeDefinition definition) {
        World = world;
        this.definition = definition;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity CreateEntity() => World.CreateEntity(definition);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity CreateEntity(Space space) => World.CreateEntity(definition, space);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CreateEntities(Span<Entity> span, Space? space = null) => World.CreateEntities(definition, span, space);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CreateEntities(IList<Entity> list, int count, Space? space = null) => World.CreateEntities(definition, list, count, space);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CreateEntities(IList<Entity> list, int start, int count, Space? space = null) => World.CreateEntities(definition, list, start, count, space);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity[] CreateEntities(int count, Space? space = null) {
        var array = new Entity[count];
        World.CreateEntities(definition, array.AsSpan(), space);
        return array;
    }
}