using System.Text;

namespace Coorth.Analyzer; 

public class CodeBuilder {
    
    private readonly StringBuilder builder = new();
    
    private int indent;

    public void Indent(int value) {
        indent += value;
    }

    public void BeginScope(string text) {
        builder.Append('\t', indent).Append(text).AppendLine(" {");
        indent++;
    }
    
    public void EndScope(string? text = null) {
        indent--;
        builder.Append('\t', indent).AppendLine("}");
        if (!string.IsNullOrEmpty(text)) {
            builder.Append('\t', indent).AppendLine(text);
        }
    }
    
    public CodeBuilder AddLine(string text) {
        builder.Append('\t', indent).AppendLine(text);
        return this;
    }
    
    
    public override string ToString() {
        return builder.ToString();
    }
}