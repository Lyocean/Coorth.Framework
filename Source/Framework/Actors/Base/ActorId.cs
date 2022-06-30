using System;
using System.Runtime.Serialization;
using Coorth.Serialize;

namespace Coorth.Framework;

#if COORTH_ACTOR_ID_GUID

[Serializable, DataContract]
public readonly record struct ActorId(Guid Id) {
    
    [DataMember(Order = 1)]
    public readonly Guid Id = Id;

    public bool IsNull => Id == Guid.Empty;

    public static ActorId Null => new(Guid.Empty);

    public static ActorId New() { return new ActorId(Guid.NewGuid()); }
        
    [Serializer(typeof(ActorId))]
    private class Serializer : Serializer<ActorId> {
        
        public override void Write(SerializeWriter writer, in ActorId value) {
            writer.WriteValue(value.Id);
        }

        public override ActorId Read(SerializeReader reader, ActorId value) {
            var guid = reader.ReadValue<Guid>();
            return new ActorId(guid);
        }
    }
}
#else

[Serializable, DataContract]
public readonly record struct ActorId(long Id) {
    
    [DataMember(Order = 1)]
    public readonly long Id = Id;
    
    public bool IsNull => Id == 0;

    public static ActorId New() => new(idPool.Next());

    public static ActorId Null => new(0);

    private static IdPool idPool = new(0, DefaultSecondProvider); 

    private static long DefaultSecondProvider() {
        var now = DateTime.UtcNow;
        var start = new DateTime(now.Year / 10, 1, 1);
        return (long)(now - start).TotalSeconds;
    }

    public static void Setup(ushort poolId, Func<long> secondGetter) => idPool = new IdPool(poolId, secondGetter);
    
    [Serializer(typeof(ActorId))]
    private class Serializer : Serializer<ActorId> {
        
        public override void Write(SerializeWriter writer, in ActorId value) {
            writer.WriteValue(value.Id);
        }

        public override ActorId Read(SerializeReader reader, ActorId value) {
            return new ActorId(reader.ReadValue<long>());
        }
    }
}

#endif
