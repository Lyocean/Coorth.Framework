using System;
using System.Collections.Generic;

namespace Coorth.Serialize; 

public abstract class BaseSerializeReader : SerializeBase, ISerializeReader {
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

    //Simple Type
    protected abstract bool ReadBool();

    protected abstract byte ReadByte();

    protected abstract sbyte ReadSByte();

    protected abstract short ReadShort();

    protected abstract ushort ReadUShort();

    protected abstract int ReadInt();

    protected abstract uint ReadUInt();

    protected abstract long ReadLong();

    protected abstract ulong ReadULong();

    protected abstract float ReadFloat();

    protected abstract double ReadDouble();

    protected abstract char ReadChar();

    protected abstract string ReadString();

    protected abstract DateTime ReadDateTime();

    protected abstract TimeSpan ReadTimeSpan();

    protected abstract Guid ReadGuid();

    protected abstract Type ReadType();

    protected abstract T ReadEnum<T>();

}