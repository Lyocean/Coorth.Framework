using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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


public static class TypeBinding {
    static TypeBinding() {
        Bind<bool>("37D16C45-4D44-4D06-8C16-9534A5C0131E");
            
        Bind<byte>("46D16A65-DBAE-472A-96FC-F9D28BF190CD");
        Bind<sbyte>("2EDEDE96-7ABD-41D0-9606-C21D0C5E4019");
        Bind<short>("AB312A25-D965-4B35-94FC-7FE854D66EB9");
        Bind<ushort>("BA83E40C-FA21-4604-8D6C-483B93698D35");
        Bind<int>("8A316756-AEA2-4E56-AE43-BA401D628E1E");
        Bind<uint>("254781C8-69A7-4115-B8D5-B152DDF0FFB8");
        Bind<long>("17C4BAA4-D93F-4AF6-ACDE-2DA174DB4410");
        Bind<ulong>("446236C3-4218-4124-9CE5-C88E921C2351");
        Bind<decimal>("4B29E067-3C53-48C3-87CD-2E1BFE1D5AA4");

        Bind<char>("408AEA98-2328-4AB7-AF68-28F71CBEA79F");
        Bind<string>("BAFECBA5-2A70-47B1-871C-8C5AB5175F50");
            
        Bind<object>("19F00BDD-2264-4465-AC85-B28A5C75B2EC");
        Bind<Type>("C0CD0AE0-33DA-4E93-A836-E17487463B5E");
        Bind<Guid>("78EDB4BE-F87E-48F3-81F3-84E487E8174A");

        Bind<DateTime>("3D312AA2-2FBD-4DDF-82C0-E7A50CA2F2D9");
        Bind<TimeSpan>("643B4585-0D3E-41BC-8276-618ADEE89A49");

        Bind<Vector2>("D4BA03C3-67F5-46A2-BA3C-DEC02A7D6C45");
        Bind<Vector3>("C7D5F4CC-91B6-431F-9006-D27B8F05E003");
        Bind<Vector4>("699015B5-D33B-41F8-ADE6-830F057F88E6");
        Bind<Quaternion>("71FD0E7B-A928-4451-A66D-1CD6D3BDA757");
        Bind<Matrix3x2>("FB504A01-BD0B-443E-AFE4-4144A6C81471");
        Bind<Matrix4x4>("ADCEF165-A68F-4453-B159-1791A0B3169B");
            
        Bind<BigInteger>("0EAC7359-A150-4DF6-B177-ED6A37927958");
        Bind<Complex>("C9AEF795-3BE0-4FD1-A0E2-DF8D8B24163A");
        Bind<Plane>("6AB47230-914E-4A21-8DD8-40F835736908");

        TypeUtil.ForEachType(LoadType, true);
    }

    private static void LoadType(Type type) {
        var guidAttribute = type.GetCustomAttribute<GuidAttribute>();
        if (guidAttribute != null) {
            Bind(type, guidAttribute.Value);
        }
    }

    private static readonly ConcurrentDictionary<Type, Guid> type2Guids = new();

    private static readonly ConcurrentDictionary<Guid, Type> guid2Types = new();

    public static void Bind<T>(string guid) => Bind(typeof(T), Guid.Parse(guid));
        
    public static void Bind<T>(Guid guid) => Bind(typeof(T), guid);
        
    public static void Bind(Type type, string guid) => Bind(type, Guid.Parse(guid));
        
    public static void Bind(Type type, Guid guid) {
        type2Guids[type] = guid;
        guid2Types[guid] = type;
    }
        
    public static Guid GetGuid<T>() => GetGuid(typeof(T));

    public static Guid GetGuid(Type type) => type2Guids.TryGetValue(type, out var guid) ? guid : type.GUID;

    public static Type? GetType(Guid guid) => guid2Types.TryGetValue(guid, out var type) ? type : null;

    public static Type? GetType(string guid) => Guid.TryParse(guid, out var value) ? GetType(value) : null;
}