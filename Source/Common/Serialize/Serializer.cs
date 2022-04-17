using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Coorth {
    public abstract class Serializer {
        
        private static readonly Dictionary<Type, Serializer> serializers = new();
        private struct Impl<T> {
            public static Serializer<T>? Instance;
        }
        
        static Serializer() {
            TypeUtil.ForEachType(Load, true);
        }
        
        private static void Load(Type type) {
            var attribute = type.GetCustomAttribute<DataSerializerAttribute>();
            if (attribute == null) {
                return;
            }
            if (Activator.CreateInstance(type) is not Serializer serializer) {
                throw new InvalidDataException($"{nameof(DataSerializerAttribute)} must be attribute of {typeof(Serializer)}");
            }
            serializers.Add(attribute.Type, serializer);
        }
        
        public static Serializer? GetSerializer(Type type) {
            return serializers.TryGetValue(type, out var serializer) ? serializer : null;
        }
        
        public static Serializer<T>? GetSerializer<T>() {
            var serializer = Impl<T>.Instance;
            if (serializer != null) {
                return serializer;
            }
            serializer = GetSerializer(typeof(T)) as Serializer<T>;
            if (serializer != null) {
                Impl<T>.Instance = serializer;
            }
            return serializer;
        }
        
        public abstract void WriteObject(SerializeWriter writer, in object? value);
        public abstract object? ReadObject(SerializeReader reader, object? value);
    }
    
    public abstract class Serializer<T> : Serializer {
        public override void WriteObject(SerializeWriter writer, in object? value) => Write(writer, value != null ? (T)value : default);
        public override object? ReadObject(SerializeReader reader, object? value) {
            var result = Read(reader, value != null ? (T)value : default);
            return result ?? default;
        }

        public abstract void Write(SerializeWriter writer, in T? value);
        public abstract T? Read(SerializeReader reader, T? value);
    }

    public abstract class TupleSerializer<T> : Serializer<T> where T : struct {
        public override void Write(SerializeWriter writer, in T value) {
            writer.BeginScope(typeof(T), SerializeScope.Tuple);
            OnWrite(writer, value);
            writer.EndScope();
        }

        protected abstract void OnWrite(SerializeWriter writer, in T value);

        public override T Read(SerializeReader reader, T value) {
            reader.BeginScope(typeof(T), SerializeScope.Tuple);
            OnRead(reader, ref value);
            reader.EndScope();
            return value;
        }

        protected abstract void OnRead(SerializeReader reader, ref T value);
    }
    
    public abstract class StructSerializer<T> : Serializer<T> where T : struct {
        public override void Write(SerializeWriter writer, in T value) {
            writer.BeginScope(typeof(T), SerializeScope.Struct);
            OnWrite(writer, value);
            writer.EndScope();
        }

        protected abstract void OnWrite(SerializeWriter writer, in T value);

        public override T Read(SerializeReader reader, T value) {
            reader.BeginScope(typeof(T), SerializeScope.Struct);
            OnRead(reader, ref value);
            reader.EndScope();
            return value;
        }

        protected abstract void OnRead(SerializeReader reader, ref T value);
    }
    
    public abstract class ClassSerializer<T> : Serializer<T> where T: class {
        public override void Write(SerializeWriter writer, in T? value) {
            writer.BeginScope(typeof(T), SerializeScope.Class);
            OnWrite(writer, value);
            writer.EndScope();
        }

        protected abstract void OnWrite(SerializeWriter writer, in T? value);

        public override T? Read(SerializeReader reader, T? value) {
            value ??= Activator.CreateInstance<T>();
            reader.BeginScope(typeof(T), SerializeScope.Class);
            OnRead(reader, ref value);
            reader.EndScope();
            return value;
        }

        protected abstract void OnRead(SerializeReader reader, ref T? value);
    }

    public abstract class CollectionSerializer<T> : Serializer<T> where T : class {
        public override object? ReadObject(SerializeReader reader, object? value) {
            return Read(reader, value as T);
        }

        public override void WriteObject(SerializeWriter writer, in object? value) {
            Write(writer, value as T);
        }
    }
}
