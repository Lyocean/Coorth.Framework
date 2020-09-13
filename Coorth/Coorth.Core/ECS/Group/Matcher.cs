using System;
using System.Linq;
using System.Collections.Generic;

namespace Coorth.ECS {

    public interface IMatcher {
        int[] AllTypes { get; }
        bool Match(EcsContainer container, EntityId id);
        bool IsExclude(int type);
        bool IsInclude(int type);
    }

    public class ComponentMatcher : IMatcher {
        protected int[] includes;
        protected int[] excludes;
        public int[] AllTypes { get; private set; }

        public bool Match(EcsContainer container, EntityId id) {
            ref var data = ref container.GetData(id.Index);
            var components = data.Components;
            if (includes != null) {
                for (int i = 0, iMax = includes.Length; i < iMax; i++) {
                    if (!components.ContainsKey(includes[i])) {
                        return false;
                    }
                }
            }
            if (excludes != null) {
                for (int i = 0, iMax = excludes.Length; i < iMax; i++) {
                    if (components.ContainsKey(excludes[i])) {
                        return false;
                    }
                }
            }
            return true;
        }

        private int[] Expand(int[] array, int[] expand) {
            if (array == null) {
                array = new int[expand.Length];
                Array.Copy(expand, array, expand.Length);
            } else {
                array = array.Union(expand).ToArray();
            }
            return array;
        }

        private ComponentMatcher Include(params int[] types) {
            includes = Expand(includes, types);
            AllTypes = Expand(AllTypes, includes);
            return this;
        }

        private ComponentMatcher Exclude(params int[] types) {
            excludes = Expand(excludes, types);
            AllTypes = Expand(AllTypes, excludes);
            return this;
        }

        public ComponentMatcher Include<T>() where T : IComponent {
            return Include(Components.Types<T>.Id);
        }

        public ComponentMatcher Include<T1, T2>() where T1 : IComponent where T2 : IComponent {
            return Include(Components.Types<T1>.Id, Components.Types<T2>.Id);
        }

        public ComponentMatcher Include<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            return Include(Components.Types<T1>.Id, Components.Types<T2>.Id, Components.Types<T3>.Id);
        }

        public ComponentMatcher Exclude<T>() where T : IComponent {
            return Exclude(Components.Types<T>.Id);
        }

        public ComponentMatcher Exclude<T1, T2>() where T1 : IComponent where T2 : IComponent {
            return Exclude(Components.Types<T1>.Id, Components.Types<T2>.Id);
        }

        public ComponentMatcher Exclude<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            return Exclude(Components.Types<T1>.Id, Components.Types<T2>.Id, Components.Types<T3>.Id);
        }

        public bool IsExclude(int type) {
            if (excludes != null) {
                for (var i = 0; i < excludes.Length; i++) {
                    if (excludes[i] == type) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsInclude(int type) {
            if (includes != null) {
                for (var i = 0; i < includes.Length; i++) {
                    if (includes[i] == type) {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class Matcher {

        public static ComponentMatcher Include<T>() where T : IComponent {
            var matcher = new ComponentMatcher();
            return matcher.Include<T>();
        }

        public static ComponentMatcher Include<T1, T2>() where T1 : IComponent where T2 : IComponent {
            var matcher = new ComponentMatcher();
            return matcher.Include<T1, T2>();
        }

        public static ComponentMatcher Include<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var matcher = new ComponentMatcher();
            return matcher.Include<T1, T2, T3>();
        }

        public static ComponentMatcher Exclude<T>() where T : IComponent {
            var matcher = new ComponentMatcher();
            return matcher.Exclude<T>();
        }

        public static ComponentMatcher Exclude<T1, T2>() where T1 : IComponent where T2 : IComponent {
            var matcher = new ComponentMatcher();
            return matcher.Exclude<T1, T2>();
        }

        public static ComponentMatcher Exclude<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var matcher = new ComponentMatcher();
            return matcher.Exclude<T1, T2, T3>();
        }
    }
}
