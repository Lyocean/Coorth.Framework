namespace Coorth.ECS {
    public class EcsConfig {
        public int EntityCapacity = 1024;

        public int EntityCache = 1024;

        public int ComponentCapacity = 16;
        public int ComponentCache = 16;

        public int ComponentsCapacity = 64;

        public static EcsConfig Default => new EcsConfig();
    }
}
