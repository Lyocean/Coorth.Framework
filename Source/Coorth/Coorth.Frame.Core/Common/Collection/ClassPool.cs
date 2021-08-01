using System.Collections.Concurrent;

namespace Coorth {
    public static class ClassPool<T> where T: class {
        
        private static readonly ConcurrentQueue<T> items = new ConcurrentQueue<T>();

        public static T Get() => items.TryDequeue(out var item) ? item : default;

        public static void Put(T item)  => items.Enqueue(item);
        
        public static void Get(ref T value) {
            if (value != null) {
                return;
            }
            value = Get();
        }
        
        public static void Put(ref T value) {
            if (value == null) {
                return;
            }
            Put(value);
            value = null;
        }
    }}