using System;

namespace Coorth.Serialize; 

public abstract partial class SerializeReader : SerializeBase {
    //Root
    public abstract void BeginRoot(Type type);
    public abstract void EndRoot();
    //Scope
    public abstract void BeginScope(Type type, SerializeScope scope);
    public abstract void EndScope();
    //List
    public abstract void BeginList(Type item, out int count);
    public abstract bool EndList();
    //Dict
    public abstract void BeginDict(Type key, Type value, out int count);
    public abstract bool EndDict();
    //Tag, Key, Value
    public abstract int ReadTag(string name, int index);
    
    // T? ReadField<T>(string name, int index);
    
    public virtual TKey ReadKey<TKey>() where TKey: notnull {   
        var type = typeof(TKey);
        if (type.IsEnum) {
            return ReadEnum<TKey>();                
        }
        if (type.IsPrimitive) {
            var serializer = Serializers.GetSerializer<TKey>();
            if (serializer != null) {
                var key = serializer.Read(this, default);
                return key ?? throw new NotSupportedException(type.ToString());
            }
        } 
        throw new NotSupportedException(type.ToString());
    }
    
    public T? ReadValue<T>() {
        if (typeof(T).IsEnum) {
            return ReadEnum<T>();
        }
        var serializer = Serializers.GetSerializer<T>();
        if (serializer != null) {
            return serializer.Read(this, default);
        }
        throw new NotSupportedException(typeof(T).ToString());
    }

    public virtual object? ReadObject(Type type) {
        var serializer = Serializers.GetSerializer(type);
        if (serializer != null) {
            return serializer.ReadObject(this, default);
        }
        throw new NotSupportedException(type.ToString());
    }

    // Types
    
    public abstract bool ReadBool();

    public abstract sbyte ReadInt8();

    public abstract short ReadInt16();

    public abstract int ReadInt32();

    public abstract long ReadInt64();

    public abstract byte ReadUInt8();

    public abstract ushort ReadUInt16();
    
    public abstract uint ReadUInt32();

    public abstract ulong ReadUInt64();

#if NET5_0_OR_GREATER
    public virtual Half ReadFloat16() => (Half) ReadFloat32();
#endif

    public abstract float ReadFloat32();

    public abstract double ReadFloat64();

    public abstract decimal ReadDecimal();
    
    public abstract char ReadChar();

    public abstract string? ReadString();

    public abstract DateTime ReadDateTime();

    public abstract TimeSpan ReadTimeSpan();

    public abstract Guid ReadGuid();

    public abstract Type ReadType();

    public abstract T ReadEnum<T>();
}