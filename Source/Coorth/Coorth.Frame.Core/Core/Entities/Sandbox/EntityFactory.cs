namespace Coorth {
    public interface IEntityFactory {
        Entity Create(Sandbox sandbox, int key);
        bool Recycle(Entity entity);
    }
}