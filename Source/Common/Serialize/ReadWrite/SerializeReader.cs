using System;
using System.Collections.Generic;
using Coorth.Serializes;

namespace Coorth {
    public abstract class SerializeReader : SerializeBase {
        
        #region Read

        //Root
        public abstract void BeginRoot();
        //Scope
        public abstract void BeginScope(Type type, SerializeScope scope);
        public void BeginScope<T>(SerializeScope scope) => BeginScope(typeof(T), scope);
        //List
        public abstract void BeginList(Type item, out int count);
        public void BeginList<T>(out int count) => BeginList(typeof(T), out count);
        //Dict
        public abstract void BeginDict(Type key, Type value, out int count);
        public void BeginDict<TKey, TValue>(out int count) => BeginDict(typeof(TKey), typeof(TValue), out count);
        //Tag, Key, Value
        public abstract int ReadTag(string name, int index);
        public virtual TKey ReadKey<TKey>() where TKey: notnull {   
            //UnityEngine.Debug.Log($"ReadKey:{typeof(TKey)}");
            var type = typeof(TKey);
            if (type.IsEnum) {
                return ReadEnum<TKey>();                
            }
            if (type.IsPrimitive) {
                var serializer = Serializer.GetSerializer<TKey>();
                if (serializer != null) {
                    var key = serializer.Read(this, default);
                    return key ?? throw new NotSupportedException(type.ToString());
                }
            } 
            throw new NotSupportedException(type.ToString());
        }

        public void ReadValue<T>(ref T? value) {
            if (typeof(T).IsEnum) {
                value = ReadEnum<T>();
                return;
            }
            var serializer = Serializer.GetSerializer<T>();
            if (serializer != null) {
                value = serializer.Read(this, value);
                return;
            }
            var objectSerializer = ObjectSerializer.Get(typeof(T));
            if (objectSerializer == null) {
                throw new NotSupportedException(typeof(T).ToString());
            }
            var obj = objectSerializer.ReadObject(this, default);
            if (obj != null) {
                value = (T)obj;
            }
            else {
                value = default;
            }
        }
        public virtual T ReadValue<T>() {
            T value = default;
            ReadValue(ref value);
            return value;
        }

        //Object
        public virtual object? ReadObject(Type type) {
            //UnityEngine.Debug.Log($"ReadObject:{type}");

            var serializer = Serializer.GetSerializer(type);
            if (serializer != null) {
                return serializer.ReadObject(this, default);
            }
            serializer = ObjectSerializer.Get(type);
            if (serializer != null) {
                return serializer.ReadObject(this, default);
            }
            return null;
        }
        
        //Field
        public T? ReadField<T>(string name, int index) {
            //UnityEngine.Debug.Log($"ReadField:{name} - {index}");
            ReadTag(name, index);
            return ReadValue<T>();
        }
        
        #endregion

        #region Primitive

        public abstract bool ReadBool();

        public abstract byte ReadByte();

        public abstract sbyte ReadSByte();

        public abstract short ReadShort();

        public abstract ushort ReadUShort();

        public abstract int ReadInt();

        public abstract uint ReadUInt();

        public abstract long ReadLong();

        public abstract ulong ReadULong();

        public abstract float ReadFloat();

        public abstract double ReadDouble();

        public abstract char ReadChar();

        public abstract string ReadString();

        public abstract DateTime ReadDateTime();

        public abstract TimeSpan ReadTimeSpan();

        public abstract Guid ReadGuid();

        public abstract Type ReadType();

        public abstract T ReadEnum<T>();

        #endregion

        #region Extension

        public void ReadList<T>(ref List<T>? list) {
            BeginList<T>(out var count);
            if (count >= 0) {
                if (list == null) {
                    list = new List<T>(count);
                }
                else {
                    list.Clear();
                }
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

        public void ReadList<T>(ref T[]? array) {
            BeginList<T>(out var count);
            if (count >= 0) {
                if (array == null || array.Length != count) {
                    array = new T[count];
                }

                for (var i = 0; i < count; i++) {
                    var value = ReadValue<T>();
                    array[i] = value;
                }
                EndList();
            }
            else {
                var list = new List<T>();
                while (!EndList()) {
                    var value = ReadValue<T>();
                    list.Add(value);
                }
                array = list.ToArray();
            }
        }

        public void ReadDict<TK, TV>(ref Dictionary<TK, TV>? dict) where TK : notnull {
            BeginDict<TK, TV>(out var count);
            if (dict == null) {
                dict = count > 0 ? new Dictionary<TK, TV>(count) : new Dictionary<TK, TV>();
            }
            else {
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

        #endregion

    }
}