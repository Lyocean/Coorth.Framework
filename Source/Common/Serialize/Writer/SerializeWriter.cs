using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Coorth.Serialize;

public interface ISerializeWriter {
    
    #region Scope

    void BeginData(Type type, short count, byte flags = SerializeConst.DATA);
    void BeginData<T>(short count, byte flags = SerializeConst.DATA);
    void EndData();

    #endregion

    #region List

    void BeginList(Type item, int count);
    void BeginList<T>(int count);
    void EndList();

    void WriteList<T>(in IList<T>? list);
    
    #endregion

    #region Dict

    void BeginDict(Type key, Type value, int count);
    void BeginDict<TKey, TValue>(int count);
    void EndDict();

    void WriteDict<TK, TV>(IDictionary<TK, TV>? dict) where TK : notnull;
    
    #endregion

    #region Tag, Key & Value

    void WriteTag(string name, int index);

    void WriteKey<T>(in T key) where T: notnull;
    void WriteKey(Type type, in object key);

    void WriteValue<T>(in T? value);
    void WriteValue(Type type, in object? value);

    void WriteField<T>(string name, int index, in T? value);
    void WriteField(string name, int index, Type type, in object? value);

    #endregion

    #region Primitive

    void WriteBytes(ReadOnlySpan<byte> span);
    
    void WriteNull();
    
    void WriteBool(bool value);

    void WriteInt8(sbyte value);

    void WriteInt16(short value);

    void WriteInt32(int value);
    
    void WriteInt64(long value);

    void WriteUInt8(byte value);

    void WriteUInt16(ushort value);

    void WriteUInt32(uint value);

    void WriteUInt64(ulong value);

#if NET5_0_OR_GREATER
    void WriteFloat16(Half value);
#endif

    void WriteFloat32(float value);

    void WriteFloat64(double value);

    void WriteDecimal(Decimal value);

    void WriteChar(char value);

    void WriteString(string? value);

    void WriteDateTime(DateTime value);

    void WriteTimeSpan(TimeSpan value);

    void WriteGuid(Guid value);

    void WriteType(Type value);

    void WriteEnum<T>(T value) where T: notnull;
    
    void WriteEnum(Type type, object value);

    #endregion

}


public abstract class SerializeWriter : ISerializeWriter {

    #region Scope
    
    public abstract void BeginData(Type type, short count, byte flags = SerializeConst.DATA);
    
    public abstract void EndData();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void BeginData<T>(short count, byte flags = SerializeConst.DATA) => BeginData(typeof(T), count, flags);

    #endregion

    #region List
    
    public abstract void BeginList(Type item, int count);
    
    public abstract void EndList();
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void BeginList<T>(int count) => BeginList(typeof(T), count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteList<T>(in IList<T>? list) {
        if (list == null) {
            WriteNull();
            return;
        }
        BeginList<T>(list.Count);
        foreach (var value in list) {
            WriteValue(value);
        }
        EndList();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteList<T>(IReadOnlyList<T>? list) {
        if (list == null) {
            WriteNull();
            return;
        }
        BeginList<T>(list.Count);
        foreach (var value in list) {
            WriteValue(value);
        }
        EndList();
    }
    
    #endregion

    #region Dict
    
    public abstract void BeginDict(Type key, Type value, int count);
    public abstract void EndDict();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void BeginDict<TKey, TValue>(int count) => BeginDict(typeof(TKey), typeof(TValue), count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteDict<TK, TV>(IDictionary<TK, TV>? dict) where TK : notnull {
        if(dict == null){
            WriteNull();
            return;
        }
        BeginDict<TK, TV>(dict.Count);
        foreach (var pair in dict) {
            WriteKey(pair.Key);
            WriteValue(pair.Value);
        }
        EndDict();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteDict<TK, TV>(IReadOnlyDictionary<TK, TV>? dict) where TK : notnull {
        if(dict == null){
            WriteNull();
            return;
        }
        BeginDict<TK, TV>(dict.Count);
        foreach (var pair in dict) {
            WriteKey(pair.Key);
            WriteValue(pair.Value);
        }
        EndDict();
    }
    
    #endregion

    #region Tag, Key, Value
    
    public abstract void WriteTag(string name, int index);
    
    public void WriteKey(Type type, in object key) {
        var formatter = SerializeFormatter.GetFormatter(type);
        if (formatter != null) {
            formatter.SerializeWriting(this, in key);
            return;
        }
        if (type.IsEnum) {
            WriteEnum(key);
            return;
        }
        throw new NotSupportedException(type.ToString());
    }
    
    public void WriteKey<T>(in T key) where T : notnull {
        var formatter = SerializeFormatter.GetFormatter<T>();
        if (formatter != null) {
            formatter.SerializeWriting(this, in key);
            return;
        }
        var type = typeof(T);
        if (type.IsEnum) {
            WriteEnum(key);
            return;
        }
        throw new NotSupportedException(type.ToString());
    }
    
    public void WriteValue<T>(scoped in T? value) {
        if (value == null) {
            WriteNull();
            return;
        }
        var formatter = SerializeFormatter.GetFormatter<T>();
        if (formatter != null) {
            formatter.SerializeWriting(this, in value);
            return;
        }
        if (value is ISerializable serializable) {
            serializable.SerializeWriting(this);
            return;
        }
        var type = typeof(T);
        if (type.IsEnum) {
#pragma warning disable CS8714
            WriteEnum<T>(value);
#pragma warning restore CS8714
            return;
        }
        throw new NotSupportedException(typeof(T).ToString());
    }

    public void WriteValue(Type type, in object? value) {
        if (value == null) {
            WriteNull();
            return;
        }
        var formatter = SerializeFormatter.GetFormatter(type);
        if (formatter != null) {
            formatter.SerializeWriting(this, in value);
            return;
        }
        if (value is ISerializable serializable) {
            serializable.SerializeWriting(this);
            return;
        }
        if (type.IsEnum) {
            WriteEnum(value);
            return;
        }
        throw new NotSupportedException(type.ToString());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteField(string name, int index, Type type, in object? value) {
        WriteTag(name, index);
        WriteValue(type, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteField<T>(string name, int index, in T? value) {
        WriteTag(name, index);
        WriteValue(value);
    }
    
    #endregion

    #region Primitive

    public abstract void WriteBytes(ReadOnlySpan<byte> span);
    
    public abstract void WriteNull();
    
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

    public abstract void WriteEnum<T>(T value) where T: notnull;
    
    public abstract void WriteEnum(Type type, object value);

    #endregion

}