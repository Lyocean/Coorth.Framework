using System;

namespace Coorth.Framework; 

public class ArchetypeBuilder {
        
    private readonly Sandbox sandbox;

    private ArchetypeDefinition definition;
        
    private bool closed;
    
    internal ArchetypeBuilder(Sandbox s, ArchetypeDefinition d) {
        sandbox = s;
        definition = d;
    }

    private void Validate(Type type) {
        if (closed) {
            throw new InvalidOperationException("Archetype is closed.");
        }
        if (!typeof(IComponent).IsAssignableFrom(type)) {
            throw new ArgumentException("Type is not implement IComponent");
        } 
        if (!sandbox.IsComponentBind(type)) {
            throw new NotBindException(type);
        }
    }

    public ArchetypeBuilder Add(Type type) {
        Validate(type);
        var typeId = Sandbox.ComponentTypeIds[type];
        definition = definition.AddComponent(typeId);
        return this;
    }
        
    public ArchetypeBuilder Add<T>() where T : IComponent {
        Validate(typeof(T));
        var typeId = ComponentType<T>.TypeId;
        definition = definition.AddComponent(typeId);
        return this;
    }

    public ArchetypeBuilder Remove(Type type) {
        Validate(type);
        var typeId = Sandbox.ComponentTypeIds[type];
        definition = definition.RemoveComponent(typeId);
        return this;
    } 

    public ArchetypeBuilder Remove<T>() where T : IComponent {
        Validate(typeof(T));
        var typeId = ComponentType<T>.TypeId;
        definition = definition.RemoveComponent(typeId);
        return this;
    }
    
    public Archetype Compile() {
        closed = true;
        return new Archetype(sandbox, definition);
    }
}