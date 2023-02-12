using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Coorth.Analyzer;

[Generator(LanguageNames.CSharp)]
public class SerializeGenerator : ISourceGenerator {
    public void Initialize(GeneratorInitializationContext context) {
        context.RegisterForSyntaxNotifications(() => new SerializeSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context) {
        if (context.SyntaxReceiver is not SerializeSyntaxReceiver syntaxReceiver || syntaxReceiver.Types.Count == 0) {
            return;
        }
        foreach (var type in syntaxReceiver.Types) {
            var source = Generate(context, type);
            context.AddSource($"{type.Identifier.Text}.Gen.cs", source);
        }
    }

    private string Generate(GeneratorExecutionContext context, TypeDeclarationSyntax type) {
        var builder = new StringBuilder();
        var nameSpace = GetNamespace(type);
        var typeName = type.Identifier.Text;
        
        //{nameSpace}
        builder.AppendLine(@$"//Generated code.
using System;
using Coorth.Serialize;
using ISerializeWriter = global::Coorth.Serialize.ISerializeWriter;
using ISerializeReader = global::Coorth.Serialize.ISerializeReader;


namespace {nameSpace?.Name};

partial {type.Keyword} {typeName} : global::Coorth.Serialize.ISerializable<{typeName}> {{
");
        //Writing
        builder.AppendLine(@$"
    public void SerializeWriting(ISerializeWriter writer) {{
        writer.BeginData<{typeName}>(1);
        {{");
        GenerateWrite(context, type, builder);
        builder.AppendLine(@$"        }}
        writer.EndData();
    }}


    public static void SerializeWriting(in ISerializeWriter writer, scoped in {typeName} value) {{
        if(value == null) {{
            writer.WriteNull();
            return;
        }}
        value.SerializeWriting(writer);
    }}
");
        //Reading
        builder.AppendLine(@$"
    public void SerializeReading(ISerializeReader reader) {{
        reader.BeginData<{typeName}>();
        {{");
        GenerateRead(context, type, builder);
        builder.AppendLine(@$"        }}
        reader.EndData();
    }}

    public static void SerializeReading(in ISerializeReader reader, scoped ref {typeName} value) {{
        if(reader.ReadNull()){{
            value = null;
            return;
        }}
        value = default;
        value.SerializeReading(reader);
    }}
");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private void GenerateWrite(GeneratorExecutionContext context, TypeDeclarationSyntax type, StringBuilder builder) {
        // builder.AppendLine("/*");
        var indent = 3;
        foreach (var (member_type, member_identifier, member_attribute) in GetMembers(type)) {
            var number = member_attribute?.ArgumentList?.Arguments.First().ToString() ?? "0";
            builder.Append('\t', indent).AppendLine($"writer.WriteTag(nameof({member_identifier.Text}), {number});");
            builder.Append('\t', indent).AppendLine($"writer.Write{Type2Prefix(member_type)}({member_identifier.Text});");
        }
        // builder.AppendLine("*/");
    }

    
    private void GenerateRead(GeneratorExecutionContext context, TypeDeclarationSyntax type, StringBuilder builder) {
        // builder.AppendLine("/* 1");
        var indent = 3;
        foreach (var (member_type, member_identifier, member_attribute) in GetMembers(type)) {
            var number = member_attribute?.ArgumentList?.Arguments.First().ToString() ?? "0";
            builder.Append('\t', indent).AppendLine($"reader.ReadTag(nameof({member_identifier.Text}), {number});");
            // GeneratorUtil.ReportDiagnostic(context, member_type, $"member_type:{member_type.Kind()}");
            builder.Append('\t', indent).AppendLine($"{member_identifier.Text} = reader.Read{Type2Prefix(member_type)}();");
        }
        // builder.AppendLine("*/");
    }

    private string Type2Prefix(TypeSyntax type) {
        if (type.IsKind(SyntaxKind.PredefinedType)) {
            var keyword = ((PredefinedTypeSyntax)type).Keyword;
            return keyword.Kind() switch {
                SyntaxKind.BoolKeyword => "Bool",
                SyntaxKind.ByteKeyword => "UInt8",
                SyntaxKind.SByteKeyword => "Int8",
                SyntaxKind.ShortKeyword => "Int16",
                SyntaxKind.UShortKeyword => "UInt16",
                SyntaxKind.IntKeyword => "Int32",
                SyntaxKind.UIntKeyword => "UInt32",
                SyntaxKind.LongKeyword => "Int64",
                SyntaxKind.ULongKeyword => "UInt64",
                SyntaxKind.FloatKeyword => "Float32",
                SyntaxKind.DoubleKeyword => "Float64",
                SyntaxKind.CharKeyword => "Char",
                SyntaxKind.StringKeyword => "String",
                SyntaxKind.DecimalKeyword => "Decimal",
                _ => $"<{type}>",
            };
        }
        return $"<{type}>";
    }
    private IEnumerable<(TypeSyntax, SyntaxToken, AttributeSyntax?)> GetMembers(TypeDeclarationSyntax type) {
        foreach (var member in type.Members) {
            var attributes = member.AttributeLists.SelectMany(_=>_.Attributes);
            var attribute = attributes.FirstOrDefault(_ => _.Name.ToString() is "StoreMember" or "StoreMemberAttribute");
            // if (attribute == null) {
            //     continue;
            // }
            switch (member) {
                case FieldDeclarationSyntax field:
                    foreach (var variable in field.Declaration.Variables) {
                        yield return (field.Declaration.Type, variable.Identifier, attribute);
                    }
                    break;
                case PropertyDeclarationSyntax property:
                    yield return (property.Type, property.Identifier, attribute);
                    break;
            }
        }
    }

    private BaseNamespaceDeclarationSyntax? GetNamespace(SyntaxNode type) {
        while (type.Parent != null) {
            type = type.Parent;
            if (type is BaseNamespaceDeclarationSyntax nameSpace) {
                return nameSpace;
            }
        }
        return null;
    }
}

internal class SerializeSyntaxReceiver : ISyntaxReceiver {
    public readonly HashSet<TypeDeclarationSyntax> Types = new();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode) {
        if (syntaxNode is not TypeDeclarationSyntax type) {
            return;
        }
        if (!type.Modifiers.Any(_ => _.IsKeyword() && _.IsKind(SyntaxKind.PartialKeyword))) {
            return;
        }
        if (type.AttributeLists.SelectMany(_ => _.Attributes).Any(Match)) {
            Types.Add(type);
        }
    }

    private static bool Match(AttributeSyntax attribute) {
        var name = attribute.Name.ToString();
        return name is "StoreContract" or "StoreContractAttribute" ; //or "DataContract" or "DataContractAttribute"
    }
}