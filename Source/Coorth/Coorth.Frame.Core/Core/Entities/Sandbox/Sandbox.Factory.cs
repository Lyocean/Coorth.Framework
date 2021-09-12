using System.Collections.Generic;

namespace Coorth {
    public partial class Sandbox {

        private readonly Dictionary<int, IEntityFactory> factories = new Dictionary<int, IEntityFactory>();

        public void Bind<TFactory>(int key) where TFactory : class, IEntityFactory, new() {
            var factory = new TFactory();
            factories.Add(key, factory);
        }

        public Entity CreateEntity(int entityType, int entityKey) {
            if (factories.TryGetValue(entityType, out var factory)) {
                return factory.Create(this, entityKey);
            }
            return Entity.Null;
        }
    }
}