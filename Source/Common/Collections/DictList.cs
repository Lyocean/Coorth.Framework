using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Coorth.Collections; 

public readonly struct DictList<TKey, TValue> where TKey : notnull {
        
    private readonly List<TValue> list;
        
    private readonly Dictionary<TKey, int> dict;

    public IReadOnlyList<TValue> List => list;
        
    public int Count => list.Count;
        
    public DictList(int capacity) {
        if (capacity == 0) {
            list = new List<TValue>();
            dict = new Dictionary<TKey, int>();
        }
        else {
            list = new List<TValue>(capacity);
            dict = new Dictionary<TKey, int>(capacity);
        }
    }
        
    public void Add(TKey key, TValue item) {
        var index = list.Count;
        list.Add(item);
        dict.Add(key, index);
    }
        
    public TValue Get(TKey key) {
        return list[dict[key]];
    }

    public bool TryGet(TKey key, [MaybeNullWhen(false)]out TValue value) {
        if (dict.TryGetValue(key, out int index)) {
            value = list[index];
            return true;
        }
        value = default;
        return false;
    }

    public bool Remove(TKey key) {
        if (dict.TryGetValue(key, out int index)) {
            list.RemoveAt(index);
            return true;
        }
        return false;
    }

    public void Clear() {
        list.Clear();
        dict.Clear();
    }
}