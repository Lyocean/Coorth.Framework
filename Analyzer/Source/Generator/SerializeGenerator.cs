using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Coorth.Analyzer; 

[Generator]
public class SerializeGenerator : ISourceGenerator {
    public void Initialize(GeneratorInitializationContext context) {
        context.RegisterForSyntaxNotifications(() => new SerializeSyntaxReceiver());
    }

    private NamespaceDeclarationSyntax? GetNamespace(SyntaxNode type) {
        while (type.Parent != null) {
            type = type.Parent;
            if (type is NamespaceDeclarationSyntax @namespace) {
                return @namespace;
            }
        }
        return null;
    }
    
    public void Execute(GeneratorExecutionContext context) {
        // context.SyntaxReceiver is CustomSyntaxReceiver syntaxReceiver
        var types = GetTypeWithAttribute(context.Compilation);
        foreach (var type in types) {
            var @namespace = GetNamespace(type);
            if (@namespace == null) {
                continue;
            }
            var builder = new StringBuilder();
            var typeName = type.Identifier.ToString();
            builder.Append(@$"
using Coorth;
using Coorth.Serialize;

namespace _Gen;

[Generated]
public partial {type.Keyword} {typeName} {{

    [Serializer(typeof({typeName})]
    public class Serializer : Serializer<{typeName}> {{

        public override void Write(SerializeWriter writer, in {typeName} value) {{
");
            //Write
            foreach (var member in type.Members.Where(_=>_.AttributeLists.SelectMany(_=>_.Attributes).Any(_=>_.Name.ToString() == "StoreMember"))) {
                if (member is FieldDeclarationSyntax field) {
                    
                }
                if (member is PropertyDeclarationSyntax property) {
                    
                }
            }
            builder.Append(@$"
        }}
        public override {typeName} Read(SerializeReader reader, {typeName} value) {{
");
            //Read
            
            builder.Append(@$"
        }}
    }}
}}
");
        }
    }

    private static List<TypeDeclarationSyntax> GetTypeWithAttribute(Compilation compilation) {
        var result = new List<TypeDeclarationSyntax>();
        var types = compilation.SyntaxTrees.SelectMany(_ => _.GetRoot().DescendantNodes()).OfType<TypeDeclarationSyntax>();
        foreach (var type in types) {
            if (!type.ChildTokens().Any(_ => _.IsKind(SyntaxKind.PartialKeyword))) {
                continue;
            }
            var attribute = type.AttributeLists.SelectMany(_ => _.Attributes).FirstOrDefault(_ => _.Name.ToString() == "StoreContract");
            if (attribute == null) {
                continue;
            }
            result.Add(type);
        }
        return result;
    }
    
    
}

internal class SerializeSyntaxReceiver : ISyntaxReceiver {
    public void OnVisitSyntaxNode(SyntaxNode syntaxNode) {
        throw new System.NotImplementedException();
    }
}