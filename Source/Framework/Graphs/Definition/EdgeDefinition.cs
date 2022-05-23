namespace Coorth.Framework; 

public record EdgeDefinition(PortDefinition Source, PortDefinition Target) {
        
    public readonly PortDefinition Source = Source;
        
    public readonly PortDefinition Target = Target;
}