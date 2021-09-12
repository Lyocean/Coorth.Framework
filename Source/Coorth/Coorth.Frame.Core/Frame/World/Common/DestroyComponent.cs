namespace Coorth {
    public struct DestroyComponent : IComponent {
        public int DelayFrameCount;
        public readonly IEntityFactory RecycleFactory;

        public DestroyComponent(int delayFrameCount, IEntityFactory recycleFactory) {
            DelayFrameCount = delayFrameCount;
            RecycleFactory = recycleFactory;
        }
    }

    public static class DestroyExtension {
        public static void Destroy(this Entity entity) {
            entity.Add<DestroyComponent>();
        }
        
        public static void Destroy(this Entity entity, int delayFrame) {
            entity.Add<DestroyComponent>(new DestroyComponent());
        }
    }
}