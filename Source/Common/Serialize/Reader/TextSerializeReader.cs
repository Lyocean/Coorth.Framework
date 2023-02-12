using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Coorth.Serialize; 

public abstract class TextSerializeReader : SerializeReader {

    protected abstract ReadOnlySpan<char> ReadChars();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DateTime ReadDateTime() => DateTime.Parse(ReadChars());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override TimeSpan ReadTimeSpan() => TimeSpan.Parse(ReadChars());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Guid ReadGuid() => Guid.Parse(ReadChars());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Type ReadType() {
        var text = ReadChars();
        var head = text[..5];
        if (head == "Guid:") {
            var guid = Guid.Parse(text[6..]);
            var type = TypeBinding.GetType(guid);
            if (type != null) {
                return type;
            }
        }
        else if (head == "Name:") {
            var name = new string(text[6..]);
            var type = TypeBinding.GetType(name);
            if (type != null) {
                return type;
            }
        }
        throw new SerializationException($"[Serialize]: Unexpected type format: {text.ToString()}.");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T ReadEnum<T>() {
#if NET5_0_OR_GREATER
        var text = ReadChars();
        return (T)Enum.Parse(typeof(T), text);
#else
        var text = ReadString();
        if (text == null) {
            throw new SerializationException();
        }
        return (T)Enum.Parse(typeof(T), text);
#endif
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override object ReadEnum(Type type) {
#if NET5_0_OR_GREATER
        var text = ReadChars();
        return Enum.Parse(type, text);
#else
        var text = ReadString();
        if (text == null) {
            throw new SerializationException();
        }
        return Enum.Parse(type, text);
#endif
    }
}