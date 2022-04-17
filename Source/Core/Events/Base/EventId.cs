using System;

namespace Coorth {
    public readonly struct EventId : IEquatable<EventId> {
        
        private readonly Guid id;
        
        public static EventId New() {
            return new EventId(Guid.NewGuid());
        }

        private EventId(Guid id) {
            this.id = id;
        }
        
        public static bool operator ==(EventId a, EventId b) {
            return a.Equals(b);
        }

        public static bool operator !=(EventId a, EventId b) {
            return !a.Equals(b);
        }

        public bool Equals(EventId other) {
            return this.id == other.id;
        }

        public override bool Equals(object? obj) {
            return obj is EventId other && Equals(other);
        }

        public override int GetHashCode() {
            return id.GetHashCode();
        }
        
        public override string ToString() {
            return $"[EventId]: {id.ToString()}";
        }
        
        public string ToShortString() {
            return id.ToString();
        }
    }
}