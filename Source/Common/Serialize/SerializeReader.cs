﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public virtual TKey ReadKey<TKey>() {   
            //UnityEngine.Debug.Log($"ReadKey:{typeof(TKey)}");
            var type = typeof(TKey);
            if (type.IsPrimitive) {
                var serializer = Serializer.GetSerializer<TKey>();
                if (serializer != null) {
                    return serializer.Read(this, default);
                }
            } else if (type.IsEnum) {
                return ReadEnum<TKey>();                
            }
            throw new NotSupportedException(type.ToString());
        }

        public virtual void ReadValue<T>(ref T value) {
            // UnityEngine.Debug.Log($"ReadValue:{typeof(T)}");
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
            if (objectSerializer != null) {
                value = (T)objectSerializer.ReadObject(this, default);
                return;
            }
            throw new NotSupportedException(typeof(T).ToString());
        }
        public virtual T ReadValue<T>() {
            T value = default;
            ReadValue(ref value);
            return value;
        }

        //Object
        public virtual object ReadObject(Type type) {
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
        public T ReadField<T>(string name, int index) {
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

        public void ReadList<T>(ref List<T> list) {
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

        public void ReadList<T>(ref T[] array) {
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

        public void ReadDict<TK, TV>(ref Dictionary<TK, TV> dict) {
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

    public abstract class ByteSerializeReader : SerializeReader {

        public override DateTime ReadDateTime() {
            return new DateTime(ReadLong());
        }

        public override TimeSpan ReadTimeSpan() {
            return new TimeSpan(ReadLong());
        }

        public override Guid ReadGuid() {
            var size = Unsafe.SizeOf<Guid>();
            var bytes = new byte[size];
            for (var i = 0; i < size; i++) {
                bytes[i] = ReadByte();
            }
            return new Guid(bytes);
        }

        public override Type ReadType() {
            var isGuid = ReadBool();
            if (isGuid) {
                return TypeBinding.GetType(ReadGuid());
            }
            else {
                return Type.GetType(ReadString());
            }
        }

        public override T ReadEnum<T>() {
            var size = Unsafe.SizeOf<T>();
            if (size == sizeof(byte)) {
                var value = ReadByte();
                return Unsafe.As<byte, T>(ref value);
            } else if (size == sizeof(short)) {
                var value = ReadShort();
                return Unsafe.As<short, T>(ref value);
            } else if (size == sizeof(int)) {
                var value = ReadInt();
                return Unsafe.As<int, T>(ref value);
            } else if (size == sizeof(long)) {
                var value = ReadLong();
                return Unsafe.As<long, T>(ref value);
            }
            throw new NotSupportedException(typeof(T).ToString());
        }
    }

    public abstract class TextSerializeReader : SerializeReader {
        public override DateTime ReadDateTime() {
            var text = ReadString();
            return DateTime.Parse(text);
        }

        public override TimeSpan ReadTimeSpan() {
            var text = ReadString();
            return TimeSpan.Parse(text);
        }

        public override Guid ReadGuid() {
            var text = ReadString();
            return Guid.Parse(text);
        }

        public override Type ReadType() {
            var text = ReadString();
            if (text.Contains("|")) {
                return TypeBinding.GetType(text.Substring(text.IndexOf("|", StringComparison.Ordinal)+1));
            }
            else {
                return Type.GetType(text);
            }
        }

        public override T ReadEnum<T>() {
            var text = ReadString();
            // UnityEngine.Debug.Log($"{typeof(T)} --> {text}");
            return (T)Enum.Parse(typeof(T), text);
        }
    }
}