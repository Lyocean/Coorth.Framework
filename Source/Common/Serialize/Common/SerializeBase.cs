using System;
using System.Collections.Generic;

namespace Coorth.Serialize;

public abstract class SerializeBase {
    
    private readonly Dictionary<Type, object> contexts = new Dictionary<Type, object>();

    protected string? error;

    public string? GetError() => error;

    public void SetContext<T>(T value) where T: notnull => contexts[typeof(T)] = value;

    public T GetContext<T>() => (T)contexts[typeof(T)];
}