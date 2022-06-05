using System;

namespace Coorth.Serialize; 

public abstract partial class SerializeWriter : SerializeBase {
    
    //Root
    public abstract void BeginRoot(Type type);
    public abstract void EndRoot();
    
    //Scope
    public abstract void BeginScope(Type type, SerializeScope scope);
    public abstract void EndScope();
    
    //List
    public abstract void BeginList(Type item, int count);
    public abstract void EndList();
    
    //Dict
    public abstract void BeginDict(Type key, Type value, int count);
    public abstract void EndDict();
    
    //Tag, Key, Value
    public abstract void WriteTag(string name, int index);
    public virtual void WriteKey<T>(in T key) where T : notnull {
        var type = typeof(T);
        if (type.IsPrimitive) {
            var serializer = Serializers.GetSerializer<T>();
            if (serializer != null) {
                serializer.Write(this, key);
                return;
            }
        } else if (type.IsEnum) {
            WriteEnum(key);
            return;
        } else if (key is Type t) {
            WriteType(t);
            return;
        }
        throw new NotSupportedException(type.ToString());
    }
    public virtual void WriteValue<T>(in T? value) {
        if (typeof(T).IsEnum) {
            WriteEnum(value);
            return;
        }
        var serializer = Serializers.GetSerializer<T>();
        if (serializer != null) {
            serializer.Write(this, value);
            return;
        }
        throw new NotSupportedException(typeof(T).ToString());
    }
    
    public virtual void WriteObject(Type type, object? obj) {
        if (type.IsEnum) {
            WriteEnum(type, obj);
            return;
        }
        var serializer = Serializers.GetSerializer(type);
        if (serializer != null) {
            serializer.WriteObject(this, obj);
            return;
        }
        throw new NotSupportedException(type.ToString());
    }
    
    public abstract void WriteBool(bool value);

    public abstract void WriteInt8(sbyte value);

    public abstract void WriteInt16(short value);

    public abstract void WriteInt32(int value);
    
    public abstract void WriteInt64(long value);

    public abstract void WriteUInt8(byte value);

    public abstract void WriteUInt16(ushort value);

    public abstract void WriteUInt32(uint value);

    public abstract void WriteUInt64(ulong value);

#if NET5_0_OR_GREATER
    public virtual void WriteFloat16(Half value) => WriteFloat32((float)value);
#endif

    public abstract void WriteFloat32(float value);

    public abstract void WriteFloat64(double value);

    public abstract void WriteDecimal(Decimal value);

    public abstract void WriteChar(char value);

    public abstract void WriteString(string? value);

    public abstract void WriteDateTime(DateTime value);

    public abstract void WriteTimeSpan(TimeSpan value);

    public abstract void WriteGuid(Guid value);

    public abstract void WriteType(Type value);

    public abstract void WriteEnum<T>(T value);
    
    public abstract void WriteEnum(Type type, object? value);
}