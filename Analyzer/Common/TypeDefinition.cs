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
    public string Name = string.Empty;
    public string Type = string.Empty;
    public int Index;
    public string Comment = string.Empty;

    public bool IsCollection;
    public CollectionType CollectionType;
    public GenericDefinition[] Children = Array.Empty<GenericDefinition>();
}

public class MethodDefinition {
    public string Name = string.Empty;
    public string Return = string.Empty;
    public readonly List<ParameterDefinition> Params = new();
}

public class ParameterDefinition {
    public string Name = string.Empty;
    public string Type = string.Empty;
}

public class GenericDefinition {
    public string Type = "";
}

public enum CollectionType {
    Array,
    List,
    Dict,
}


