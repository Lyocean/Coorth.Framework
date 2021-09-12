using System;
using System.Runtime.Serialization;

namespace Coorth {
    [Serializable, DataContract]
    public readonly struct ActorId : IEquatable<ActorId> {
        
        [DataMember(Order = 0)]
        public readonly Guid Id;

        public bool IsNull => Id == Guid.Empty;
        
        public ActorId(Guid key) { Id = key; }

        public static ActorId New() { return new ActorId(Guid.NewGuid()); }
        
        public bool Equals(ActorId other) { return this.Id == other.Id; }

        public static bool operator ==(ActorId a, ActorId b) { return a.Equals(b); }

        public static bool operator !=(ActorId a, ActorId b) { return !a.Equals(b); }

        public override bool Equals(object obj) { return obj is ActorId actorId && actorId.Equals(this); }

        public override int GetHashCode() { return Id.GetHashCode(); }

        public override string ToString() { return $"[ActorId]: {Id.ToString()}"; }

        public string ToShortString() { return Id.ToString(); }
    }
}