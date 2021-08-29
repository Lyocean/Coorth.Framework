using System;
using System.Runtime.Serialization;

namespace Coorth {
    [DataContract]
    public readonly struct ActorId : IEquatable<ActorId> {
        [DataMember(Order = 0)]
        private readonly Guid id;

        public bool IsNull => id == Guid.Empty;
        
        private ActorId(Guid key) { id = key; }

        public static ActorId New() { return new ActorId(Guid.NewGuid()); }
        
        public bool Equals(ActorId other) { return this.id == other.id; }

        public static bool operator ==(ActorId a, ActorId b) { return a.Equals(b); }

        public static bool operator !=(ActorId a, ActorId b) { return !a.Equals(b); }

        public override bool Equals(object obj) { return obj is ActorId actorId && actorId.Equals(this); }

        public override int GetHashCode() { return id.GetHashCode(); }

        public override string ToString() { return $"[ActorId]: {id.ToString()}"; }

        public string ToShortString() { return id.ToString(); }
    }
}