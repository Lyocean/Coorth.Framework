using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Coorth.Collections;

public class Blackboard<TKey> where TKey : notnull {
    
    private readonly Dictionary<Type, IGroup> groups = new();
    public IReadOnlyDictionary<Type, IGroup> Groups => groups;

    private readonly Dictionary<TKey, int> indices = new();
    public IReadOnlyDictionary<TKey, int> Indices => indices;

    public int Count => indices.Count;

    public int GetCount<TValue>() where TValue : notnull {
        if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
            return 0;
        }

        var group = (Group<TValue>)valueGroup;
        return group.Count;
    }

    public bool Has<TValue>(TKey key) where TValue : notnull {
        return indices.ContainsKey(key) && groups.ContainsKey(typeof(TValue));
    }

    public TValue Get<TValue>(TKey key) where TValue : notnull {
        if (!indices.TryGetValue(key, out var index)) {
            throw new KeyNotFoundException($"index:{index}");
        }

        if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
            throw new KeyNotFoundException($"type:{typeof(TValue)}");
        }

        var group = (Group<TValue>)valueGroup;
        return group.Get(index);
    }

    public ref TValue Ref<TValue>(TKey key) where TValue : notnull {
        if (!indices.TryGetValue(key, out var index)) {
            throw new KeyNotFoundException($"index:{index}");
        }
        if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
            throw new KeyNotFoundException($"type:{typeof(TValue)}");
        }
        var group = (Group<TValue>)valueGroup;
        return ref group.Ref(index);
    }

    public bool TryGet<TValue>(TKey key, [NotNullWhen(true), MaybeNullWhen(false)] out TValue value)
        where TValue : notnull {
        if (!indices.TryGetValue(key, out var index)) {
            value = default;
            return false;
        }
        if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
            value = default;
            return false;
        }
        var group = (Group<TValue>)valueGroup;
        value = group.Get(index);
        return true;
    }

    public void Set<TValue>(TKey key, TValue value) where TValue : notnull {
        Group<TValue> group;
        if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
            group = new Group<TValue>();
            groups[typeof(TValue)] = group;
        }
        else {
            group = (Group<TValue>)valueGroup;
        }

        var index = group.Add(value);
        indices.Add(key, index);
    }

    public bool Remove<TValue>(TKey key) where TValue : notnull {
        if (!indices.TryGetValue(key, out var index)) {
            return false;
        }

        if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
            return false;
        }

        var group = (Group<TValue>)valueGroup;
        return group.Remove(index);
    }

    public void Clear() {
        groups.Clear();
        indices.Clear();
    }

    public interface IGroup {
        int Count { get; }
    }

    private sealed class Group<TValue> : IGroup where TValue : notnull {
        private ValueList<TValue> values = new(1);

        private ValueList<int> holes;

        public int Count => values.Count - holes.Count;

        public TValue Get(int index) => values.Get(index);

        public ref TValue Ref(int index) => ref values.Ref(index);

        public int Add(TValue value) {
            if (!holes.IsNull && holes.Count > 0) {
                var index = holes[^1];
                holes.RemoveLast();
                values[index] = value;
                return index;
            }
            else {
                var index = values.Count;
                values.Add(value);
                return index;
            }
        }

        public bool Remove(int index) {
            if (index >= values.Count) {
                return false;
            }
            if (index == values.Count - 1) {
                values.RemoveLast();
                return true;
            }
            holes.Add(index);
            values[index] = default!;
            return true;
        }
    }
}