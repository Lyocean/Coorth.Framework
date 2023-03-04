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
    
    public IndexDict<int> Components;
    
    public int Count => Archetype.ComponentCount;

    public int this[int type] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Archetype.GetComponentIndex(ref this, type);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Archetype.SetComponentIndex(ref this, type, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Get(int type) => Archetype.GetComponentIndex(ref this, type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(int type) => Archetype.HasComponent(ref this, type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGet(int type, out int value) => Archetype.TryGetComponentIndex(ref this, type, out value);

}