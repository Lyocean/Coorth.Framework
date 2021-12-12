using System.Collections.Generic;

namespace Coorth {
    public interface IEntityFactory {
        Entity Create(Sandbox sandbox, int key);
        bool Recycle(Entity entity);
    }

    public abstract class EntityFactory {

        protected Dictionary<Sandbox, Archetype> archetypes = new Dictionary<Sandbox, Archetype>();

        protected virtual void GetArchetype(Sandbox sandbox) {
            
        }
    }
}