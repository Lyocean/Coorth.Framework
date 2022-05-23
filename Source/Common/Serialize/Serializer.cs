namespace Coorth.Serialize; 

public abstract class Serializer {
    public abstract void WriteObject(ISerializeWriter writer, in object? value);
    public abstract object? ReadObject(ISerializeReader reader, object? value);
}

public abstract class Serializer<T> : Serializer {
    public override void WriteObject(ISerializeWriter writer, in object? value) {
        Write(writer, value != null ? (T)value : default);
    }
    
    public override object? ReadObject(ISerializeReader reader, object? value) {
        var result = Read(reader, value != null ? (T)value : default);
        return result ?? default;
    }

    public abstract void Write(ISerializeWriter writer, in T? value);
    public abstract T? Read(ISerializeReader reader, T? value);
}