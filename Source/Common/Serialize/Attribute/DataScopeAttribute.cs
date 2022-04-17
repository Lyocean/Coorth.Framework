using System;

namespace Coorth {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class DataScopeAttribute : Attribute {

        public string? Label { get; set; }

        public bool CanFold { get; set; } = true;

        public DataScopeAttribute() {
            
        }
        
        public DataScopeAttribute(string label, bool canFold = true) {
            this.Label = label;
            this.CanFold = canFold;
        }
    }
}