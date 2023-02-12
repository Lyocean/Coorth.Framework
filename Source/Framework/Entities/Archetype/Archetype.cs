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
    public void CreateEntities(Span<Entity> span) => World.CreateEntities(definition, span);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CreateEntities(IList<Entity> list, int count) => World.CreateEntities(definition, list, count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CreateEntities(IList<Entity> list, int start, int count) => World.CreateEntities(definition, list, start, count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity[] CreateEntities(int count) {
        var array = new Entity[count];
        World.CreateEntities(definition, array.AsSpan());
        return array;
    }
}