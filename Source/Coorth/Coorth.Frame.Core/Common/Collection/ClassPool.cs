using System.Collections.Concurrent;

namespace Coorth {
    public static class ClassPool<T> where T: class {
        
        private static readonly ConcurrentQueue<T> items = new ConcurrentQueue<T>();

        public static T Create() => items.TryDequeue(out var item) ? item : default;

        public static void Return(T item)  => items.Enqueue(item);
        
        public static void Create(ref T value) {
            if (value != null) {
                return;
            }
            value = Create();
        }
        
        public static void Return(ref T value) {
            if (value == null) {
                return;
            }
            Return(value);
            value = null;
        }
    }}