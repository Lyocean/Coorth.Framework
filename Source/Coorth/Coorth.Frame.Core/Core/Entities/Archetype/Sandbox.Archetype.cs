using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth {
    public partial class Sandbox {
        
        private readonly Dictionary<int, List<Archetype>> archetypes = new Dictionary<int, List<Archetype>>();

        private Archetype emptyArchetype;
        
        private readonly Dictionary<int, List<ArchetypeGroup>> componentToGroups = new Dictionary<int, List<ArchetypeGroup>>();

        private readonly Dictionary<EntityMatcher, ArchetypeGroup> matcherToGroups = new Dictionary<EntityMatcher, ArchetypeGroup>();

        internal (int Index, int Chunk) ArchetypeCapacity;
        
        private void InitArchetypes(int indexCapacity, int chunkCapacity) {
            ArchetypeCapacity = (indexCapacity, chunkCapacity);
            emptyArchetype = new Archetype(this);
        }

        #region Create & Add & Remove & Get

        
        public ArchetypeBuilder CreateArchetype() {
            var archetype = new ArchetypeBuilder(this, emptyArchetype);
            return archetype;
        }

        public ArchetypeCompiled GetArchetype(in EntityId entityId) {
            ref var context = ref GetContext(entityId.Index);
            return new ArchetypeCompiled(this, context.Archetype);
        }
        
        public ArchetypeGroup GetArchetypeGroup(EntityMatcher matcher) {
            if(matcherToGroups.TryGetValue(matcher, out var archetypeGroup)) {
                return archetypeGroup;
            }
            archetypeGroup = new ArchetypeGroup(this, matcher);
            matcherToGroups[matcher] = archetypeGroup;
            foreach(var compType in matcher.AllTypes) {
                if(!componentToGroups.TryGetValue(compType, out var list)) {
                    list = new List<ArchetypeGroup>();
                    componentToGroups[compType] = list;
                }
                list.Add(archetypeGroup);
            }
            foreach (var pair in archetypes) {
                for (var i = 0; i < pair.Value.Count; i++) {
                    var archetype = pair.Value[i];
                    archetypeGroup.Match(archetype);
                }
            }
            return archetypeGroup;
        }
        
        internal void OnAddArchetype(Archetype archetype) {
            if (!archetypes.TryGetValue(archetype.Flag, out var archetypeList)) {
                archetypeList = new List<Archetype> {archetype};
                archetypes[archetype.Flag] = archetypeList;
            }
            else {
                foreach (var item in archetypeList) {
                    if (item.ComponentsEquals(archetype)) {
                        break;
                    }
                }
                archetypeList.Add(archetype);
            }
            for(var i =0;i< archetype.Types.Length; i++) {
                var typeId = archetype.Types[i];
                if (componentToGroups.TryGetValue(typeId, out var list)) {
                    foreach (var archetypeGroup in list) {
                        archetypeGroup.Match(archetype);
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnEntityAddComponent(ref EntityContext context, int componentType, int componentIndex) {
            var archetype = context.Archetype;
            archetype.RemoveEntity(context.Group);

            archetype = archetype.AddComponent(componentType);
            context.Archetype = archetype;
            context.Group = archetype.AddEntity(context.Index);
            
            context.Components.Add(componentType, componentIndex);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnEntityRemoveComponent(ref EntityContext context, int componentType) {
            var archetype = context.Archetype;
            archetype.RemoveEntity(context.Group);
            
            archetype = archetype.RemoveComponent(componentType);
            context.Archetype = archetype;
            context.Group = archetype.AddEntity(context.Index);
            context.Components.Remove(componentType);
        }


        #endregion


        #region Read & Write
        
        private Archetype _ReadArchetype<TSerializer>(TSerializer serializer) where TSerializer : ISerializer {
            var componentCount = serializer.Read<ushort>();
            var archetype = emptyArchetype;
            for (var i = 0; i < componentCount; i++) {
                var componentType = serializer.Read<Type>();
                archetype = archetype.AddComponent(componentType);
            }
            return archetype;
        }
        
        public ArchetypeCompiled ReadArchetype<TSerializer>(TSerializer serializer) where TSerializer : ISerializer {
            var archetype = _ReadArchetype<TSerializer>(serializer);
            return new ArchetypeCompiled(this, archetype);
        }
        
        private IList<Entity> ReadArchetypeWithEntities<TSerializer>(TSerializer serializer, IList<Entity> entities) where TSerializer : ISerializer {
            var archetype = _ReadArchetype<TSerializer>(serializer);
            var count = serializer.Read<int>();
            if (entities == null) {
                entities = new List<Entity>(count);
            }
            for (var i = 0; i < count; i++) {
                entities[i] = CreateEntity(archetype);
            }
            foreach (var componentId in archetype.Components) {
                var componentGroup = GetComponentGroup(componentId);
                for (var i = 0; i < count; i++) {
                    var entity = entities[i];
                    ReadComponent<TSerializer>(serializer, entity.Id, componentGroup.Type);
                }
            }
            return entities;
        }
        
        private void _WriteArchetype<TSerializer>(TSerializer serializer, Archetype archetype) where TSerializer : ISerializer {
            serializer.WriteDictBegin(true, typeof(Entity));
            try {
                serializer.WriteTag(nameof(archetype.ComponentCount), 0);
                serializer.WriteValue<ushort>((ushort)archetype.ComponentCount);
                
                serializer.WriteTag(nameof(archetype.Components), 1);
                serializer.WriteListBegin(typeof(Type));
                try {
                    foreach (int componentId in archetype.Components) {
                        var componentGroup = GetComponentGroup(componentId);
                        serializer.WriteValue<Type>(componentGroup.Type);
                    }
                }
                finally {
                    serializer.WriteListEnd();
                }
            }
            finally {
                serializer.WriteDictEnd();
            }
        }
        
        public void WriteArchetype<TSerializer>(TSerializer serializer, ArchetypeCompiled archetype) where TSerializer : ISerializer {
            _WriteArchetype<TSerializer>(serializer, archetype.archetype);
        }
        
        private void WriteArchetypeWithEntities<TSerializer>(TSerializer serializer, Archetype archetype) where TSerializer : ISerializer {
            serializer.WriteDictBegin(true, default);
            try {
                serializer.WriteTag(nameof(Archetype), 0);
                _WriteArchetype<TSerializer>(serializer, archetype);
                serializer.WriteTag(nameof(EntityCount), 1);
                serializer.WriteValue<int>(archetype.EntityCount);
                
                var entities = archetype.GetEntities();

                serializer.WriteTag(nameof(archetype.Components), 2);
                serializer.WriteListBegin(default);
                try {
                    foreach (var componentId in archetype.Components) {
                        var componentGroup = GetComponentGroup(componentId);
                        for (var i = 0; i < entities.Count; i++) {
                            var entityIndex = entities[i];
                            if (entityIndex <= 0) {
                                continue;
                            }
                            ref var context = ref GetContext(entityIndex);
                            var componentIndex = context[componentId];
                            componentGroup.WriteComponent<TSerializer>(serializer, componentIndex);
                        }
                    }
                }
                finally {
                    serializer.WriteListEnd();
                }
            }
            finally {
                serializer.WriteDictEnd();
            }
        }

        #endregion
    }
}