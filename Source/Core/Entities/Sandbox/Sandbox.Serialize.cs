using System;
using System.Collections.Generic;
using System.Linq;

namespace Coorth {
    public partial class Sandbox {

        #region Read Write Component

        public void ReadComponent(SerializeReader reader, in EntityId entityId, Type type) {
            ref var context = ref GetContext(entityId.Index);
            var componentGroup = GetComponentGroup(type);
            if(context.TryGet(componentGroup.TypeId, out var componentIndex)) {
                componentGroup.ReadComponent(reader, componentIndex);
                componentGroup.OnComponentModify(entityId, componentIndex);
            } else {
                componentIndex = componentGroup.AddComponent(context.GetEntity(this));
                componentGroup.ReadComponent(reader, componentIndex);
                OnEntityAddComponent(ref context, componentGroup.TypeId, componentIndex);
                componentGroup.OnComponentAdd(entityId, componentIndex);
            }
        }
        
        public void WriteComponent(SerializeWriter writer, in EntityId entityId, Type type) {
            ref var context = ref GetContext(entityId.Index);
            var componentGroup = GetComponentGroup(type);
            componentGroup.WriteComponent(writer, context[componentGroup.TypeId]);
        }

        #endregion

        #region Read Write Entity

        public void ReadEntity(SerializeReader reader, EntityId entityId) {
            reader.BeginScope(typeof(Entity), SerializeScope.Class);
            var count = reader.ReadField<int>(nameof(Entity.Count), 1);
            reader.ReadTag(nameof(EntityContext.Components), 2);
            reader.BeginDict(typeof(Type), typeof(IComponent), out var _);
            for (var i = 0; i < count; i++) {
                var type = reader.ReadKey<Type>();
                ReadComponent(reader, entityId, type);
            }
            reader.EndDict();
            reader.EndScope();
        }
        
        public Entity ReadEntity(SerializeReader reader) {
            var entity = CreateEntity();
            ReadEntity(reader, entity.Id);
            return entity;
        }

        public void WriteEntity(SerializeWriter writer, in EntityId entityId) {
            writer.BeginScope(typeof(Entity), SerializeScope.Class);
            ref var context = ref GetContext(entityId.Index);
            writer.WriteField(nameof(Entity.Count), 1, context.Count);
            writer.WriteTag(nameof(EntityContext.Components), 2);
            writer.BeginDict(typeof(Type), typeof(IComponent), context.Count);
            foreach (var pair in context.Components) {
                var componentGroup = GetComponentGroup(pair.Key);
                writer.WriteKey(componentGroup.Type);
                componentGroup.WriteComponent(writer, pair.Value);
            }
            writer.EndDict();
            writer.EndScope();
        }

        public void WriteEntities(SerializeWriter writer, IEnumerable<Entity> entities) {
            var list = (entities as IList<Entity>) ?? entities.ToArray();
            writer.BeginList(typeof(Entity), list.Count);
            foreach (var entity in list) {
                WriteEntity(writer, entity.Id);
            }
            writer.EndList();
        }

        public List<Entity> ReadEntities(SerializeReader reader) {
            reader.BeginList<Entity>(out var count);
            if (count >= 0) {
                var list = new List<Entity>(count);
                for (var i = 0; i < count; i++) {
                    var entity = ReadEntity(reader);
                    list.Add(entity);
                }
                reader.EndList();
                return list;
            }
            else {
                var list = new List<Entity>();
                while (!reader.EndList()) {
                    var entity = ReadEntity(reader);
                    list.Add(entity);
                }
                return list;
            }
        }
        
        #endregion

        #region Read Write Archetype

        public Archetype ReadArchetype(SerializeReader reader) {
            var builder = CreateArchetype();
            reader.BeginScope<Archetype>(SerializeScope.Class);
            reader.BeginList<Type>(out var count);
            if (count >= 0) {
                for (var i = 0; i < count; i++) {
                    var type = reader.ReadValue<Type>();
                    builder.Add(type);
                }
            }
            else {
                while (!reader.EndList()) {
                    var type = reader.ReadValue<Type>();
                    builder.Add(type);
                }
            }
            reader.EndScope();
            return builder.Compile();
        }
        
        public void WriteArchetype(SerializeWriter writer, Archetype archetype) {
            writer.BeginScope<Archetype>(SerializeScope.Class);
            writer.BeginList<Type>(archetype.Definition.ComponentCount);
            foreach (var typeId in archetype.Definition.Types) {
                var componentGroup = GetComponentGroup(typeId);
                writer.WriteValue<Type>(componentGroup.Type);
            }
            writer.EndList();
            writer.EndScope();
        }
        
