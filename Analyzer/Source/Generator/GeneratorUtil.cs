using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace Coorth.Analyzer; 

public static class GeneratorUtil {

    // public static bool IsPartial(TypeDeclarationSyntax type) {
    //     return type.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword));
    // }
    //
    // public static bool IsNested(TypeDeclarationSyntax typeDeclaration) {
    //     return typeDeclaration.Parent is TypeDeclarationSyntax;
    // }
    //
    public static void ReportDiagnostic(GeneratorExecutionContext context, TypeSyntax type, string content) {
        var descriptor = new DiagnosticDescriptor(
                id: "SerializeDebug001", 
                title: "SerializeDebug", 
                messageFormat: "{0}", 
                category: "SerializeDebug",
                defaultSeverity: DiagnosticSeverity.Error, 
                isEnabledByDefault: true);
        var diagnostic = Diagnostic.Create(descriptor, type.GetLocation(), content);
        context.ReportDiagnostic(diagnostic);
    }

}