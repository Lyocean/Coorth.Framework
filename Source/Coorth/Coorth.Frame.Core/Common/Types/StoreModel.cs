using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;


namespace Coorth {
    public class StoreModel {
        
        #region Static
        
        private static readonly ConcurrentDictionary<Guid, Type> guid2Types = new ConcurrentDictionary<Guid, Type>();
        public static IDictionary<Guid, Type> Guid2Types => guid2Types;

        private static readonly ConcurrentDictionary<Type, StoreModel> models = new ConcurrentDictionary<Type, StoreModel>();
        
        static StoreModel() {
            TypeUtil.ForEachType(OnLoadType, true);
        }

        private static void OnLoadType(Type type) {
            if (models.ContainsKey(type)) {
                return;
            }
            var attribute = type.GetCustomAttribute<StoreContractAttribute>();
            if (attribute == null) {
                return;
            }

            var model = models.GetOrAdd(type, _ => new StoreModel(_, attribute));
            if (model.Guid != Guid.Empty) {
                guid2Types.TryAdd(model.Guid, type);
            }
                        
            if (model.Base != null) {
                OnLoadType(model.Base);
                var baseModel = models[model.Base];
                lock (baseModel.Locking) {
                    baseModel.children.Add(model.Name, model);
                }
            }
        }

        public static Type GetTypeByGuid(Guid guid) {
            return guid2Types.TryGetValue(guid, out var type) ? type : null;
        }

        public static StoreModel GetModel(Type type) {
            return models.TryGetValue(type, out var model) ? model : null;
        }
        
        public static StoreModel GetModel(Type type, string name) {
            var model = GetModel(type);
            if (model == null || name == null) {
                return null;
            }
            if (model.HasChild) {
                return model.GetChild(name);
            }
            return null;
        }
        
        #endregion

        #region Child

        public readonly string Name;
        
        public readonly Type Type;

        public Type Base => Attribute.Base;

        public readonly StoreContractAttribute Attribute;

        public readonly Guid Guid;

        private Dictionary<string, StoreModel> children = new Dictionary<string, StoreModel>();
        public IReadOnlyDictionary<string, StoreModel> Chilren => children;
        
        private readonly List<Member> members = new List<Member>();
        public IReadOnlyList<Member> Members => members;

        public bool HasChild => children.Count > 0;
        
        public readonly object Locking = new object();
        
        private StoreModel(Type type, StoreContractAttribute attribute) {
            this.Type = type;
            this.Name = $"[{this.Type.Name}]";
            this.Attribute = attribute;
            this.Guid = Guid.TryParse(attribute.Guid, out var guid) ? guid : Guid.Empty;

            foreach (var memberInfo in type.GetMembers(GetFlags(type))) {
                var memberAttr = memberInfo.GetCustomAttribute<StoreMemberAttribute>();
                if (memberAttr == null) {
                    continue;
                }
                var memberType = memberInfo.MemberType == MemberTypes.Field
                    ? (memberInfo as FieldInfo)?.FieldType
                    : (memberInfo as PropertyInfo)?.PropertyType;
                if (memberType == null) {
                    continue;
                }
                var member = (memberInfo is FieldInfo) 
                    ? new Member((FieldInfo)memberInfo, memberAttr) 
                    : new Member((PropertyInfo)memberInfo, memberAttr);
                members.Add(member);
            }
            members.Sort((a, b)=> a.Attribute.Index - b.Attribute.Index);
        }

        private BindingFlags GetFlags(Type type) {
            if (type.IsEnum) {
                return BindingFlags.Static| BindingFlags.Public| BindingFlags.NonPublic;
            }
            else {
                return BindingFlags.Instance | BindingFlags.NonPublic| BindingFlags.Public;
            }
        }
        
        private StoreModel GetChild(string name) {
            return children.TryGetValue(name, out var child) ? child : null;
        }

        public class Member {
            public readonly Type Type;
            public readonly StoreMemberAttribute Attribute;
            public readonly MemberInfo Info;
            public bool IsBase => (Type.IsAbstract || Type.IsInterface);
            public bool IsList => Type.GetGenericTypeDefinition() == typeof(List<>);
            public bool IsDict => Type.GetGenericTypeDefinition() == typeof(Dictionary<,>);

            public Member(FieldInfo field, StoreMemberAttribute attribute) {
                this.Info = field;
                this.Type = field.FieldType;
                this.Attribute = attribute;
            }
        
            public Member(PropertyInfo property, StoreMemberAttribute attribute) {
                this.Info = property;
                this.Type = property.PropertyType;
                this.Attribute = attribute;
            }
        }

        #endregion
    }
}