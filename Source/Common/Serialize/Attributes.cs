using System;

namespace Coorth {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum)]
    public class StoreContractAttribute : Attribute {
        public Type Base;

        public int Index;

        public readonly Guid Guid;
        
        public Type Serializer;
        
        public StoreContractAttribute() {
        }
        
        public StoreContractAttribute(string guid) {
            this.Guid = !string.IsNullOrEmpty(guid) ? Guid.Parse(guid) : Guid.Empty;
        }
    }
    
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

    public class StoreIgnoreAttribute : Attribute {
    }
    
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
    public class DisplayAttribute : Attribute {
        
        public string Label = null;
        
        public bool Editable = true;
        
        public bool Visible = true;
        
    }
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class TitleAttribute : Attribute {
        
        public readonly string Label;
        
        public readonly bool Foldable;

        public TitleAttribute(string label, bool foldable = false) {
            this.Label = label;
            this.Foldable = foldable;
        }
    }
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DataRangeAttribute : Attribute {
        
        public readonly double Min;
        
        public readonly double Max;

        public DataRangeAttribute(double min, double max) {
            this.Min = min;
            this.Max = max;
        }
    }
}