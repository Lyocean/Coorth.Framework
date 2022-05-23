using Coorth.Logs;

namespace Coorth.Framework; 

public record SandboxOptions {

    public string Name { get; set; } = "Sandbox-Default";

    public (int Index, int Chunk) EntityCapacity { get; set; } = (64, 64);
        
    public (int Index, int Chunk) ArchetypeCapacity { get; set; } = (64, 64);

    public int ComponentGroupCapacity { get; set; } = 32;

    public (int Index, int Chunk) ComponentDataCapacity { get; set; } = (2, 128);

    public IServiceLocator? Services { get; set; } = null;
    
    public ILogger? Logger { get; set; } = null;
    
    public Dispatcher? Dispatcher { get; set; } = null;
    
    public static SandboxOptions Default => new();
    
}