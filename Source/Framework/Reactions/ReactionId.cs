using System;
using Coorth.Serialize;

namespace Coorth.Framework; 

[Serializer(typeof(Serializer))]
public readonly record struct ReactionId {
        
    private readonly Guid id;
        
    public static ReactionId New() => new(Guid.NewGuid());

    private ReactionId(Guid id) => this.id = id;

    private class Serializer : Serializer<ReactionId> {
        public override void Write(ISerializeWriter writer, in ReactionId value) {
            writer.WriteValue(value.id);
        }

        public override ReactionId Read(ISerializeReader reader, ReactionId value) {
            return new ReactionId(reader.ReadValue<Guid>());
        }
    }
}