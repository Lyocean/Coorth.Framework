namespace Coorth.Analyzer.Define; 

public class ActorBuilder {

    private readonly CodeBuilder builder = new();
    
    public string Generate(TypeDefinition type) {
        builder.AddLine("#if SOURCE_GENERATOR");

        var name = type.TypeName.TrimStart('I') + "_Proxy";
        builder.AddLine("[ActorProxy]");
        builder.BeginScope($"public sealed class {name} : global::Coorth.Framework.ActorProxy, {type.TypeName}");
        
        builder.EndScope();
        builder.AddLine("#endif");
        return builder.ToString();
    }
}