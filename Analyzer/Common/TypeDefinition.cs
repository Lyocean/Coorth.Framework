using System;
using System.Collections.Generic;

namespace Coorth.Analyzer; 

public class TypeDefinition {
    public bool IsClass;
    public bool IsRecord;
    public string[] Using = Array.Empty<string>();
    public string Namespace = string.Empty;
    public string TypeName = string.Empty;
    public string FullName => string.IsNullOrEmpty(Namespace) ? TypeName : $"{Namespace}.{TypeName}";
    
    public readonly List<FieldDefinition> Fields = new();
    public readonly List<MethodDefinition> Methods = new();
}

public class FieldDefinition {
    public string Name;
    public string Type;
    public int Index;
    public string Comment;

    public bool IsCollection;
    public CollectionType CollectionType;
    public GenericDefinition[] Children;
}

public class MethodDefinition {
    public string Name;
    public List<ParamDefinition> Params = new();
}

public class ParamDefinition {
    public string Name;
    public string Type;
}

public class GenericDefinition {
    public string Type;
}

public enum CollectionType {
    Array,
    List,
    Dict,
}


