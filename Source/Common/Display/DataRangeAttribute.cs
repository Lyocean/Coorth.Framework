using System;

namespace Coorth {
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