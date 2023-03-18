// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Coorth.Analyzer.Define;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.CSharp;
// using Microsoft.CodeAnalysis.CSharp.Syntax;
//
// namespace Coorth.Analyzer;
//
// [Generator(LanguageNames.CSharp)]
// public class SerializeGenerator : ISourceGenerator {
//     public void Initialize(GeneratorInitializationContext context) {
//         context.RegisterForSyntaxNotifications(() => new SerializeSyntaxReceiver());
//     }
//
//     public void Execute(GeneratorExecutionContext context) {
//         if (context.SyntaxReceiver is not SerializeSyntaxReceiver syntaxReceiver || syntaxReceiver.Types.Count == 0) {
//             return;
//         }
//
//         foreach (var type in syntaxReceiver.Types) {
//             var source = Generate(context, type);
//             context.AddSource($"{type.Identifier.Text}.Serialize.cs", source);
//         }
//     }
//
//     private string Generate(GeneratorExecutionContext context, TypeDeclarationSyntax type) {
//         var definition = new TypeDefinition();
//         definition.Using = GetUsing(type).ToArray();
//         definition.Namespace = GetNamespace(type)?.Name.ToString() ?? string.Empty;
//         definition.TypeName = type.Identifier.ToString();
//
//         definition.IsRecord = AnalyzerUtil.IsRecord(type);
//         definition.IsClass = AnalyzerUtil.IsClass(type);
//         
//         if (type is RecordDeclarationSyntax recordSyntax) {
//             definition.IsRecord = true;
//             definition.IsClass = recordSyntax.ClassOrStructKeyword.IsKind(SyntaxKind.ClassDeclaration);
//         }
//         else {
//             definition.IsRecord = false;
//             definition.IsClass = type is ClassDeclarationSyntax;
//         }
//
//         var index = 0;
//         foreach (var member in type.Members) {
//             if (member is FieldDeclarationSyntax fieldSyntax) {
//                 if (!IsExport(fieldSyntax)) {
//                     continue;
//                 }
//
//                 var field = new FieldDefinition(
//                     name: fieldSyntax.Declaration.Variables.First().Identifier.ToString(),
//                     type: GetFieldType(fieldSyntax.Declaration.Type) + ":" + fieldSyntax.Declaration.Type.Kind(),
//                     index: index++);
//                 definition.Fields.Add(field);
//                 continue;
//             }
//
//             if (member is PropertyDeclarationSyntax propertySyntax) {
//                 continue;
//             }
//         }
//
//         var builder = new SerializeBuilder();
//         var source = builder.Build(definition);
//         return source;
//     }
//
//     private IEnumerable<(TypeSyntax, SyntaxToken, AttributeSyntax?)> GetMembers(TypeDeclarationSyntax type) {
//         foreach (var member in type.Members) {
//             var attributes = member.AttributeLists.SelectMany(_ => _.Attributes);
//             var attribute =
//                 attributes.FirstOrDefault(_ => _.Name.ToString() is "StoreMember" or "StoreMemberAttribute");
//             // if (attribute == null) {
//             //     continue;
//             // }
//             switch (member) {
//                 case FieldDeclarationSyntax field:
//                     foreach (var variable in field.Declaration.Variables) {
//                         yield return (field.Declaration.Type, variable.Identifier, attribute);
//                     }
//
//                     break;
//                 case PropertyDeclarationSyntax property:
//                     yield return (property.Type, property.Identifier, attribute);
//                     break;
//             }
//         }
//     }
//
//     private static IEnumerable<string> GetUsing(SyntaxNode type) {
//         while (type.Parent != null) {
//             type = type.Parent;
//             if (type is CompilationUnitSyntax compilation) {
//                 foreach (var compilationUsing in compilation.Usings) {
//                     yield return compilationUsing.ToString();
//                 }
//             }
//         }
//     }
//
//     private static BaseNamespaceDeclarationSyntax? GetNamespace(SyntaxNode type) {
//         while (type.Parent != null) {
//             type = type.Parent;
//             if (type is BaseNamespaceDeclarationSyntax nameSpace) {
//                 return nameSpace;
//             }
//         }
//
//         return null;
//     }
//
//     private static bool IsExport(FieldDeclarationSyntax field) {
//         foreach (var modifier in field.Modifiers) {
//             if (modifier.IsKind(SyntaxKind.StaticKeyword)) {
//                 return false;
//             }
//         }
//
//         return true;
//     }
//
//     private static string GetFieldType(TypeSyntax type) {
//         return type.Kind() switch {
//             SyntaxKind.BoolKeyword => typeof(bool).FullName,
//
//             SyntaxKind.SByteKeyword => typeof(sbyte).FullName,
//             SyntaxKind.ByteKeyword => typeof(byte).FullName,
//             SyntaxKind.ShortKeyword => typeof(short).FullName,
//             SyntaxKind.UShortKeyword => typeof(ushort).FullName,
//             SyntaxKind.IntKeyword => typeof(int).FullName,
//             SyntaxKind.UIntKeyword => typeof(uint).FullName,
//             SyntaxKind.LongKeyword => typeof(long).FullName,
//             SyntaxKind.ULongKeyword => typeof(ulong).FullName,
//
//             SyntaxKind.DecimalKeyword => typeof(decimal).FullName,
//
//             SyntaxKind.FloatKeyword => typeof(float).FullName,
//             SyntaxKind.DoubleKeyword => typeof(double).FullName,
//
//             SyntaxKind.CharKeyword => typeof(char).FullName,
//             SyntaxKind.StringKeyword => typeof(string).FullName,
//
//             SyntaxKind.TypeKeyword => typeof(Type).FullName,
//
//             _ => type.ToString(),
//         };
//     }
// }
//
// internal class SerializeSyntaxReceiver : ISyntaxReceiver {
//     public readonly HashSet<TypeDeclarationSyntax> Types = new();
//
//     public void OnVisitSyntaxNode(SyntaxNode syntaxNode) {
//         if (syntaxNode is not TypeDeclarationSyntax type) {
//             return;
//         }
//
//         if (!type.Modifiers.Any(_ => _.IsKeyword() && _.IsKind(SyntaxKind.PartialKeyword))) {
//             return;
//         }
//
//         if (type.AttributeLists.SelectMany(_ => _.Attributes).Any(Match)) {
//             Types.Add(type);
//         }
//     }
//
//     private static bool Match(AttributeSyntax attribute) {
//         var name = attribute.Name.ToString();
//         return name is "StoreContract" or "StoreContractAttribute"; //or "DataContract" or "DataContractAttribute"
//     }
// }