using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Coorth; 

public static class TypeUtil {
    private static void ForEachAssembly(Action<Assembly> action, bool onLoaded) {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
            action(assembly);
        }
        if (onLoaded) {
            AppDomain.CurrentDomain.AssemblyLoad += (_, args) => action(args.LoadedAssembly);
        }
    }

    public static void ForEachType(Action<Type> action, bool onLoaded) {
        ForEachAssembly(assembly => {
            foreach (var type in assembly.GetTypes()) {
                action(type);
            }
        }, onLoaded);
    }
        
    public static IEnumerable<Type> GetTypeOfBase<T>(Func<Type, bool>? filter = null) {
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());
        return filter != null 
            ? types.Where(t => filter(t) && typeof(T).IsAssignableFrom(t)) 
            : types.Where(t => typeof(T).IsAssignableFrom(t));
    }
        
    public static IEnumerable<Type> GetTypesWithAttribute<T>(IEnumerable<Assembly> assemblies) {
        return assemblies
            .SelectMany((assembly, _) => assembly.GetTypes())
            .Where(t => t.IsDefined(typeof(T), false));
    }
        
    public static IEnumerable<Type> GetTypesWithAttribute<T>() {
        return GetTypesWithAttribute<T>(AppDomain.CurrentDomain.GetAssemblies());
    }

    private static readonly ConcurrentDictionary<Type, string> displayNames = new ConcurrentDictionary<Type, string>();

    private static string _GetDisplayName(Type type) {
        if (type.IsPrimitive) {
            return type.Name;
        }
        if (type.IsArray) {
            return $"{GetDisplayName(type.GetElementType()!)}[]";
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

    private static readonly Func<Type, string> getDisplayAction = _GetDisplayName;

    public static string GetDisplayName(Type type) {
        return displayNames.GetOrAdd(type, getDisplayAction);
    }
}