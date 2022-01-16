using System;

namespace Coorth {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class TitleAttribute : Attribute {
        
        public readonly string Label;
        
        public readonly bool Foldable;

        public TitleAttribute(string label, bool foldable = false) {
            this.Label = label;
            this.Foldable = foldable;
        }
    }
}