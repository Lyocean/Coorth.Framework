using System.Collections.Generic;
using System.Linq;
using Coorth.Analyzer.Define;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace Coorth.Analyzer; 

public static class AnalyzerUtil {

    public static string ActorAttribute => "Coorth.Framework.ActorAttribute";

    public static string StoreContractAttribute => "Coorth.StoreContractAttribute";
    
    public static string StoreMemberAttribute => "Coorth.StoreMemberAttribute";

    public static string StoreIgnoreAttribute => "Coorth.StoreIgnoreAttribute";

    public static bool IsPartial(TypeDeclarationSyntax type) {
        return type.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword));
    }
    
    public static bool IsNested(TypeDeclarationSyntax type) {
        return type.Parent is TypeDeclarationSyntax;
    }

    public static bool IsRecord(TypeDeclarationSyntax type) {
        return type is RecordDeclarationSyntax;
    }

    public static bool IsClass(TypeDeclarationSyntax type) {
        return type is ClassDeclarationSyntax;
    }
    
    public static bool IsStruct(TypeDeclarationSyntax type) {
        return type is StructDeclarationSyntax;
    }

    public static IEnumerable<ISymbol> GetMembers(INamedTypeSymbol symbol) {
        if (symbol.BaseType != null) {
            foreach (var member in GetMembers(symbol.BaseType)) {
                if (member.IsOverride) {
                    continue;
                }
                yield return member;
            }
        }

        foreach (var member in symbol.GetMembers()) {
            if (member.IsOverride) {
                continue;
            }
            yield return member;
        }
    }

    public static bool HasAttribute(TypeDeclarationSyntax type, string attribute) {
        return type.AttributeLists.SelectMany(x => x.Attributes)
            .Select(x => x.Name)
            .OfType<IdentifierNameSyntax>()
            .Select(x => x.Identifier.Text)
            .Any(x => x == attribute);
    }

    public static bool HasAttribute(ISymbol symbol, INamedTypeSymbol attribute) {
        return symbol.GetAttributes().Any(x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, attribute));
    }

    public static string GetFullName(ITypeSymbol type) {
        var name = type.ContainingNamespace == null ? type.Name : GetFullName(type.ContainingNamespace) + "." + type.Name;
        if(type is INamedTypeSymbol namedTypeSymbol && namedTypeSymbol.IsGenericType && namedTypeSymbol.TypeArguments.Length > 0) {
            name += "<";
            for (var i = 0; i < namedTypeSymbol.TypeArguments.Length; i++) {
                var typeArgument = namedTypeSymbol.TypeArguments[i];
                if(i < namedTypeSymbol.TypeArguments.Length - 1) {
                    name += GetFullName(typeArgument) + ", ";
                } else {
                    name += GetFullName(typeArgument);
                }
            }
            name += ">";
        }
        return name;
    }
    
    private static string GetFullName(INamespaceSymbol ns) {
        if (ns.IsGlobalNamespace) {
            return ns.Name;
        }
        var parentName = GetFullName(ns.ContainingNamespace);
        return !string.IsNullOrEmpty(parentName) ? parentName + "." + ns.Name : ns.Name;
    }
    
    public static bool EqualsGenericType(INamedTypeSymbol a, INamedTypeSymbol b) {
        var l = a.IsGenericType ? a.ConstructUnboundGenericType() : a;
        var r = b.IsGenericType ? b.ConstructUnboundGenericType() : b;
        return SymbolEqualityComparer.Default.Equals(l, r);
    }
    
}