using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Coorth.Serialize;

public interface ISerializeReader {
    
    #region Scope

    void BeginData(Type type);
    void BeginData<T>();
    void EndData();

    #endregion

    #region List

    void BeginList(Type item, out int count);
    void BeginList<T>(out int count);
    bool EndList();
    
    #endregion

    #region Dict

    void BeginDict(Type key, Type value, out int count);
    void BeginDict<TKey, TValue>(out int count);
    void EndDict();
    
    #endregion

    #region Tag, Key & Value

    int ReadTag(string name, int index);

    void ReadKey<T>(out T value) where T : notnull;
    T ReadKey<T>() where T : notnull;
    void ReadKey(Type type, out object key);
    object ReadKey(Type type);
    
    void ReadValue<T>(ref T? value);
    T? ReadValue<T>();
    void ReadValue(Type type, out object? value);
    object? ReadValue(Type type);

    T? ReadField<T>(string name, int index);
    
    #endregion

    #region Primitive

    bool ReadNull();
    
    bool ReadBool();

    sbyte ReadInt8();

    short ReadInt16();

    int ReadInt32();

    long ReadInt64();

    byte ReadUInt8();

    ushort ReadUInt16();
    
    uint ReadUInt32();

    ulong ReadUInt64();

#if NET5_0_OR_GREATER
    Half ReadFloat16();
#endif

    float ReadFloat32();

    double ReadFloat64();

    decimal ReadDecimal();
    
    char ReadChar();

    string? ReadString();

    DateTime ReadDateTime();

    TimeSpan ReadTimeSpan();

    Guid ReadGuid();

    Type ReadType();

    T ReadEnum<T>() where T: notnull;

    object ReadEnum(Type type);

    #endregion

}

public abstract class SerializeReader : ISerializeReader {
    
    #region Scope

    public abstract void BeginData(Type type);
    public abstract void EndData();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void BeginData<T>() => BeginData(typeof(T));

    #endregion
    
    #region List
    
    public abstract void BeginList(Type item, out int count);
    public abstract bool EndList();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void BeginList<T>(out int count) => BeginList(typeof(T), out count);
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public List<T?>? ReadList<T>() {
        List<T?>? list = null;
        ReadList(ref list);
        return list;
    }

    public void ReadList<T>(scoped ref List<T?>? list) {
        if (ReadNull()) {
            list = null;
            return;
        }
        BeginList<T>(out var count);
        if (list == null) {
            list = count >=0 ? new List<T?>(count) : new List<T?>();
        } else {
            list.Clear();
        }
        if (count >= 0) {
            for (var i = 0; i < count; i++) {
                var value = ReadValue<T>();
                list.Add(value);
            }  
            EndList();
        }
        else {
            while (!EndList()) {
                var value = ReadValue<T>();
                list.Add(value);
            }
        }
    }
    
    public void ReadList<T>(ref T?[]? array) {
        BeginList<T>(out var count);
        if (count >= 0) {
            if (array == null || array.Length != count) {
                array = new T?[count];
            }
            for (var i = 0; i < count; i++) {
                var value = ReadValue<T>();
                array[i] = value;
            }
            EndList();
        }
        else {
            var list = new List<T?>();
            while (!EndList()) {
                var value = ReadValue<T>();
                list.Add(value);
            }
            array = list.ToArray();
        }
    }

    #endregion

    #region Dict

    public abstract void BeginDict(Type key, Type value, out int count);
    public abstract bool EndDict();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void BeginDict<TKey, TValue>(out int count) => BeginDict(typeof(TKey), typeof(TValue), out count);

    void ISerializeReader.EndDict() {
        throw new NotImplementedException();
    }

