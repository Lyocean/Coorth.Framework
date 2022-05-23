using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth.Serialize; 

public abstract class BaseSerializeWriter : SerializeBase, ISerializeWriter {
        
    //Root
    public abstract void BeginRoot(Type type);
    public abstract void EndRoot();
    //Scope
    public abstract void BeginScope(Type type, SerializeScope scope);
    public abstract void EndScope();
    //List
    public abstract void BeginList(Type type, int count);
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
        if (value is null) {
            WriteNull();
            return;
        }
        if (typeof(T).IsEnum) {
            WriteEnum(value);
            return;
        }
        if(WriteInternal(value)){
            return;
        }
        var serializer = Serializers.GetSerializer<T>();
        if (serializer != null) {
            serializer.Write(this, value);
            return;
        }
        throw new NotSupportedException(typeof(T).ToString());
    }
    
    private bool WriteInternal<T>(in T? value) {
        switch (value) {
            case null:
                WriteNull();
                return true;
            case bool v:
                WriteBool(v);
                return true;
            case byte v:
                WriteByte(v);
                return true;
            case sbyte v:
                WriteSByte(v);
                return true;
            case short v:
                WriteShort(v);
                return true;
            case ushort v:
                WriteUShort(v);
                return true;
            case int v:
                WriteInt(v);
                return true;
            case uint v:
                WriteUInt(v);
                return true;
            case long v:
                WriteLong(v);
                return true;
            case ulong v:
                WriteULong(v);
                return true;
            case float v:
                WriteFloat(v);
                return true;
            case double v:
                WriteDouble(v);
                return true;
            case char v:
                WriteChar(v);
                return true;
            case string v:
                WriteString(v);
                return true;
            case Type v:
                WriteType(v);
                return true;
            case DateTime v:
                WriteDateTime(v);
                return true;
            case TimeSpan v:
                WriteTimeSpan(v);
                return true;
            case Guid v:
                WriteGuid(v);
                return true;
        }
        return false;
    }

    //Simple Type
    protected abstract void WriteNull();

    protected abstract void WriteBool(bool value);

    protected abstract void WriteByte(byte value);
        
    protected abstract void WriteSByte(sbyte value);
        
    protected abstract void WriteShort(short value);
        
    protected abstract void WriteUShort(ushort value);
        
    protected abstract void WriteInt(int value);
        
    protected abstract void WriteUInt(uint value);
        
    protected abstract void WriteLong(long value);
        
    protected abstract void WriteULong(ulong value);
        
    protected abstract void WriteFloat(float value);
        
    protected abstract void WriteDouble(double value);
        
    protected abstract void WriteChar(char value);
        
    protected abstract void WriteString(string value);

    protected abstract void WriteDateTime(DateTime value);

    protected abstract void WriteTimeSpan(TimeSpan value);

    protected abstract void WriteGuid(Guid value);

    protected abstract void WriteType(Type value);

    protected abstract void WriteEnum<T>(T value);
}