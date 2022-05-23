using System;
using System.Collections.Generic;

namespace Coorth.Framework; 

public readonly record struct Archetype {

    internal readonly ArchetypeDefinition Definition;

    public readonly Sandbox Sandbox;
    
    public bool IsNull => Sandbox == null || Definition == null;

    public int Count => Definition?.ComponentCount ?? 0;

    public bool Has<T>() where T : IComponent => Definition.HasComponent(ComponentType<T>.TypeId);

    internal Archetype(Sandbox sandbox, ArchetypeDefinition definition) {
        Sandbox = sandbox;
        Definition = definition;
    }

    public Entity CreateEntity() => Sandbox.CreateEntity(Definition);

    public void CreateEntities(Span<Entity> span) => Sandbox.CreateEntities(Definition, span);

    public void CreateEntities(IList<Entity> list, int count) => Sandbox.CreateEntities(Definition, list, count);

    public void CreateEntities(IList<Entity> list, int start, int count) => Sandbox.CreateEntities(Definition, list, start, count);

    public Entity[] CreateEntities(int count) {
        var array = new Entity[count];
        Sandbox.CreateEntities(Definition, array.AsSpan());
        return array;
    }
}