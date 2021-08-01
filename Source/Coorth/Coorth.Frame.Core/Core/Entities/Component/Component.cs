using System;
using System.Collections.Generic;

namespace Coorth {
    public interface IComponent {
    }
    
    public interface IComponent<in TP1> : IComponent, IAwake<TP1> {
    }
    
    public interface IComponent<in TP1, in TP2> : IComponent, IAwake<TP1, TP2> {
    }
    
    public interface IComponent<in TP1, in TP2, in TP3> : IComponent, IAwake<TP1, TP2, TP3> {
    }

    public interface IComponent<in TP1, in TP2, in TP3, in TP4> : IComponent, IAwake<TP1, TP2, TP3, TP4> {
    }
    
    public interface IComponent<in TP1, in TP2, in TP3, in TP4, in TP5> : IComponent, IAwake<TP1, TP2, TP3, TP4, TP5> {
    }
    
    public interface IRefComponent : IComponent {
        Entity Entity { get; set; }
    }
    


    public interface IHierarchyComponent : IComponent {
        IEnumerable<Entity> GetChildrenEntity();
    }

    public interface IHierarchyComponent<out TRefComponent> : IHierarchyComponent where TRefComponent: IRefComponent {
        IEnumerable<TRefComponent> GetChildrenComponent();
    }

    public enum ComponentScale {
        Singleton,
        Small,
        Large,
    }
    
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct)]
    public class ComponentAttribute : Attribute {
        public ComponentScale Scale = ComponentScale.Large;
        public int Capacity = 0;
    }

    public abstract class RefComponent : IRefComponent {
        public Entity Entity { get; set; }
        protected Sandbox Sandbox => Entity.Sandbox;
    }

    public static class ComponentExtension {
        public static ComponentWrap<T> Wrap<T>(this T component) where T : IRefComponent {
            return component.Entity.Wrap<T>();
        }
    }
    
    public readonly struct ComponentWrap<T> where T : IComponent {
        
        private readonly EntityId entity;
        
        private readonly ComponentGroup<T> group;
        
        private readonly int index;
        
        public ref T Ref => ref group.Ref(index);
        
        public T Val => group[index];

        internal ComponentWrap(EntityId entity, ComponentGroup<T> group, int index) {
            this.entity = entity;
            this.group = group;
            this.index = index;
        }
    }


}
