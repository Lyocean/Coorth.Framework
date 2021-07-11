using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Coorth {
    internal struct EntityContext {
        
        //public Dictionary<int, int> Components;

        public IndexDict<int> Components;

        public Archetype Archetype;

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
        public int Get(int type) {
            return Components[type];
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has(int type) => Components.ContainsKey(type);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet(int type, out int value) {
            return Components.TryGetValue(type, out value);
            // if (Components.TryGetValue(type, out var index)) {
            //     value = Components[index];
            //     return true;
            // }
            //
            // value = 0;
            // return false;
        }
    }
}