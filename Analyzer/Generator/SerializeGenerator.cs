﻿using System.Linq;
using Coorth.Analyzer.Define;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Coorth.Analyzer;

[Generator(LanguageNames.CSharp)]
public class SerializeGenerator2 : IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        var types = context.SyntaxProvider.ForAttributeWithMetadataName(AnalyzerUtil.StoreContractAttribute,
            predicate: static (node, _) => node is ClassDeclarationSyntax 
                                                or StructDeclarationSyntax
                                                or RecordDeclarationSyntax
                                                or InterfaceDeclarationSyntax, 
            transform: static (context, _) => context.TargetNode);
        var source = types.Combine(context.CompilationProvider);
        context.RegisterSourceOutput(source, static (context, source) => {
            GenerateType((TypeDeclarationSyntax)source.Left, source.Right, context);
        });
    }

    private static void GenerateType(TypeDeclarationSyntax type, Compilation compilation, SourceProductionContext context) {
        var model = compilation.GetSemanticModel(type.SyntaxTree);
        var symbol = model.GetDeclaredSymbol(type, context.CancellationToken);
        if (symbol == null) {
            return;
        }
        if (AnalyzerUtil.IsNested(type)) {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NestedTypeNotSupport, type.Identifier.GetLocation(), symbol.Name));
            return;
        }
        if (!AnalyzerUtil.IsPartial(type)) {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.TypeMustBePartial, type.Identifier.GetLocation(), symbol.Name));
            return;
        }
        var definition = new TypeDefinition {
            Namespace = symbol.ContainingNamespace.IsGlobalNamespace ? "" : symbol.ContainingNamespace.ToString(),
            TypeName = symbol.Name
        };
        definition.IsRecord = symbol.IsRecord;
        definition.IsClass = type is ClassDeclarationSyntax;
        
        var storeContractSymbol = compilation.GetTypeByMetadataName(AnalyzerUtil.StoreContractAttribute);
        var storeMemberSymbol   = compilation.GetTypeByMetadataName(AnalyzerUtil.StoreMemberAttribute);
        var storeIgnoreSymbol   = compilation.GetTypeByMetadataName(AnalyzerUtil.StoreIgnoreAttribute);
        var attributeData = symbol.GetAttributes().FirstOrDefault(x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, storeContractSymbol));

        var publicField = false;
        var publicProperty = false;
        
        if (attributeData != null && attributeData.ConstructorArguments.Length > 0) {
            var argument = attributeData.ConstructorArguments[0].Value;
            if(argument is int i) {
                publicField = (i & (1 << 1)) != 0;
                publicProperty = (i & (1 << 2)) != 0;
            }
            definition.Using = new[] {$"//{argument} publicField:{publicField} publicProperty:{publicProperty}"};
        }
        // storeContractSymbol.Constructors.First().Parameters.First().
        var index = 1;
        var memberSymbols = AnalyzerUtil.GetMembers(symbol);
        foreach (var memberSymbol in memberSymbols) {
            if(memberSymbol.IsStatic || memberSymbol.IsImplicitlyDeclared || !memberSymbol.CanBeReferencedByName) {
                continue;
            }
            if (storeIgnoreSymbol != null && AnalyzerUtil.HasAttribute(memberSymbol, storeIgnoreSymbol)) {
                continue;
            }
            var export = (storeMemberSymbol != null && AnalyzerUtil.HasAttribute(memberSymbol, storeMemberSymbol));
       
            if (memberSymbol is IFieldSymbol fieldSymbol && (export || publicField)) {
                if (!export && !publicField) {
                    continue;
                }
                var field = GenerateField(fieldSymbol.Name, fieldSymbol.Type, index, compilation);
                definition.Fields.Add(field);
                index++;
            }
            if(memberSymbol is IPropertySymbol propertySymbol && propertySymbol.GetMethod != null && propertySymbol.SetMethod != null && (export || publicProperty)) {
                var field = GenerateField(propertySymbol.Name, propertySymbol.Type, index, compilation);
                definition.Fields.Add(field);
                index++;
            }
        }
        
        var builder = new SerializeBuilder();
        var source = builder.Build(definition);
        context.AddSource($"{type.Identifier}.Serialize.cs", source);
    }

    private static FieldDefinition GenerateField(string name, ITypeSymbol type, int index, Compilation compilation) {
        var field = new FieldDefinition();
        field.Name = name;
        field.Type = AnalyzerUtil.GetFullName(type);
        field.Index = index;
        field.Comment = $"{type} - {type.TypeKind}";
        if (type.TypeKind == TypeKind.Array && type is IArrayTypeSymbol arrayType) {
            field.IsCollection = true;
            field.CollectionType = CollectionType.Array;
            field.Children = new[] { new GenericDefinition(){Type = AnalyzerUtil.GetFullName(arrayType.ElementType) } };
            field.Comment = $"{type} - {type.TypeKind} - {AnalyzerUtil.GetFullName(arrayType.ElementType)}";
        }
        else if (type.TypeKind == TypeKind.Class && type is INamedTypeSymbol namedType && namedType.IsGenericType) {
            var list = compilation.GetTypeByMetadataName("System.Collections.Generic.List`1")?.ConstructUnboundGenericType();
            if (list != null && AnalyzerUtil.EqualsGenericType(namedType, list)) {
                field.IsCollection = true;
                field.CollectionType = CollectionType.List;
                field.Children = new[] { new GenericDefinition() {
                    Type = AnalyzerUtil.GetFullName(namedType.TypeArguments[0])
                } };
            }
            var dict = compilation.GetTypeByMetadataName("System.Collections.Generic.Dictionary`2")?.ConstructUnboundGenericType();
            if (dict != null && AnalyzerUtil.EqualsGenericType(namedType, dict)) {
                field.IsCollection = true;
                field.CollectionType = CollectionType.Dict;
                field.Children = new[] { new GenericDefinition() {
                    Type = AnalyzerUtil.GetFullName(namedType.TypeArguments[0])
                }, new GenericDefinition() {
                    Type = AnalyzerUtil.GetFullName(namedType.TypeArguments[1])
                }};
            }
        }
        return field;
    }
    
}