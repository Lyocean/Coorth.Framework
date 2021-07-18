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

        public ArchetypeBuilder CreateArchetype() {
            var archetype = new ArchetypeBuilder(this, emptyArchetype);
            return archetype;
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
    }
}