namespace Coorth.Serialize;

public abstract class Serializer {
    public abstract void WriteObject(SerializeWriter writer, in object value);
    public abstract object? ReadObject(SerializeReader reader, object? value);
}

public abstract class Serializer<T> : Serializer {
    
    public override void WriteObject(SerializeWriter writer, in object value) {
        Write(writer, (T)value);
    }
    
    public override object? ReadObject(SerializeReader reader, object? value) {
        var result = Read(reader, value != null ? (T)value : default);
        return result ?? default;
    }

    public abstract void Write(SerializeWriter writer, in T value);
    public abstract T? Read(SerializeReader reader, T? value);
}

public abstract class ScopeSerializer<T> : Serializer<T> {
    public override void Write(SerializeWriter writer, in T value) {
        var type = typeof(T);
        writer.BeginScope(type);
        if (value != null) {
            OnWrite(writer, in value);
        }
        writer.EndScope();
    }

    protected abstract void OnWrite(SerializeWriter writer, in T value);

    public override T Read(SerializeReader reader, T? value) {
        reader.BeginScope(typeof(T), SerializeScope.Table);
        OnRead(reader, ref value);
        reader.EndScope();
        return value;
    }

    protected abstract void OnRead(SerializeReader reader, ref T value);
}