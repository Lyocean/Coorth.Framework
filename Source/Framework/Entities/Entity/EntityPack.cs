using System;
using System.Buffers;

namespace Coorth.Framework; 

public sealed class EntityPack : Disposable {

    public readonly ArchetypeDefinition Archetype;
    
    public readonly ArraySegment<ComponentPack> Components;

    internal EntityPack(ArchetypeDefinition archetype) {
        Archetype = archetype;
        var capacity = archetype.ComponentCount;
        var array = ArrayPool<ComponentPack>.Shared.Rent(capacity);
        Components = new ArraySegment<ComponentPack>(array, 0, capacity);
    }

    protected override void OnDispose() {
        if (Components.Array == null) {
            return;
        }
        ArrayPool<ComponentPack>.Shared.Return(Components.Array, true);
    }
}