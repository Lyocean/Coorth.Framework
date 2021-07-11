namespace Coorth {
    public class SandboxConfig {

        public (int Index, int Chunk) EntityCapacity = (64, 4096);

        public int ComponentGroupCapacity = 32;

        public (int Index, int Chunk) ComponentDataCapacity = (2, 4096);
        
        public static SandboxConfig Default => new SandboxConfig();
    }
}