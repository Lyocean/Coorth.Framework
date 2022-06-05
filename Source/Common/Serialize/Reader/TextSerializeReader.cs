using System;
using System.Runtime.Serialization;

namespace Coorth.Serialize; 

public abstract class TextSerializeReader : SerializeReader {
    public override DateTime ReadDateTime() {
        var text = ReadString();
        if (text == null) {
            throw new SerializationException();
        }
        return DateTime.Parse(text);
    }

    public override TimeSpan ReadTimeSpan() {
        var text = ReadString();
        if (text == null) {
            throw new SerializationException();
        }
        return TimeSpan.Parse(text);
    }

    public override Guid ReadGuid() {
        var text = ReadString();
        if (text == null) {
            throw new SerializationException();
        }
        return Guid.Parse(text);
    }

    public override Type ReadType() {
        var text = ReadString();
        if (text == null) {
            throw new SerializationException();
        }
        var index = text.IndexOf(":", StringComparison.Ordinal);
        var content = text[(index + 1)..];
        if (text.StartsWith("Guid:")) {
            var type = TypeBinding.GetType(content);
            if (type != null) {
                return type;
            }
        }
        if(text.StartsWith("Name:")) {
            var type = Type.GetType(content);
            if (type != null) {
                return type;
            }
        }
        throw new SerializationException($"[Serialize]: Unexpected type format: {text}.");
    }

    public override T ReadEnum<T>() {
        var text = ReadString();
        if (text == null) {
            throw new SerializationException();
        }
        return (T)Enum.Parse(typeof(T), text);
    }
}