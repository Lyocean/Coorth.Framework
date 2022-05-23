using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Coorth.Analyzer; 

[Generator]
public class SerializeGenerator : ISourceGenerator {
    public void Initialize(GeneratorInitializationContext context) {
    }

    public void Execute(GeneratorExecutionContext context) {
        var classes = GetClassWithAttribute(context.Compilation);
        foreach (var cds in classes) {
            
            var sourceText = SourceText.From($@"
            using Coorth;
            
            name Coorth._Gen;            

            public partial class {cds.Identifier} {{

            }}", Encoding.UTF8);
         
            context.AddSource($"{cds.Identifier.ValueText}.Generate.cs", sourceText);
        }
    }

    private List<ClassDeclarationSyntax> GetClassWithAttribute(Compilation compilation) {
        var result = new List<ClassDeclarationSyntax>();
        var nodes = compilation.SyntaxTrees.SelectMany(_ => _.GetRoot().DescendantNodes());
        var classes = nodes.Where(_=>_.IsKind(SyntaxKind.ClassDeclaration)).OfType<ClassDeclarationSyntax>();
        foreach (var cds in classes) {
            if (cds.AttributeLists.SelectMany(_ => _.Attributes).Any(_ => _.Name.ToFullString() == "ActorProxy22")) {
                result.Add(cds);
            }
        }
        return result;
    }
}