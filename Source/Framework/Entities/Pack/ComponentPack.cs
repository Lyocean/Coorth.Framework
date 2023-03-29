using System;

namespace Coorth.Framework;

public abstract class ComponentPack {
    public abstract Type Type { get; }

    public abstract void UnPack(World world, Entity entity);
}

public class ComponentPack<T> : ComponentPack where T : IComponent {
    public override Type Type => typeof(T);

    public T? Component;

    public override void UnPack(World world, Entity entity) {
        var group = world.GetComponentGroup<T>();
        if (Component != null) {
            group._Clone(entity, ref Component, out var newComponent);
            entity.Add(newComponent);
        }
        else {
            throw new NullReferenceException();
        }
    }

    public void Pack(World world, Entity entity, ref T component) {
        var group = world.GetComponentGroup<T>();
        group._Clone(entity, ref component, out Component);
    }
}