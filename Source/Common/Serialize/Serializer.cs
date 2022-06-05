namespace Coorth.Serialize; 

public abstract class Serializer {
    public abstract void WriteObject(SerializeWriter writer, in object? value);
    public abstract object? ReadObject(SerializeReader reader, object? value);
}

public abstract class Serializer<T> : Serializer {
    
    public override void WriteObject(SerializeWriter writer, in object? value) {
        Write(writer, value != null ? (T)value : default);
    }
    
    public override object? ReadObject(SerializeReader reader, object? value) {
        var result = Read(reader, value != null ? (T)value : default);
        return result ?? default;
    }

    public abstract void Write(SerializeWriter writer, in T? value);
    public abstract T? Read(SerializeReader reader, T? value);
}