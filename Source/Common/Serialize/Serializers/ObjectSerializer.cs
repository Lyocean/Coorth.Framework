using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
// using UnityEngine;

namespace Coorth.Serializes {
    public class ObjectSerializer : Serializer<object> {

        #region Static

        private static readonly ConcurrentDictionary<Type, ObjectSerializer?> serializers = new();

        public static ObjectSerializer? Get(Type type) {
            return serializers.GetOrAdd(type, createAction);
        }

        private static readonly Func<Type, ObjectSerializer> createAction = Create;

        private static ObjectSerializer? Create(Type type) {
            // if (type.IsDefined(typeof(DataContractAttribute))) {
            //     return new ObjectSerializer(type, type.GetCustomAttribute<DataContractAttribute>()!);
            // }
            if (type.IsDefined(typeof(DataContractAttribute))) {
                return new ObjectSerializer(type, type.GetCustomAttribute<DataContractAttribute>()!);
            }
            if (type.IsDefined(typeof(SerializableAttribute))) {
                return new ObjectSerializer(type, type.GetCustomAttribute<SerializableAttribute>()!);
            }
            return null;
        }

        #endregion

        #region Common

        public readonly Type Type;

        private readonly MemberSerializer[] members;
        public IReadOnlyList<MemberSerializer> Members => members;

        // public ObjectSerializer(Type type, DataContractAttribute attribute) {
        //     this.Type = type;
        //     this.members = this.InitMembers();
        // }

        public ObjectSerializer(Type type, DataContractAttribute attribute) {
            this.Type = type;
            this.members = this.InitMembers();
        }

        public ObjectSerializer(Type type, SerializableAttribute attribute) {
            this.Type = type;
            this.members = this.InitMembers();
        }

