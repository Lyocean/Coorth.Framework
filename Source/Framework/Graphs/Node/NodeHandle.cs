using System;

namespace Coorth.Framework;

public readonly record struct NodeHandle(Type Type, Guid Id) {
    
    public readonly Type Type = Type;
    
    public readonly Guid Id = Id;

    public NodeHandle<T> Cast<T>() {
        if (!typeof(T).IsAssignableFrom(Type)) {
            throw new InvalidCastException();
        }
        return new NodeHandle<T>(Id);
    }
}

public readonly record struct NodeHandle<T>(Guid Id) {

    public readonly Type Type => typeof(T);
    
    public readonly Guid Id = Id;

    public NodeHandle Cast() => new(typeof(T), Id);

    public static implicit operator NodeHandle(NodeHandle<T> handle) => handle.Cast();

    public static implicit operator NodeHandle<T>(NodeHandle handle) => handle.Cast<T>();
}
