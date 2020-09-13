using System;
using System.Collections.Generic;

namespace Coorth.ECS {
    internal sealed class Groups {

        private EcsContainer container;

        private Dictionary<int, List<EntityGroup>> componentToGroups = new Dictionary<int, List<EntityGroup>>();
        private Dictionary<IMatcher, EntityGroup> matcherToGroups = new Dictionary<IMatcher, EntityGroup>();

        public Groups(EcsContainer container) {
            this.container = container;
        }

        public EntityGroup GetGroup(IMatcher matcher) {
            if(matcherToGroups.TryGetValue(matcher, out var group)) {
                return group;
            }
            group = new EntityGroup(container, matcher);
            matcherToGroups[matcher] = group;
            foreach(var compType in matcher.AllTypes) {
                if(!componentToGroups.TryGetValue(compType, out var groups)) {
                    groups = new List<EntityGroup>();
                    componentToGroups[compType] = groups;
                }
                groups.Add(group);
            }
            foreach(var id in container.GetEntityIds()) {
                ref var data = ref container.GetData(id.Index);
                group.Match(ref data);
            }
            return group;
        }

        public bool RemoveGroup(EntityGroup group) {
            if (matcherToGroups.Remove(group.matcher)) {
                foreach (var compType in group.matcher.AllTypes) {
                    if (componentToGroups.TryGetValue(compType, out var groups)) {
                        groups.Remove(group);
                    }
                }
                return true;
            }
            return false;
        }

        public void OnComponentAdd<T>(ref EntityData data) {
            var typeId = Components.Types<T>.Id;
            if(componentToGroups.TryGetValue(typeId, out var groups)) {
                foreach(var group in groups) {
                    group.Match(ref data, typeId, true);
                }
            }
        }

        public void OnComponentModify<T>(ref EntityData data) {
            
        }

        public void OnComponentRemove<T>(ref EntityData data) {
            var typeId = Components.Types<T>.Id;
            if (componentToGroups.TryGetValue(typeId, out var groups)) {
                foreach (var group in groups) {
                    group.Match(ref data, typeId, true);
                }
            }
        }
    }
}