        private MemberSerializer[] InitMembers() {
            var list = new List<MemberSerializer>();
            var fieldInfos = Type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var fieldInfo in fieldInfos) {
                AddMember(list, fieldInfo.FieldType, fieldInfo, fieldInfo.IsPublic);
            }
            var propertyInfos = Type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var propertyInfo in propertyInfos) {
                if (propertyInfo.CanRead && propertyInfo.CanWrite) {
                    AddMember(list, propertyInfo.PropertyType, propertyInfo, false);
                }
            }
            var index = list.Count > 0 ? list.Max(a => a.Index) + 1 : 0;
            for (var i = 0; i < list.Count; i++) {
                var item = list[i];
                if (item.Index == 0) {
                    item.Index = index++;
                }
                list[i] = item;
            }
            list.Sort((a, b) => a.Index - b.Index);
            return list.ToArray();
        }

        private static bool AddMember(ICollection<MemberSerializer> list, Type memberType, MemberInfo memberInfo, bool isDefaultSerialize) {
            if (memberInfo.IsDefined(typeof(IgnoreDataMemberAttribute)) || memberInfo.IsDefined(typeof(DataIgnoreAttribute)) || memberInfo.IsDefined(typeof(NonSerializedAttribute))) {
                return false;
            }
            var markByAttribute = memberInfo.IsDefined(typeof(DataMemberAttribute));
            if (!isDefaultSerialize && ! markByAttribute) {
                return false;
            }
            var memberSerializer = MemberSerializer.Create(memberInfo, memberType);
            if(memberSerializer != null) {
                list.Add(memberSerializer);
                return true;
            }

            if (markByAttribute) {
                throw new SerializationException($"Create serializer failed : {memberType}");
            }
            
            
            return false;
        }

        #endregion

        #region Read Write

        public override void Write(SerializeWriter writer, in object value) {
            var scope = Type.IsClass ? SerializeScope.Class : SerializeScope.Struct;
            writer.BeginScope(Type, scope);
            foreach (var member in Members) {
                member.WriteMember(writer, value);
            }
            writer.EndScope();
        }

        public override object Read(SerializeReader reader, object value) {
            LogUtil.Debug($"================>Read:{Type}");
            var scope = Type.IsClass ? SerializeScope.Class : SerializeScope.Struct;
            reader.BeginScope(Type, scope);
            value ??= Activator.CreateInstance(Type);
            foreach (var member in Members) {
                member.ReadMember(reader, value);
            }
            reader.EndScope();
            return value;
        }

        #endregion
    }

    public class MemberSerializer {
        private readonly MemberInfo memberInfo;
        private readonly Serializer serializer;
        private readonly string name;
        private string Name => name ?? memberInfo.Name;
        public int Index;
        private readonly bool IsRequire;

        private MemberSerializer(MemberInfo memberInfo, Serializer serializer) {
            this.memberInfo = memberInfo;
            this.serializer = serializer;
            var store = memberInfo.GetCustomAttribute<DataMemberAttribute>();
            if (store != null) {
                name = store.Name;
                Index = store.Order;
                IsRequire = store.IsRequired;
                return;
            }
            var data = memberInfo.GetCustomAttribute<System.Runtime.Serialization.DataMemberAttribute>();
            if (data != null) {
                name = data.Name;
                Index = data.Order;
                IsRequire = data.IsRequired;
                return;
            }
            name = null;
            Index = 0;
            IsRequire = true;
        }

        public static MemberSerializer Create(MemberInfo memberInfo, Type memberType) {
            var serializer = Serializer.GetSerializer(memberType);
            if (serializer != null) {
                return new MemberSerializer(memberInfo, serializer);
            }
            if (memberType.IsEnum) {
                var serializerType = (typeof(EnumSerializer<>)).MakeGenericType(memberType);
                serializer = (Serializer)Activator.CreateInstance(serializerType);
                return new MemberSerializer(memberInfo, serializer);
            }
            if (memberType.IsArray) {
                var itemType = memberType.GetElementType();
                var serializerType = (typeof(ListSerializer<>)).MakeGenericType(itemType);
                serializer = (Serializer)Activator.CreateInstance(serializerType);
                return new MemberSerializer(memberInfo, serializer);
            }
            if (memberType.IsGenericType) {
                var genericDefinition = memberType.GetGenericTypeDefinition();
                if (genericDefinition == typeof(List<>) || genericDefinition == typeof(IList<>) || genericDefinition == typeof(IReadOnlyList<>)) {
                    var itemType = memberType.GetGenericArguments()[0];
                    var serializerType = (typeof(ArraySerializer<>)).MakeGenericType(itemType);
                    serializer = (Serializer)Activator.CreateInstance(serializerType);
                    return new MemberSerializer(memberInfo, serializer);
                }
                if (genericDefinition == typeof(Dictionary<,>) || genericDefinition == typeof(IDictionary<,>) || genericDefinition == typeof(IReadOnlyDictionary<,>)) {
                    var keyType = memberType.GetGenericArguments()[0];
                    var valueType = memberType.GetGenericArguments()[1];
                    var serializerType = (typeof(DictSerializer<,>)).MakeGenericType(keyType, valueType);
                    serializer = (Serializer)Activator.CreateInstance(serializerType);
                    return new MemberSerializer(memberInfo, serializer);
                }
            }
            if (memberType.IsDefined(typeof(DataMemberAttribute)) || 
                memberType.IsDefined(typeof(System.Runtime.Serialization.DataContractAttribute)) || 
                memberType.IsDefined(typeof(SerializableAttribute))) {
                
                serializer = ObjectSerializer.Get(memberType);
                return new MemberSerializer(memberInfo, serializer);
            }
            return null;
        }

        public void ReadMember(SerializeReader reader, object obj) {
            reader.ReadTag(Name, Index);
            // UnityEngine.Debug.Log($"<color=yellow>ReadMember ++++++++++++++++</color>:{this.Name}");
            var value = serializer.ReadObject(reader, default);
            // UnityEngine.Debug.Log($"<color=red>ReadMember ----------------</color>:{this.Name} {memberInfo} = {value}");

            if (memberInfo is FieldInfo fieldInfo) {
                fieldInfo.SetValue(obj, value);
            } else if (memberInfo is PropertyInfo propertyInfo) {
                propertyInfo.SetValue(obj, value);
            } else {
                throw new NotSupportedException();
            }
        }

        public void WriteMember(SerializeWriter writer, object obj) {
            if (memberInfo is FieldInfo fieldInfo) {
                var value = fieldInfo.GetValue(obj);
                writer.WriteTag(Name, Index);
                serializer.WriteObject(writer, value);
            } else if (memberInfo is PropertyInfo propertyInfo) {
                var value = propertyInfo.GetValue(obj);
                writer.WriteTag(Name, Index);
                serializer.WriteObject(writer, value);
            } else {
                throw new NotSupportedException();
            }
        }
    }


}