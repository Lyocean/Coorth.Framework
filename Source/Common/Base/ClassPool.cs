using System.Collections.Concurrent;

namespace Coorth;

public static class ClassPool {
    private static class Pool<T> where T: class {
        public static readonly ConcurrentQueue<T> Items = new ConcurrentQueue<T>();
    }

    public static T? Create<T>() where T: class => Pool<T>.Items.TryDequeue(out var item) ? item : default;

    public static void Return<T>(T item) where T: class => Pool<T>.Items.Enqueue(item);
        
    public static void Create<T>(ref T? value) where T: class {
        if (value != null) {
            return;
        }
        value = Create<T>();
    }
        
    public static void Return<T>(ref T? value) where T: class {
        if (value == null) {
            return;
        }
        Return(value);
        value = null;
    }
}