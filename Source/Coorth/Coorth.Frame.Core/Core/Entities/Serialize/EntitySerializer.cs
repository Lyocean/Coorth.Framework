namespace Coorth {
    public interface IComponentSerializer {
        void ReadComponent<T>(out T component);
        void WriteComponent<T>(ref T component);
    }
    
    public class EntitySerializer : IComponentSerializer {
    
        private ISerializeReader reader;
        private ISerializeWriter writer;
        
        
        //
        // public void Write(Span<Entity> entities) {
        //     foreach (Entity entity in entities) {
        //         Write(entity);
        //     }
        // }
        //
        // public Entity[] Read(Sandbox sandbox) {
        //     var entities = new List<Entity>();
        //     var entityCount = reader.ReadInt32();
        //     while (!reader.IsFinish()) {
        //         var count = reader.ReadUInt16();
        //         var entity = sandbox.CreateEntity();
        //         entities.Add(entity);
        //         if (count == 0) {
        //             continue;
        //         }
        //         for (var i = 0; i < count; i++) {
        //             var type = reader.ReadType();
        //             sandbox.ReadComponent(entity, type, this);
        //         }
        //     }
        //     return entities.ToArray();
        // }

        public void Write(Entity entity) {
            var sandbox = entity.Sandbox;
            ref var context = ref entity.GetContext();
            foreach (var pair in context.Components) {
                sandbox.WriteComponent(pair.Key, pair.Value, this);
            }
        }

        public void Read(Sandbox sandbox, out Entity entity) {
            entity = sandbox.CreateEntity();
            var count = reader.ReadUInt16();
            for (var i = 0; i < count; i++) {
                var type = reader.ReadType();
                sandbox.ReadComponent(entity, type, this);
            }
        }
        
        public void ReadComponent<T>(out T component) {
            component = reader.ReadValue<T>();
        }

        public void WriteComponent<T>(ref T component) {
            writer.WriteValue(component);
        }
    }

}