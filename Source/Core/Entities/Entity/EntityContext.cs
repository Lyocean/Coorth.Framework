using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Coorth {
    internal struct EntityContext {

        public IndexDict<int> Components;

        public ArchetypeDefinition Archetype;

        public int Index;

        public int Version;

        public int Group;

        public EntityId Id => new EntityId(Index, Version);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity GetEntity(Sandbox sandbox) => new Entity(sandbox, new EntityId(Index, Version));

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
    }
}