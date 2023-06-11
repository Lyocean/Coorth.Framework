using System;

namespace Coorth.Framework;

public abstract class ComponentPack {
    public abstract Type Type { get; }
}

public class ComponentPack<T> : ComponentPack where T : IComponent {
    public override Type Type => typeof(T);

    public readonly T Component;

    public ComponentPack(T component) {
        Component = component;
    }
}