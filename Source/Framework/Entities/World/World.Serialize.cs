using System;
using Coorth.Serialize;

namespace Coorth.Framework;

public partial class World {
    public void ReadComponent(ISerializeReader reader, in EntityId id, Type type) {
        ref var context = ref GetContext(id.Index);
        var group = GetComponentGroup(type);
        if (context.TryGet(group.TypeId, out var index)) {
            group.Read(reader, index);
            group.OnModify(id, index);
        }
        else {
            index = group.Add(context.GetEntity(this));
            group.Read(reader, index);
            context.Archetype.OnAddComponent(ref context, group.TypeId, index);
            group.OnAdd(id, index);
        }
    }

    public void WriteComponent(ISerializeWriter writer, in EntityId entityId, Type type) {
        ref var context = ref GetContext(entityId.Index);
        var componentGroup = GetComponentGroup(type);
        componentGroup.Write(writer, context[componentGroup.TypeId]);
    }

    public void ReadEntity(ISerializeReader reader, EntityId entityId) {
        reader.BeginData(typeof(Entity));
        var count = reader.ReadField<int>(nameof(Entity.Count), 1);
        reader.ReadTag(nameof(EntityContext.Components), 2);
        reader.BeginDict(typeof(Type), typeof(IComponent), out var _);
        for (var i = 0; i < count; i++) {
            var type = reader.ReadKey<Type>();
            ReadComponent(reader, entityId, type);
        }

        reader.EndDict();
        reader.EndData();
    }

    public Entity ReadEntity(ISerializeReader reader) {
        var entity = CreateEntity();
        ReadEntity(reader, entity.Id);
        return entity;
    }

    public void WriteEntity(ISerializeWriter writer, in EntityId entityId) {
        writer.BeginData<Entity>(2);
        ref var context = ref GetContext(entityId.Index);
        writer.WriteField(nameof(Entity.Count), 1, context.Count);
        writer.WriteTag(nameof(EntityContext.Components), 2);
        writer.BeginDict(typeof(Type), typeof(IComponent), context.Count);
        foreach (var pair in context.Components) {
            var componentGroup = GetComponentGroup(pair.Key);
            writer.WriteKey(componentGroup.Type);
            componentGroup.Write(writer, pair.Value);
        }

        writer.EndDict();
        writer.EndData();
    }
}