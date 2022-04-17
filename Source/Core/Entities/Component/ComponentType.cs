using System;
using System.Reflection;
using System.Threading;

namespace Coorth {
    public static class ComponentType<T> {
        
        public static readonly Type Type;
        
        public static readonly int TypeId;
        
        public static readonly ComponentAttribute? Attribute;
        
        public static readonly bool IsValueType;
        
        public static readonly bool IsPinned;
        
        static ComponentType() {
            Type = typeof(T);
            Attribute = Type.GetCustomAttribute<ComponentAttribute>();
            TypeId = Interlocked.Increment(ref Sandbox.ComponentTypeCount);
            Sandbox.ComponentTypeIds[Type] = TypeId;
            IsValueType = typeof(T).IsValueType;
            IsPinned = (Attribute != null) && Attribute.IsPinned;
        }
    }
}