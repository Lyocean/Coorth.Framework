using System;
using System.Runtime.Serialization;

namespace Coorth.Serialize; 

public abstract class TextSerializeReader : BaseSerializeReader {
    protected override DateTime ReadDateTime() {
        var text = ReadString();
        return DateTime.Parse(text);
    }

    protected override TimeSpan ReadTimeSpan() {
        var text = ReadString();
        return TimeSpan.Parse(text);
    }

    protected override Guid ReadGuid() {
        var text = ReadString();
        return Guid.Parse(text);
    }

    protected override Type ReadType() {
        var text = ReadString();
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

    protected override T ReadEnum<T>() {
        var text = ReadString();
        return (T)Enum.Parse(typeof(T), text);
    }
}