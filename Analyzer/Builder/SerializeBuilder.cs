using System;
using System.Collections.Generic;

namespace Coorth.Analyzer.Define;

public class SerializeBuilder {
    public string Build(TypeDefinition type) {
        var builder = new CodeBuilder();
        builder.AddLine("//Generated code.");
        // builder.AddLine("#if SOURCE_GENERATOR");
        foreach (var use in type.Using) {
            builder.AddLine(use);
        }

        builder.AddLine("");
        builder.AddLine($"namespace {type.Namespace};");
        builder.AddLine("");

        if (type.IsRecord && !type.IsClass) {
            builder.BeginScope($"partial record struct {type.TypeName}");
        } else if (type.IsRecord) {
            builder.BeginScope($"partial record {type.TypeName}");
        } else if (type.IsClass) {
            builder.BeginScope($"partial class {type.TypeName}");
        }
        else {
            builder.BeginScope($"partial struct {type.TypeName}");
        }

        GenerateFormatter(type, builder);

        builder.EndScope();
        // builder.AddLine("#endif");
        return builder.ToString();
    }

    private void GenerateFormatter(TypeDefinition type, CodeBuilder builder) {
        builder.AddLine($"[System.CodeDom.Compiler.GeneratedCode(\"{type.FullName}\",\"1.0.0\")]");
        builder.AddLine($"[Coorth.Serialize.SerializeFormatter(typeof({type.TypeName}))]");
        builder.BeginScope(
            $"public sealed class {type.TypeName}_Formatter : Coorth.Serialize.SerializeFormatter<{type.TypeName}>");
        {
            GenerateWriting(type, builder);
            GenerateReading(type, builder);
        }
        builder.EndScope();
    }

