namespace Coorth.Analyzer.Define; 

public class ActorBuilder {

    private readonly CodeBuilder builder = new();
    
    public string Generate(TypeDefinition type) {

        builder.AddLine("//Generate code.");
        builder.AddLine("#if false");
        builder.AddLine($"namespace {type.Namespace};");
        builder.AddLine("");
        var name = type.TypeName.TrimStart('I') + "_Proxy";
        builder.AddLine("[Coorth.Framework.Actor]");
        builder.BeginScope($"public sealed class {name} : Coorth.Framework.ActorProxy, {type.FullName}");
        foreach (var method in type.Methods) {
            var parameters = "";
            for (var i = 0; i < method.Params.Count; i++) {
                var parameter = method.Params[i];
                if(i < method.Params.Count - 1) {
                    parameters += parameter.Type + " " + parameter.Name + ", ";
                } else {
                    parameters += parameter.Type + " " + parameter.Name;
                }
            }
            builder.BeginScope($"public {method.Return} {method.Name}({parameters})");
            {
                builder.AddLine("return default;");
            }
            builder.EndScope();
        }
        builder.EndScope();
        builder.AddLine("#endif");
        return builder.ToString();
    }
}