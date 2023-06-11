using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Coorth.Serialize;


namespace Coorth.Framework;

public partial class World {

    #region Archetype Common

    private readonly Archetype rootArchetype;
    public Archetype RootArchetype => rootArchetype;

    private readonly Dictionary<int, Archetype> archetypes = new();

    private const int ARCHETYPE_CHUNK_SIZE = 4096;
    
    private void SetupArchetypes(out Archetype root) {
        root = new Archetype(this, ArchetypeDefinition.Empty, ARCHETYPE_CHUNK_SIZE);
        archetypes.Add(rootArchetype.Hash, rootArchetype);
    }

    private void ClearArchetypes() {
        archetypes.Clear();
    }

    #endregion

    
    #region Create Archetype

    private Archetype CreateArchetype(Span<ComponentType> span) {
        var archetype = rootArchetype;
        for (var i = 0; i < span.Length; i++) {
            ref readonly var componentType = ref span[i];
            archetype = archetype.NextArchetype(in componentType);
        }
        return archetype;
    }
    
    private Archetype CreateArchetype(Span<Type> span) {
        var archetype = rootArchetype;
        for (var i = 0; i < span.Length; i++) {
            var type = ComponentRegistry.Get(span[i]);
            archetype = archetype.NextArchetype(in type);
        }
        return archetype;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype NextArchetype(Archetype archetype, in ComponentType type) {
        return archetype.NextArchetype(in type);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype(ArchetypeDefinition definition) {
        return CreateArchetype(definition.Types.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype(params Type[] types) {
        return CreateArchetype(types.AsSpan());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ArchetypeBuilder BuildArchetype() {
        var builder = new ArchetypeBuilder(rootArchetype);
        return builder;
    }

    #endregion


    #region Get Archetype

    public Archetype GetArchetype(in EntityId id) {
        ref var entity_context = ref entities.Get(in id);
        return entity_context.Archetype;
    }

    #endregion

    #region Archetype Component

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void OnComponentMove(int entity_index, in ComponentType type, int component_index) {
        ref var entity = ref entities.Get(entity_index);
        entity.Archetype.MoveComponent(entity.LocalIndex, in type, component_index);
    }

    #endregion


    #region Serialize Archetype

    public void WriteArchetype<TWriter>(ref TWriter writer, in Archetype archetype) where TWriter : ISerializeWriter {
        var component_types = archetype.Types;
        writer.BeginList<Type>(component_types.Count);
        foreach (var component_type in component_types) {
            writer.WriteType(component_type.Type);
        }
        writer.EndList();
    }
    
    public Archetype ReadArchetype<TReader>(ref TReader reader) where TReader : ISerializeReader {
        var archetype = rootArchetype;
        reader.BeginList<Type>(out var count);
        for (var i = 0; i < count; i++) {
            var type = reader.ReadType();
            var component_type = ComponentRegistry.Get(type);
            archetype = NextArchetype(archetype, in component_type);
        }
        reader.EndList();
        return archetype;
    }

    #endregion

}