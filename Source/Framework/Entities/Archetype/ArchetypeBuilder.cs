using System;

namespace Coorth.Framework;

public class ArchetypeBuilder {

    private readonly World world;
    
    private Archetype archetype;

    private bool closed;

    internal ArchetypeBuilder(Archetype archetype) {
        world = archetype.World;
        this.archetype = archetype;
    }

    private void Validate(Type type) {
        if (closed) {
            throw new InvalidOperationException("[Entity] Archetype is closed.");
        }
        if (!typeof(IComponent).IsAssignableFrom(type)) {
            throw new ArgumentException("Type is not implement IComponent");
        }
    }

    public ArchetypeBuilder Add(Type type) {
        Validate(type);
        archetype = world.NextArchetype(archetype, ComponentRegistry.Get(type));
        return this;
    }

    public ArchetypeBuilder Add<T>() where T : IComponent {
        Validate(typeof(T));
        world.BindComponent<T>();
        archetype = world.NextArchetype(archetype, ComponentRegistry.Get<T>());
        return this;
    }
    
    public Archetype Build() {
        closed = true;
        return archetype;
    }
}