namespace Coorth {
    public class SandboxConfig {

        public (int Index, int Chunk) EntityCapacity = (64, 64);
        
        public (int Index, int Chunk) ArchetypeCapacity = (64, 64);

        public int ComponentGroupCapacity = 32;

        public (int Index, int Chunk) ComponentDataCapacity = (2, 128);
        
        public static SandboxConfig Default => new SandboxConfig();
    }
}