        #endregion

        #region Read Write Group

        public List<Entity> ReadEntitiesByArchetype(SerializeReader reader) {
            throw new NotImplementedException();
        }
        
        public void WriteEntitiesByArchetype(SerializeWriter writer, IEnumerable<Entity> entities) {
            throw new NotImplementedException();
        }

        #endregion
        
        #region Read Write Sandbox

        public void ReadSandbox(SerializeReader reader) {
            throw new NotImplementedException();
        }

        public void WriteSandbox(SerializeWriter writer) {
            throw new NotImplementedException();
        }
        
        #endregion


        #region _Temp

        
        //
        // public IList<Entity> ReadEntities2(ISerializeReader reader) {
        //     reader.ReadScope(typeof(object));
        //     List<Entity> list;
        //     var archetypeMode = reader.ReadField<bool>("ArchetypeMode", 1);
        //     if (archetypeMode) {
        //         list = new List<Entity>();
        //     }
        //     else {
        //         var tag = reader.ReadTag("Entities", 2);
        //         reader.ReadList<Entity>(out var count);
        //         list = new List<Entity>(count);
        //         for (var i = 0; i < count; i++) {
        //             var entity = ReadEntity(reader);
        //             list.Add(entity);
        //         }
        //         if(!reader.EndList()){ throw new SerializationException($"Serialize write entity failed.");};
        //     }
        //     reader.EndScope();
        //     return list;
        // }
        //
        // public void WriteEntities2(ISerializeWriter writer, IEnumerable<Entity> entities, bool archetypeMode = false) {
        //     writer.WriteScope(typeof(object));
        //     writer.WriteField("ArchetypeMode", 1, archetypeMode);
        //     if (archetypeMode) {
        //         writer.WriteTag("Archetypes", 2);
        //         foreach (var grouping in entities.GroupBy(e=>e.GetContext().Archetype)) {
        //             // WriteArchetype(writer, grouping.Key, grouping);
        //         }
        //     }
        //     else {
        //         writer.WriteTag("Entities", 2);
        //         var array = entities.ToArray();
        //         writer.WriteList<Entity>(array.Length);
        //         foreach (var entity in array) {
        //             WriteEntity(writer, entity.Id);
        //         }
        //         if(!writer.EndList()){ throw new SerializationException($"Serialize write entity failed.");};
        //     }
        //     writer.EndScope();
        // }
        //
        // private void WriteArchetype2(ISerializeWriter writer, ArchetypeDefinition archetype, IEnumerable<Entity> entities) {
        //     writer.WriteScope(typeof(ArchetypeDefinition));
        //     writer.WriteTag(nameof(archetype.Types), 1);
        //
        //     writer.WriteDict(typeof(Type), typeof(List<IComponent>), archetype.ComponentCount);
        //     foreach (var typeId in archetype.Types) {
        //         var componentGroup = GetComponentGroup(typeId);
        //         writer.WriteKey(componentGroup.Type);
        //         writer.WriteList(typeof(IComponent), archetype.EntityCount);
        //         var entityIds = archetype.GetEntities();
        //         for (var i = 0; i < entityIds.Count; i++) {
        //             var index = entityIds[i];
        //             if (index == 0) {
        //                 continue;
        //             }
        //             componentGroup.WriteComponent(writer, index);
        //         }
        //         if (!writer.EndList()) {
        //             throw new SerializationException($"Serialize write entity failed.");
        //         }
        //     }
        //     if (!writer.EndDict()) {
        //         throw new SerializationException($"Serialize write entity failed.");
        //     }
        //     writer.EndScope();
        // }
        //
        // private void ReadArchetype2(ISerializeReader reader) {
        //     reader.ReadScope(typeof(ArchetypeDefinition));
        //     var tag = reader.ReadTag(nameof(ArchetypeDefinition.Types), 1);
        //     reader.ReadDict(typeof(Type), typeof(List<IComponent>), out var count);
        //     for (var i = 0; i < count; i++) {
        //         var type = reader.ReadKey<Type>();
        //         var componentGroup = GetComponentGroup(type);
        //         reader.ReadList(typeof(IComponent), out var length);
        //         for (var j = 0; j < length; j++) {
        //             // componentGroup.ReadComponent(reader, );
        //         }
        //         if (!reader.EndList()) {
        //             throw new SerializationException($"Serialize write entity failed.");
        //         }
        //     }
        //     if (!reader.EndDict()) {
        //         throw new SerializationException($"Serialize write entity failed.");
        //     }
        //     reader.EndScope();
        // }
        //
        // private ArchetypeDefinition _ReadArchetype2(ISerializeReader reader) {
        //     var componentCount = reader.ReadValue<ushort>();
        //     var archetype = emptyArchetype;
        //     for (var i = 0; i < componentCount; i++) {
        //         var componentType = reader.ReadValue<Type>();
        //         archetype = archetype.AddComponent(this, componentType);
        //     }
        //     return archetype;
        // }
        //
        // public Archetype ReadArchetype3(ISerializeReader serializer) {
        //     var archetype = _ReadArchetype2(serializer);
        //     return new Archetype(this, archetype);
        // }
        //
        // private IList<Entity> ReadArchetypeWithEntities2(ISerializeReader serializer, IList<Entity> entities) {
        //     var archetype = _ReadArchetype2(serializer);
        //     var count = serializer.ReadValue<int>();
        //     if (entities == null) {
        //         entities = new List<Entity>(count);
        //     }
        //     for (var i = 0; i < count; i++) {
        //         entities[i] = CreateEntity(archetype);
        //     }
        //     foreach (var componentId in archetype.Components) {
        //         var componentGroup = GetComponentGroup(componentId);
        //         for (var i = 0; i < count; i++) {
        //             var entity = entities[i];
        //             ReadComponent(serializer, entity.Id, componentGroup.Type);
        //         }
        //     }
        //     return entities;
        // }
        //
        // private void _WriteArchetype2(ISerializeWriter writer, ArchetypeDefinition archetype) {
        //     writer.WriteScope(typeof(ArchetypeDefinition));
        //
        //     writer.WriteField(nameof(archetype.ComponentCount), 1, archetype.ComponentCount);
        //     writer.WriteTag(nameof(archetype.Components), 1);
        //     writer.WriteDict(typeof(Type), typeof(List<IComponent>), archetype.ComponentCount);
        //     foreach (int componentId in archetype.Components) {
        //         var componentGroup = GetComponentGroup(componentId);
        //         writer.WriteKey(componentGroup.Type);
        //         using (writer.WriteList(typeof(IComponent), archetype.EntityCount)) {
        //             var entities = archetype.GetEntities();
        //             for (var i = 0; i < entities.Count; i++) {
        //                 var index = entities[i];
        //                 if (index == 0) {
        //                     continue;
        //                 }
        //                 componentGroup.WriteComponent(writer, index);
        //             }
        //         }
        //     }
        //     writer.EndDict();
        //     
        //     writer.EndScope();
        //     
        //     
        //     
        //     
        //     using (writer.WriteScope(typeof(ArchetypeDefinition))) {
        //         writer.WriteTag(nameof(archetype.ComponentCount), 1);
        //         writer.WriteValue((ushort)archetype.ComponentCount); 
        //         
        //         writer.WriteTag(nameof(archetype.Components), 1);
        //         using (writer.WriteDict(typeof(Type), typeof(List<IComponent>), archetype.ComponentCount)) {
        //             foreach (int componentId in archetype.Components) {
        //                 var componentGroup = GetComponentGroup(componentId);
        //                 writer.WriteKey(componentGroup.Type);
        //                 using (writer.WriteList(typeof(IComponent), archetype.EntityCount)) {
        //                     var entities = archetype.GetEntities();
        //                     for (var i = 0; i < entities.Count; i++) {
        //                         var index = entities[i];
        //                         if (index == 0) {
        //                             continue;
        //                         }
        //                         componentGroup.WriteComponent(writer, index);
        //                     }
        //                 }
        //             }
        //         }
        //     }
        // }
        //
        //
        //
        //
        //
        //
        //
        // public void WriteArchetype2(ISerializeWriter serializer, Archetype archetype) {
        //     _WriteArchetype2(serializer, archetype.Definition);
        // }
        //
        // private void WriteArchetypeWithEntities2(ISerializeWriter serializer, ArchetypeDefinition archetype) {
        //     _WriteArchetype2(serializer, archetype);
        // }
        //
        // public void ReadSandbox2(ISerializeReader serializer) {
        //     var archetypeCount = serializer.ReadValue<int>();
        //     for (var i = 0; i < archetypeCount; i++) {
        //         ReadArchetypeWithEntities2(serializer, null);
        //     }
        // }
        //
        // public void WriteSandbox2(ISerializeWriter serializer) {
        //     var archetypeCount = archetypes.Sum(pair => pair.Value.Count);
        //     serializer.WriteValue<int>(archetypeCount);
        //     foreach (var pair in archetypes) {
        //         for (int i = 0; i < pair.Value.Count; i++) {
        //             var archetype = pair.Value[i];
        //             WriteArchetypeWithEntities2(serializer, archetype);
        //         }
        //     }
        // }

        #endregion
        
    }
}