    public void ReadDict<TK, TV>(ref Dictionary<TK, TV?>? dict) where TK : notnull {
        if (ReadNull()) {
            dict = null;
            return;
        }
        BeginDict<TK, TV>(out var count);
        if (dict == null) {
            dict = count > 0 ? new Dictionary<TK, TV?>(count) : new Dictionary<TK, TV?>();
        } else {
            dict.Clear();
        }
        if (count >= 0) {
            for (var i = 0; i < count; i++) {
                var key = ReadKey<TK>();
                var value = ReadValue<TV>();
                dict.Add(key, value);
            }
            EndList();
        }
        else {
            while (!EndDict()) {
                var key = ReadKey<TK>();
                var value = ReadValue<TV>();
                dict.Add(key, value);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Dictionary<TK, TV?>? ReadDict<TK, TV>() where TK : notnull {
        Dictionary<TK, TV?>? dict = null;
        ReadDict(ref dict);
        return dict;
    }
    #endregion

    #region Tag, Key, Value

    //Tag, Key, Value
    public abstract int ReadTag(string name, int index);
    
    public void ReadKey<T>(out T value) where T : notnull {
        value = ReadKey<T>();
    }

    public T ReadKey<T>() where T: notnull {
        var formatter = SerializeFormatter.GetFormatter<T>();
        if (formatter != null) {
            T? key = default;
            formatter.SerializeReading(this, ref key);
            if (key == null) {
                throw new SerializationException();
            }
            return key;
        }
        var type = typeof(T);
        if (type.IsEnum) {
            return ReadEnum<T>();                
        }
        throw new NotSupportedException(type.ToString());
    }

    public void ReadKey(Type type, out object key) {
        key = ReadKey(type);
    }

    public object ReadKey(Type type) {   
        var formatter = SerializeFormatter.GetFormatter(type);
        if (formatter != null) {
            object? key = default; 
            formatter.SerializeReading(this, ref key!);
            return key;
        }
        if (type.IsEnum) {
            return ReadEnum(type);                
        }
        throw new NotSupportedException(type.ToString());
    }

    // public T? ReadValue<T>(ref T? value) {
    //     
    // }
    //
    public T ReadValue<T>() {
        if (ReadNull()) {
            return default!;
        }
        var formatter = SerializeFormatter.GetFormatter<T>();
        if (formatter != null) {
            T? value = default;
            formatter.SerializeReading(this, ref value!);
            return value;
        }
        var type = typeof(T);
        if (type.IsAssignableFrom(typeof(ISerializable))) {
            var value = Activator.CreateInstance<T>();
            (value as ISerializable)?.SerializeReading(this);
            return value;
        }
        if (type.IsEnum) {
#pragma warning disable CS8714
            return ReadEnum<T>();
#pragma warning restore CS8714
        }
        throw new NotSupportedException(typeof(T).ToString());
    }

    // public T ReadValue<T>(T value) {
        // ReadValue<T>(ref value);
        // return value;
    // }
    
    public void ReadValue(Type type, out object? value) {
        throw new NotImplementedException();
    }

    public object? ReadValue(Type type) {
        if (ReadNull()) {
            return default;
        }
        var formatter = SerializeFormatter.GetFormatter(type);
        if (formatter != null) {
            object? value = default;
            formatter.SerializeReading(this, ref value!);
            return value;
        }
        if (type.IsAssignableFrom(typeof(ISerializable))) {
            var value = Activator.CreateInstance(type);
            (value as ISerializable)?.SerializeReading(this);
            return value;
        }
        if (type.IsEnum) {
            return ReadEnum(type);
        }
        throw new NotSupportedException(type.ToString());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ReadValue<T>(ref T? value) => value = ReadValue<T>();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T? ReadField<T>(string name, int index) {
        ReadTag(name, index);
        return ReadValue<T>();
    }
    
    // [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // public void ReadField<T>(string name, int index, out T? value) {
    //     ReadTag(name, index);
    //     ReadValue(ref value);
    // }

    #endregion

    #region Primitive

    // Types
    public abstract bool ReadNull();
    
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

    public abstract string ReadString();

    public abstract DateTime ReadDateTime();

    public abstract TimeSpan ReadTimeSpan();

    public abstract Guid ReadGuid();

    public abstract Type ReadType();

    public abstract T ReadEnum<T>() where T: notnull;
    
    public abstract object ReadEnum(Type type);

    #endregion

    
}

