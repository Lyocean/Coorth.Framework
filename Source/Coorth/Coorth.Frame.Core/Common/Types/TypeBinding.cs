using System;
using System.Collections.Concurrent;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using Coorth.Maths;

namespace Coorth {
    public static class TypeBinding {

        private abstract class BindingData<T> {
            public static BindingData<T> Instance;
            public static Type ImplType => Instance.GetImplType();
            protected abstract Type GetImplType();
            protected abstract T CreateInstance();
            public static T Create() => Instance.CreateInstance();
        }
        
        private class BindingData<T, TImpl> : BindingData<T> where TImpl : T {
            protected override Type GetImplType() {
                return typeof(TImpl);
            }

            protected override T CreateInstance() {
                return Activator.CreateInstance<TImpl>();
            }
        }

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
            
            Bind<object>("19F00BDD-2264-4465-AC85-B28A5C75B2EC");

            Bind<Type>("C0CD0AE0-33DA-4E93-A836-E17487463B5E");

            Bind<DateTime>("3D312AA2-2FBD-4DDF-82C0-E7A50CA2F2D9");
            Bind<TimeSpan>("643B4585-0D3E-41BC-8276-618ADEE89A49");
            Bind<Vector2>("D4BA03C3-67F5-46A2-BA3C-DEC02A7D6C45");
            Bind<Vector3>("C7D5F4CC-91B6-431F-9006-D27B8F05E003");
            Bind<Vector4>("699015B5-D33B-41F8-ADE6-830F057F88E6");
            
            TypeUtil.ForEachType(LoadType);
        }

        private static void LoadType(Type type) {
            var attribute = type.GetCustomAttribute<GuidAttribute>();
            if (attribute != null) {
                Bind(type, attribute.Value);
            }
        }
        
        public static void Bind<T, TImpl>() where TImpl : T, new(){
            BindingData<T>.Instance = new BindingData<T, TImpl>();
        }

        public static T Create<T>() {
            return BindingData<T>.Create();
        }

        private static readonly ConcurrentDictionary<Type, Guid> type2Guids = new ConcurrentDictionary<Type, Guid>();

        private static readonly ConcurrentDictionary<Guid, Type> guid2Types = new ConcurrentDictionary<Guid, Type>();

        public static void Bind<T>(string guid) => Bind(typeof(T), Guid.Parse(guid));
        public static void Bind<T>(Guid guid) => Bind(typeof(T), guid);
        public static void Bind(Type type, string guid) => Bind(type, Guid.Parse(guid));
        public static void Bind(Type type, Guid guid) {
            type2Guids[type] = guid;
            guid2Types[guid] = type;
        }
        
        
        public static Guid GetGuid<T>() => GetGuid(typeof(T));

        public static Guid GetGuid(Type type) {
            return type2Guids.TryGetValue(type, out var guid) ? guid : type.GUID;
        }
        
        public static Type GetType(Guid guid) {
            return guid2Types.TryGetValue(guid, out var type) ? type : Type.GetTypeFromCLSID(guid);
        }

    }
}