using System;
using System.Collections.Generic;
using Coorth.Collections;

namespace Coorth.Framework; 

public partial class Matcher {
    
    private ComponentType[] allTypes = Array.Empty<ComponentType>();
    public IReadOnlyList<ComponentType> AllTypes => allTypes;

    private ComponentType[] notTypes = Array.Empty<ComponentType>();
    public IReadOnlyList<ComponentType> NotTypes => notTypes;

    private ComponentType[] anyTypes = Array.Empty<ComponentType>();
    public IReadOnlyList<ComponentType> AnyTypes => anyTypes;

    private ComponentType[] types = Array.Empty<ComponentType>();
    public IReadOnlyList<ComponentType> Types => types;
    
    internal bool Match(Archetype archetype) {
        for (var i = 0; i < allTypes.Length; i++) {
            if (!archetype.ContainType(in allTypes[i])) {
                return false;
            }
        }
        for (var i = 0; i < notTypes.Length; i++) {
            if (archetype.ContainType(in notTypes[i])) {
                return false;
            }
        }
        if (anyTypes.Length == 0) {
            return true;
        }
        for (var i = 0; i < anyTypes.Length; i++) {
            if (archetype.ContainType(in notTypes[i])) {
                return true;
            }
        }
        return false;
    }
    
    private Matcher WithAll(params ComponentType[] values) {
        allTypes = values;
        types = ComponentRegistry.Combine(types, allTypes);
        return this;
    }

    private Matcher WithNot(params ComponentType[] values) {
        notTypes = values;
        types = ComponentRegistry.Combine(types, notTypes);
        return this;
    }
    
    private Matcher WithAny(params ComponentType[] values) {
        anyTypes = values;
        types = ComponentRegistry.Combine(types, anyTypes);
        return this;
    }

    public override string ToString() {
        using var builder = new ValueStringBuilder(512);
        builder.Append("Matcher={ ");
        
        builder.Append("All=[");
        foreach (var type in allTypes) {
            builder.Append(type.Type.Name);
            builder.Append(", ");
        }
        builder.Append("], ");
        
        builder.Append("Not=[");
        foreach (var type in notTypes) {
            builder.Append(type.Type.Name);
            builder.Append(", ");
        }
        builder.Append("], ");
        
        builder.Append("Any=[");
        foreach (var type in anyTypes) {
            builder.Append(type.Type.Name);
            builder.Append(", ");
        }
        builder.Append("]");
        
        builder.Append(" }");
        return builder.ToString();
    }
}
