using System;
using System.Collections.Generic;

namespace Coorth {
    public interface IBlackboard<in TKey> {
        bool Has<TValue>(TKey key);
        TValue Get<TValue>(TKey key);
        ref TValue Ref<TValue>(TKey key);
        bool TryGet<TValue>(TKey key, out TValue value);
        void Set<TValue>(TKey key, TValue value);
        bool Remove<TValue>(TKey key);
    }
    
    public class Blackboard<TKey> : IBlackboard<TKey> {

        private readonly Dictionary<Type, IGroup> groups = new Dictionary<Type, IGroup>();

        private readonly Dictionary<TKey, int> indices = new Dictionary<TKey, int>();

        public IReadOnlyDictionary<TKey, int> Indices => indices;

        public int Count => indices.Count;
        
        public int GetCount<TValue>() {
            if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
                return 0;
            }
            var group = (Group<TValue>) valueGroup;
            return group.Count;
        }
        
        public bool Has<TValue>(TKey key) {
            return indices.ContainsKey(key) && groups.ContainsKey(typeof(TValue));
        }
        
        public TValue Get<TValue>(TKey key) {
            if (!indices.TryGetValue(key, out var index)) {
                throw new KeyNotFoundException($"index:{index}");
            }
            if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
                throw new KeyNotFoundException($"type:{typeof(TValue)}");
            }
            var group = (Group<TValue>) valueGroup;
            return group.Get(index);
        }
        
        public ref TValue Ref<TValue>(TKey key) {
            if (!indices.TryGetValue(key, out var index)) {
                throw new KeyNotFoundException($"index:{index}");
            }
            if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
                throw new KeyNotFoundException($"type:{typeof(TValue)}");
            }
            var group = (Group<TValue>) valueGroup;
            return ref group.Ref(index);
        }
        
        public bool TryGet<TValue>(TKey key, out TValue value) {
            if (!indices.TryGetValue(key, out var index)) {
                value = default;
                return false;
            }
            if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
                value = default;
                return false;
            }
            var group = (Group<TValue>) valueGroup;
            value = group.Get(index);
            return true;
        }
        
        public void Set<TValue>(TKey key, TValue value) {
            Group<TValue> group;
            if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
                group = new Group<TValue>();
                groups[typeof(TValue)] = group;
            }
            else {
                group = (Group<TValue>) valueGroup;
            }
            var index = group.Add(value);
            indices.Add(key, index);
        }

        public bool Remove<TValue>(TKey key) {
            if (!indices.TryGetValue(key, out var index)) {
                return false;
            }
            if (!groups.TryGetValue(typeof(TValue), out var valueGroup)) {
                return false; 
            }
            var group = (Group<TValue>) valueGroup;
            return group.Remove(index);
        }

        public void Clear() {
            groups.Clear();
            indices.Clear();
        }

        private interface IGroup {
            
        }
        
        public class Group<TValue> : IGroup {
            
            private RawList<TValue> values = new RawList<TValue>(1);
            
            private RawList<int> holes = new RawList<int>(1);

            public int Count => values.Count - holes.Count;
            
            public TValue Get(int index) {
                return values.Get(index);
            }
            
            public ref TValue Ref(int index) {
                return ref values.Ref(index);
            }

            public int Add(TValue value) {
                if (holes.Count > 0) {
                    var index = holes[holes.Count - 1];
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
                values[index] = default;
                return true;
            }
        }
    }
}