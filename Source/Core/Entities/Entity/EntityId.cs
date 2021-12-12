using System;

namespace Coorth {
    public readonly struct EntityId : IEquatable<EntityId> {
        
        internal readonly int Index;
        
        internal readonly int Version;

        public static EntityId Null => new EntityId(0, 0);

        public EntityId(int index, int version) {
            Index = index;
            Version = version;
        }

        public EntityId(long uid) {
            Index = (int) (uid & ~0xFFFFFFFF);
            Version = (int) ((uid >> sizeof(int)) & ~0xFFFFFFFF);
        }

        public bool IsNull => Index == 0 && Version == 0;

        public bool IsNotNull => Index != 0 || Version != 0;

        public static explicit operator long(EntityId id) {
            return (((long) id.Version) << sizeof(int)) | ((long) id.Index);
        }

        public static explicit operator EntityId(long uid) {
            return new EntityId(uid);
        }

        public static bool operator ==(EntityId a, EntityId b) {
            return a.Index == b.Index && a.Version == b.Version;
        }

        public static bool operator !=(EntityId a, EntityId b) {
            return a.Index != b.Index || a.Version != b.Version;
        }

        public bool Equals(EntityId other) {
            return Index == other.Index && Version == other.Version;
        }

        public override bool Equals(object obj) {
            return obj != null && Equals((EntityId) obj);
        }

        public override int GetHashCode() {
            return Index & (Version << 8);
        }

        public override string ToString() {
            return $"EntityId({Index}-{Version})";
        }
    }
}