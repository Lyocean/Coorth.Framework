using System;

namespace Coorth {
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
    public class DisplayAttribute : Attribute {
        
        public string Label = null;
        
        public bool Editable = true;
        
        public bool Visible = true;
        
    }
}