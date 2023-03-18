using System;
using Coorth.Analyzer.Define;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Coorth.Analyzer; 

[Generator(LanguageNames.CSharp)]
public class ActorGenerator : IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        var types = context.SyntaxProvider.ForAttributeWithMetadataName(AnalyzerUtil.ActorAttribute,
            predicate: static (node, _) => node is InterfaceDeclarationSyntax, 
            transform: static (context, _) => context.TargetNode);
        var source = types.Combine(context.CompilationProvider);
        context.RegisterSourceOutput(source, static (context, source) => {
            Generate((TypeDeclarationSyntax)source.Left, source.Right, context);
        });
    }

    private static void Generate(TypeDeclarationSyntax type, Compilation compilation, SourceProductionContext context) {
        var model = compilation.GetSemanticModel(type.SyntaxTree);
        var symbol = model.GetDeclaredSymbol(type, context.CancellationToken);
        if (symbol == null) {
            return;
        }
        if (AnalyzerUtil.IsNested(type)) {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NestedTypeNotSupport, type.Identifier.GetLocation(), symbol.Name));
            return;
        }

        var definition = new TypeDefinition();
        definition.Namespace = symbol.ContainingNamespace.IsGlobalNamespace ? "" : symbol.ContainingNamespace.ToString();
        definition.TypeName = symbol.Name;
        
        var builder = new ActorBuilder();
        var source = builder.Generate(definition);
        context.AddSource($"{symbol.Name}.ActorProxy.cs", source);
    }
}

