using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Coorth.Serializes;

namespace Coorth {
    public abstract class SerializeWriter : SerializeBase {
        
        #region Write

        //Root
        public abstract void BeginRoot(Type type);
        //Scope
        public abstract void BeginScope(Type type, SerializeScope scope);
        public void BeginScope<T>(SerializeScope scope) => BeginScope(typeof(T), scope);
        //List
        public abstract void BeginList(Type type, int count);
        public void BeginList<T>(int count) => BeginList(typeof(T), count);
        //Dict
        public abstract void BeginDict(Type key, Type value, int count);
        public void BeginDict<TKey, TValue>(int count) => BeginDict(typeof(TKey), typeof(TValue), count);
        //Tag, Key, Value
        public abstract void WriteTag(string name, int index);

        public virtual void WriteKey<TKey>(in TKey key) where TKey : notnull {
            // UnityEngine.Debug.Log($"WriteKey:{key}");
            var type = typeof(TKey);
            if (type.IsPrimitive) {
                var serializer = Serializer.GetSerializer<TKey>();
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

        public virtual void WriteValue<T>(in T value) {
            //LogUtil.Debug($"WriteValue:{value}");

            if (typeof(T).IsEnum) {
                // UnityEngine.Debug.Log($"WriteValue Enum:{value}");
                WriteEnum<T>(value);
                return;
            }
            var serializer = Serializer.GetSerializer<T>();
            if (serializer != null) {
                // UnityEngine.Debug.Log($"WriteValue Enum Custom:{value}");
                serializer.Write(this, value);
                return;
            }

            var reflectSerializer = ObjectSerializer.Get(typeof(T));
            if (reflectSerializer != null) {
                // UnityEngine.Debug.Log($"WriteValue Enum Reflect:{value}");
                reflectSerializer.WriteObject(this, value);
                return;
            }
            throw new NotSupportedException(typeof(T).ToString());
        }
        //Object
        public virtual void WriteObject(Type type, object? value) {
            //LogUtil.Debug($"WriteObject:{type}");
            if (value == null) {
                return;
            }
            var serializer = Serializer.GetSerializer(type);
            if (serializer != null) {
                //UnityEngine.Debug.Log($"GetSerializer:{serializer}");
                serializer.WriteObject(this, value);
                return;
            }
            var reflectSerializer = ObjectSerializer.Get(type);
            if (reflectSerializer != null) {
                //UnityEngine.Debug.Log($"reflectSerializer:{serializer}");

                // UnityEngine.Debug.Log($"WriteValue Enum Reflect:{value}");
                reflectSerializer.WriteObject(this, value);
                return;
            }
            throw new NotSupportedException(type.ToString());
        }

        //Field
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteField<T>(string name, int index, in T value) {
            WriteTag(name, index);
            WriteValue(value);
        }

        #endregion

        #region Primitive

        public abstract void WriteBool(bool value);
        
        public abstract void WriteByte(byte value);
        
        public abstract void WriteSByte(sbyte value);
        
        public abstract void WriteShort(short value);
        
        public abstract void WriteUShort(ushort value);
        
        public abstract void WriteInt(int value);
        
        public abstract void WriteUInt(uint value);
        
        public abstract void WriteLong(long value);
        
        public abstract void WriteULong(ulong value);
        
        public abstract void WriteFloat(float value);
        
        public abstract void WriteDouble(double value);
        
        public abstract void WriteChar(char value);
        
        public abstract void WriteString(string value);

        public abstract void WriteDateTime(DateTime value);

        public abstract void WriteTimeSpan(TimeSpan value);

        public abstract void WriteGuid(Guid value);

        public abstract void WriteType(Type value);

        public abstract void WriteEnum<T>(T value) where T : notnull;

        #endregion

        #region Extension

        public void WriteList<T>(in IList<T> list) {
            BeginList<T>(list.Count);
            foreach (var value in list) {
                WriteValue(value);
            }
            EndList();
        }
        
        public void WriteList<T>(IReadOnlyList<T> list) {
            BeginList<T>(list.Count);
            foreach (var value in list) {
                WriteValue(value);
            }
            EndList();
        }
        
        public void WriteDict<TK, TV>(IDictionary<TK, TV>? dict) where TK : notnull {
            if(dict == null){
                BeginDict<TK, TV>(0);
                EndDict();
                return;
            }
            BeginDict<TK, TV>(dict.Count);
            foreach (var pair in dict) {
                WriteKey(pair.Key);
                WriteValue(pair.Value);
            }
            EndDict();
        }
        
        public void WriteDict<TK, TV>(IReadOnlyDictionary<TK, TV>? dict) where TK : notnull {
            if(dict == null){
                BeginDict<TK, TV>(0);
                EndDict();
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
    }
}