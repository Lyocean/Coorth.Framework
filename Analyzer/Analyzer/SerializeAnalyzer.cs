using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Coorth.Analyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class SerializeAnalyzer : DiagnosticAnalyzer {
    
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } 
        = ImmutableArray.Create(DiagnosticDescriptors.TypeMustBePartial, DiagnosticDescriptors.NestedTypeNotSupport);

    public override void Initialize(AnalysisContext context) {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.RegisterSyntaxNodeAction(AnalyzeTypeDeclaration, SyntaxKind.ClassDeclaration, SyntaxKind.StructDeclaration);
    }

    private void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext context) {
        var typeDeclaration = (TypeDeclarationSyntax) context.Node;
        var symbol = context.SemanticModel.GetDeclaredSymbol(typeDeclaration);
        if (symbol == null) {
            return;
        }
        var storeContractSymbol = context.Compilation.GetTypeByMetadataName(AnalyzerUtil.StoreContractAttribute);
        if(storeContractSymbol != null && !AnalyzerUtil.HasAttribute(symbol, storeContractSymbol)) {
            return;
        }
        if (symbol.ContainingType != null) {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NestedTypeNotSupport, typeDeclaration.Identifier.GetLocation(), symbol.Name));
            return;
        }
        
        if (!AnalyzerUtil.IsPartial(typeDeclaration)) {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.TypeMustBePartial, typeDeclaration.Identifier.GetLocation(), symbol.Name));
        }
    }

}