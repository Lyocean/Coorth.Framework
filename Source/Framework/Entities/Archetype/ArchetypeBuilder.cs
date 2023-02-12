using System;

namespace Coorth.Framework; 

public class ArchetypeBuilder {
        
    private readonly World world;

    private ArchetypeDefinition definition;
        
    private bool closed;
    
    internal ArchetypeBuilder(World s, ArchetypeDefinition d) {
        world = s;
        definition = d;
    }

    private void Validate(Type type) {
        if (closed) {
            throw new InvalidOperationException("Archetype is closed.");
        }
        if (!typeof(IComponent).IsAssignableFrom(type)) {
            throw new ArgumentException("Type is not implement IComponent");
        } 
        if (!world.IsComponentBind(type)) {
            throw new NotBindException(type);
        }
    }

    public ArchetypeBuilder Add(Type type) {
        Validate(type);
        var typeId = World.ComponentTypeIds[type];
        definition = definition.AddComponent(typeId);
        return this;
    }
        
    public ArchetypeBuilder Add<T>() where T : IComponent {
        world.BindComponent<T>();
        Validate(typeof(T));
        var typeId = ComponentType<T>.TypeId;
        definition = definition.AddComponent(typeId);
        return this;
    }

    public ArchetypeBuilder Remove(Type type) {
        Validate(type);
        var typeId = World.ComponentTypeIds[type];
        definition = definition.RemoveComponent(typeId);
        return this;
    } 

    public ArchetypeBuilder Remove<T>() where T : IComponent {
        world.BindComponent<T>();
        Validate(typeof(T));
        var typeId = ComponentType<T>.TypeId;
        definition = definition.RemoveComponent(typeId);
        return this;
    }
    
    public Archetype Compile() {
        closed = true;
        return new Archetype(world, definition);
    }
}