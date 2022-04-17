namespace Coorth {
    public class WorldConfig {
        public string Name { get; set; } = "World";

        public ActorSetting Actor { get; set; } = ActorSetting.Default;
        
        public SandboxConfig Sandbox { get; set; } = SandboxConfig.Default;
        
    }
}