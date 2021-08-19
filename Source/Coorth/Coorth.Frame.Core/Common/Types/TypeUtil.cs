using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Coorth {
    public static class TypeUtil {

        #region Assembly

        public static void ForEachAssembly(Action<Assembly> action, bool onLoaded = true) {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                action(assembly);
            }

            if (onLoaded) {
                AppDomain.CurrentDomain.AssemblyLoad += (_, args) => action(args.LoadedAssembly);
            }
        }

        #endregion
        
        #region Type

        public static void ForEachType(Action<Type> action, bool onLoaded = true) {
            ForEachAssembly(assembly => {
                foreach (Type type in assembly.GetTypes()) {
                    action(type);
                }
            }, onLoaded);
        }

        public static IEnumerable<Type> GetTypeOfBase<T>(Func<Type, bool> filter = null) {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());
            return filter != null 
                ? types.Where(t => filter(t) && typeof(T).IsAssignableFrom(t)) 
                : types.Where(t => typeof(T).IsAssignableFrom(t));
        }

        public static bool TryGetTypeByGuid(Guid key, out Type type) {
            type = StoreModel.GetTypeByGuid(key);
            return type != null;
        }
        
        public static bool TryGetTypeByGuid(string guid, out Type type) {
            if (string.IsNullOrEmpty(guid) || !Guid.TryParse(guid, out var key)) {
                type = null;
                return false;
            }
            type = StoreModel.GetTypeByGuid(key);
            return type != null;
        }

        public static bool TryGetGuidByType(Type type, out Guid guid) {
           var model = StoreModel.GetModel(type);
           if (model != null) {
               guid = model.Guid;
               return (guid != Guid.Empty);
           }
           guid = Guid.Empty;
           return false;
        }

        public static IEnumerable<KeyValuePair<Guid, Type>> GetTypesWithGuid() {
            foreach (var pair in StoreModel.Guid2Types) {
                yield return pair;
            }
        }

        private static readonly ConcurrentDictionary<Type, string> displayNames = new ConcurrentDictionary<Type, string>();

        private static string _GetDisplayName(Type type) {
            if (type.IsPrimitive) {
                return type.Name;
            }
            if (type.IsArray) {
                return $"{GetDisplayName(type.GetElementType())}[]";
            }
            if (type.IsGenericType) {
                var definition = type.GetGenericTypeDefinition();
                var arguments = type.GetGenericArguments();
                if (definition == typeof(List<>)) {
                    var item = arguments[0];
                    return $"List[{GetDisplayName(item)}]";
                }
                if (definition == typeof(Dictionary<,>)) {
                    var key = arguments[0];
                    var value = arguments[1];
                    return $"Dictionary[{GetDisplayName(key)}, {GetDisplayName(value)}]";
                }
                
                var name = type.Name.Substring(0, type.Name.Length-2) + "[";
                foreach (var argument in arguments) {
                    name += GetDisplayName(argument) + ", ";
                }

                return name.Substring(0, name.Length-2) + "]";
            }
            return type.Name;
        }
        
        public static string GetDisplayName(Type type) {
            if (displayNames.TryGetValue(type, out var name)) {
                return name;
            }
            name = _GetDisplayName(type);
            displayNames.TryAdd(type, name);
            return name;
        }



        #endregion

        #region Attribute

        public static IEnumerable<Type> GetTypesWithAttribute<T>(IEnumerable<Assembly> assemblies) {
            return assemblies
                .SelectMany((assembly, i) => assembly.GetTypes())
                .Where(t => t.IsDefined(typeof(T), false));
        }
        
        public static IEnumerable<Type> GetTypesWithAttribute<T>() {
            return GetTypesWithAttribute<T>(AppDomain.CurrentDomain.GetAssemblies());
        }


        #endregion

        #region Member

        public static IEnumerable<PropertyInfo> GetProperties(Type type, Type attr = null) {
            if (type.IsInterface) {
                return type.GetInterfaces()
                    .SelectMany(t => GetProperties(t))
                    .Concat(type.GetProperties())
                    .Where(t => attr == null || t.IsDefined(attr));
            } else {
                return type.GetProperties()
                    .Where(t => attr == null || t.IsDefined(attr));
            }
        }

        public static string TrimInterfaceName(string name) {
            if (name[0] != 'I' || name.Length < 2) {
                return name;
            }
            if (char.IsLetter(name[1]) && char.IsUpper(name[1])) {
                return name.Substring(1);
            }
            return name;
        }

        #endregion

        #region Serialzie

        

        #endregion
    }
}