    private void GenerateWriting(TypeDefinition type, CodeBuilder builder) {
        builder.BeginScope(
            $"public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in {type.TypeName} value)");
        {
            builder.BeginScope($"writer.BeginData<{type.TypeName}>({type.Fields.Count});");
            foreach (var field in type.Fields) {
                builder.AddLine($"//Field: {field.Name}, Comment: {field.Comment}");
                builder.AddLine($"writer.WriteTag(nameof({type.TypeName}.{field.Name}), {field.Index});");
                if (field.IsCollection) {
                    switch (field.CollectionType) {
                        case CollectionType.Array: {
                            builder.BeginScope($"writer.BeginList<{field.Children[0].Type}>(value.{field.Name}.Length);");
                            builder.BeginScope($"foreach(var __item__ in value.{field.Name})");
                            {
                                WriteField("__item__", field.Children[0].Type, builder);
                            }
                            builder.EndScope();
                            builder.EndScope("writer.EndList();");
                            break;
                        }
                        case CollectionType.List: {
                            builder.BeginScope($"writer.BeginList<{field.Children[0].Type}>(value.{field.Name}.Count);");
                            builder.BeginScope($"foreach(var __item__ in value.{field.Name})");
                            {
                                WriteField("__item__", field.Children[0].Type, builder);
                            }
                            builder.EndScope();
                            builder.EndScope("writer.EndList();");
                            break;
                        }
                        case CollectionType.Dict: {
                            builder.BeginScope($"writer.BeginDict<{field.Children[0].Type},{field.Children[1].Type}>(value.{field.Name}.Count);");
                            builder.BeginScope($"foreach(var (__key__, __value__) in value.{field.Name})");
                            {
                                builder.AddLine($"writer.WriteKey<{field.Children[0].Type}>(__key__);");
                                builder.AddLine($"writer.WriteValue<{field.Children[1].Type}>(__value__);");
                            }
                            builder.EndScope();
                            builder.EndScope("writer.EndDict();");
                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else {
                    WriteField($"value.{field.Name}", field.Type, builder);
                }
            }
            builder.EndScope("writer.EndData();");
        }
        builder.EndScope();
    }

    private void WriteField(string name, string type, CodeBuilder builder) {
        builder.AddLine(baseTypes.TryGetValue(type, out var method)
            ? $"writer.{method.write}({name});"
            : $"writer.WriteValue<{type}>({name});");
    }

    private void GenerateReading(TypeDefinition type, CodeBuilder builder) {
        var typeName = type.IsClass ? type.TypeName + "?" : type.TypeName;
        builder.BeginScope(
            $"public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref {typeName} value)");
        {
            if (type.IsClass) {
                builder.AddLine($"value ??= new {type.TypeName}();");
            }
            
            builder.BeginScope($"reader.BeginData<{type.TypeName}>();");
            foreach (var field in type.Fields) {
                builder.AddLine($"//Field: {field.Name}, Comment: {field.Comment}");
                builder.AddLine($"reader.ReadTag(nameof({type.TypeName}.{field.Name}), {field.Index});");
                if (field.IsCollection) {
                    builder.BeginScope("");
                 switch (field.CollectionType) {
                        case CollectionType.Array: {
                            builder.BeginScope($"reader.BeginList<{field.Children[0].Type}>(out var __count__);");
                            builder.AddLine($"value.{field.Name} = new {field.Children[0].Type}[__count__];");
                            builder.BeginScope($"for(var i=0; i<__count__; i++)");
                            {
                                ReadField($"value.{field.Name}[i]", field.Children[0].Type, builder);
                            }
                            builder.EndScope();
                            builder.EndScope("reader.EndList();");
                            break;
                        }
                        case CollectionType.List: {
                            builder.BeginScope($"reader.BeginList<{field.Children[0].Type}>(out var __count__);");
                            builder.AddLine($"value.{field.Name} = new System.Collections.Generic.List<{field.Children[0].Type}>(__count__);");
                            builder.BeginScope($"for(var i=0; i<__count__; i++)");
                            {
                                ReadField($"value.{field.Name}[i]", field.Children[0].Type, builder);
                            }
                            builder.EndScope();
                            builder.EndScope("reader.EndList();");
                            break;
                        }
                        case CollectionType.Dict: {
                            builder.BeginScope($"reader.BeginDict<{field.Children[0].Type},{field.Children[1].Type}>(out var __count__);");
                            builder.AddLine($"value.{field.Name} = new System.Collections.Generic.Dictionary<{field.Children[0].Type},{field.Children[1].Type}>(__count__);");
                            builder.BeginScope($"for(var i=0; i<__count__; i++)");
                            {
                                ReadField($"var __key__", field.Children[0].Type, builder);
                                ReadField($"var __value__", field.Children[1].Type, builder);
                                builder.AddLine($"value.{field.Name}.Add(__key__, __value__);");
                            }
                            builder.EndScope();
                            builder.EndScope("reader.EndDict();");
                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                 
                 builder.EndScope();
                }
                
                else {
                    ReadField($"value.{field.Name}", field.Type, builder);
                }
            }

            builder.EndScope("reader.EndData();");
        }
        builder.EndScope();
    }
    
    private void ReadField(string name, string type, CodeBuilder builder) {
        builder.AddLine(baseTypes.TryGetValue(type, out var method)
            ? $"{name} = reader.{method.read}();"
            : $"{name} = reader.ReadValue<{type}>();");
    }

    private readonly Dictionary<string, (string read, string write)> baseTypes = new() {
        [typeof(bool).FullName] = ("ReadBool", "WriteBool"),

        [typeof(sbyte).FullName] = ("ReadInt8", "WriteInt8"),
        [typeof(byte).FullName] = ("ReadUInt8", "WriteUInt8"),
        [typeof(short).FullName] = ("ReadInt16", "WriteInt16"),
        [typeof(ushort).FullName] = ("ReadUInt16", "WriteUInt16"),
        [typeof(int).FullName] = ("ReadInt32", "WriteInt32"),
        [typeof(uint).FullName] = ("ReadUInt32", "WriteUInt32"),
        [typeof(long).FullName] = ("ReadInt64", "WriteInt64"),
        [typeof(ulong).FullName] = ("ReadUInt64", "WriteUInt64"),
        [typeof(decimal).FullName] = ("ReadDecimal", "WriteDecimal"),

        ["System.Half"] = ("ReadFloat16", "WriteFloat16"),
        [typeof(float).FullName] = ("ReadFloat32", "WriteFloat32"),
        [typeof(double).FullName] = ("ReadFloat64", "WriteFloat64"),

        [typeof(char).FullName] = ("ReadChar", "WriteChar"),
        [typeof(string).FullName] = ("ReadString", "WriteString"),

        [typeof(DateTime).FullName] = ("ReadDateTime", "WriteDateTime"),
        [typeof(TimeSpan).FullName] = ("ReadTimeSpan", "WriteTimeSpan"),

        [typeof(Type).FullName] = ("ReadType", "WriteType"),
        [typeof(Guid).FullName] = ("ReadGuid", "WriteGuid"),
    };
}