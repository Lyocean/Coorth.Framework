using System;

namespace Coorth.Framework; 

public abstract class ComponentPack {
    public abstract Type Type { get; }

    public abstract void UnPack(Sandbox sandbox, Entity entity);
}
    
public class ComponentPack<T> : ComponentPack where T : IComponent {
        
    public override Type Type => typeof(T);

    public T? Component;

    public override void UnPack(Sandbox sandbox, Entity entity) {
        var componentGroup = sandbox.GetComponentGroup<T>();
        if (Component != null) {
            componentGroup._Clone(entity, ref Component, out var newComponent);
            entity.Add(newComponent);
        }
        else {
            throw new NullReferenceException();
        }
    }
        
    public void Pack(Sandbox sandbox, Entity entity, ref T component) {
        var componentGroup = sandbox.GetComponentGroup<T>();
        componentGroup._Clone(entity, ref component, out Component);
    }
}