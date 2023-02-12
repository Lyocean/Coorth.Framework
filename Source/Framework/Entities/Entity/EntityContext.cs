#define CHUNK_MODE

using System.Runtime.CompilerServices;


namespace Coorth.Framework;

internal record struct EntityContext {
    
    public int Index;

    public int Version;

    public ArchetypeDefinition Archetype;
    
    public int LocalIndex;

    public EntityId Id { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(Index, Version); }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetEntity(World world) => new(world, new EntityId(Index, Version));
    
#if CHUNK_MODE
    public IndexDict<int> Components;

    public int Count => Archetype.ComponentCount;

    public int this[int type] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Components[type];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Components[type] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Get(int type) => Components[type];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(int type) => Components.ContainsKey(type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGet(int type, out int value) => Components.TryGetValue(type, out value);
#else
    
    public IndexDict<int> Components;

    public int Count => Archetype.ComponentCount;

    public int this[int type] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Components[type];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Components[type] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Get(int type) => Components[type];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(int type) => Components.ContainsKey(type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGet(int type, out int value) => Components.TryGetValue(type, out value);
    
#endif
}