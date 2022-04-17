namespace Coorth {
    public class SandboxConfig {

        public string Name { get; set; } = "Sandbox-Default";

        public (int Index, int Chunk) EntityCapacity { get; set; } = (64, 64);
        
        public (int Index, int Chunk) ArchetypeCapacity { get; set; } = (64, 64);

        public int ComponentGroupCapacity { get; set; } = 32;

        public (int Index, int Chunk) ComponentDataCapacity { get; set; } = (2, 128);
        
        public static SandboxConfig Default => new SandboxConfig();
    }
}