using System;

namespace Coorth; 

public class Box<T> : IDisposable {
    
    public T? Value;

    private Box(T value) { Value = value; }

    public static Box<T> Create(in T value) {
        var box = ClassPool.Create<Box<T>>() ?? new Box<T>(value);
        box.Value = value;
        return box;
    }

    public void Dispose() {
        Value = default;
        ClassPool.Return(this);
    }
}