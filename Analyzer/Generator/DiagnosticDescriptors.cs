using Microsoft.CodeAnalysis;

namespace Coorth.Analyzer;

internal static class DiagnosticDescriptors {
    
    private const string CATEGORY = "Generate";

    public static readonly DiagnosticDescriptor TypeMustBePartial = new(
        id: "COORTH001",
        title: "Type must be partial",
        messageFormat: "Type '{0}' must be partial",
        category: CATEGORY,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
    
    public static readonly DiagnosticDescriptor NestedTypeNotSupport = new(
        id: "COORTH002",
        title: "Type must be partial",
        messageFormat: "Type '{0}' must be partial",
        category: CATEGORY,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
}