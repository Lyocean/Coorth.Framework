using System;
using System.Collections.Generic;
using System.Linq;

namespace Coorth.Framework; 

public class ArchetypeMatcher {
    
    private int include;
    private int[]? includes;
    public IReadOnlyList<int> Includes => includes ?? Array.Empty<int>();

    private int exclude;
    private int[]? excludes;
    public IReadOnlyList<int> Excludes => excludes ?? Array.Empty<int>();

    private int allType;
    private int[]? allTypes;
    public int[] AllTypes => allTypes ?? Array.Empty<int>();

    internal bool Match(ArchetypeDefinition archetype) {
        var components = archetype.Components;
        if (includes != null) {
            for (int i = 0, iMax = includes.Length; i < iMax; i++) {
                if (!components.Contains(includes[i])) {
                    return false;
                }
            }
        }
        if (excludes != null) {
            for (int i = 0, iMax = excludes.Length; i < iMax; i++) {
                if (components.Contains(excludes[i])) {
                    return false;
                }
            }
        }
        return true;
    }

    private static int[] Expand(int[]? array, int[] expand, ref int flag) {
        if (array == null) {
            array = new int[expand.Length];
            Array.Copy(expand, array, expand.Length);
        } else {
            array = array.Union(expand).ToArray();
        }

        foreach (var typeId in expand) {
            flag &= (1 << (typeId % 32));
        }
        return array;
    }

    private ArchetypeMatcher Include(params int[] types) {
        includes = Expand(includes, types, ref include);
        allTypes = Expand(AllTypes, includes, ref allType);
        return this;
    }

    private ArchetypeMatcher Exclude(params int[] types) {
        excludes = Expand(excludes, types, ref exclude);
        allTypes = Expand(AllTypes, excludes, ref allType);
        return this;
    }

    public ArchetypeMatcher Include<T>() where T : IComponent {
        return Include(ComponentType<T>.TypeId);
    }

    public ArchetypeMatcher Include<T1, T2>() where T1 : IComponent where T2 : IComponent {
        return Include(ComponentType<T1>.TypeId, ComponentType<T2>.TypeId);
    }

    public ArchetypeMatcher Include<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        return Include(ComponentType<T1>.TypeId, ComponentType<T2>.TypeId, ComponentType<T3>.TypeId);
    }

    public ArchetypeMatcher Exclude<T>() where T : IComponent {
        return Exclude(ComponentType<T>.TypeId);
    }

    public ArchetypeMatcher Exclude<T1, T2>() where T1 : IComponent where T2 : IComponent {
        return Exclude(ComponentType<T1>.TypeId, ComponentType<T2>.TypeId);
    }

    public ArchetypeMatcher Exclude<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        return Exclude(ComponentType<T1>.TypeId, ComponentType<T2>.TypeId, ComponentType<T3>.TypeId);
    }

    public bool IsExclude(int type) {
        if (excludes == null) {
            return false;
        }
        foreach (var t in excludes) {
            if (t == type) {
                return true;
            }
        }
        return false;
    }

    public bool IsInclude(int type) {
        if (includes == null) {
            return false;
        }
        for (var i = 0; i < includes.Length; i++) {
            if (includes[i] == type) {
                return true;
            }
        }
        return false;
    }
    
}
