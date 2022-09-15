using System;
using System.Threading;
using Coorth.Serialize;

namespace Coorth.Framework;

#if COORTH_ACTOR_ID_GUID

[Serializable, StoreContract]
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

[Serializable, StoreContract]
public readonly record struct ActorId(long Id) {
    
    [StoreMember(1)]
    public readonly long Id = Id;
    
    public bool IsNull => Id == 0;

    public static ActorId New() => new(idGenerator.Next());

    public static ActorId Null => new(0);

    private static IdGenerator idGenerator = new(0, DefaultSecondProvider); 

    private static long DefaultSecondProvider() {
        var now = DateTime.UtcNow;
        var start = new DateTime(now.Year / 10, 1, 1);
        return (long)(now - start).TotalSeconds;
    }

    public static void Setup(ushort poolId, Func<long> secondGetter) => idGenerator = new IdGenerator(poolId, secondGetter);
    
    [Serializer(typeof(ActorId))]
    private class Serializer : Serializer<ActorId> {
        
        public override void Write(SerializeWriter writer, in ActorId value) {
            writer.WriteValue(value.Id);
        }

        public override ActorId Read(SerializeReader reader, ActorId value) {
            return new ActorId(reader.ReadValue<long>());
        }
    }
    
    private class IdGenerator {
    
        private readonly int head;

        private readonly ushort id;

        private long second;

        private volatile int current;

        private readonly Func<long> provider;
    
        public IdGenerator(ushort id, Func<long> provider) {
            this.head = sizeof(short) * 8;
            this.id = id;
            this.provider = provider;
        }
    
        public IdGenerator(ushort id, int startYear) {
            this.head = sizeof(short) * 8;
            this.id = id;
            this.provider = () => DefaultSecondProvider(startYear);
        }
    
        public IdGenerator(byte id, int startYear) {
            this.head = sizeof(byte) * 8;
            this.id = id;
            this.provider = () => DefaultSecondProvider(startYear);
        }

        public long Next() {
            if (current >= (1 << head - 1)) {
                var currSecond = provider();
                if (currSecond <= Interlocked.Read(ref second)) {
                    SpinWait.SpinUntil(() => provider() > Interlocked.Read(ref second));
                }
                Interlocked.Increment(ref second);
                current = 0;
            }
            var increment = Interlocked.Increment(ref current);
            return ((long) id << (64 - head)) | (second << (32 - head)) | (uint) increment;
        }
    
        private static long DefaultSecondProvider(int startYear) {
            var now = DateTime.UtcNow;
            var start = new DateTime(now.Year - startYear, 1, 1);
            return (long)(now - start).TotalSeconds;
        }
    }

}

#endif
