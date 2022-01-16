using System;

namespace Coorth {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class StoreMemberAttribute : Attribute, IComparable<StoreMemberAttribute> {

        public bool IsRequired;

        public string Name;
        
        public readonly int Index;

        public readonly bool Polymorphic;

        public StoreMemberAttribute(int index, bool polymorphic = false) {
            this.Index = index;
            this.Polymorphic = polymorphic;
        }

        public int CompareTo(StoreMemberAttribute other) {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            int indexComparison = Index.CompareTo(other.Index);
            return indexComparison;
        }
    }
}