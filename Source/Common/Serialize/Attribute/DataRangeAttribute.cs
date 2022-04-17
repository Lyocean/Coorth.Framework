using System;

namespace Coorth {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DataRangeAttribute : Attribute {

        public double Min { get; set; } = double.MinValue;
        
        public double Max { get; set; } = double.MaxValue;

        public DataRangeAttribute() { }
        
        public DataRangeAttribute(double min, double max) {
            this.Min = min;
            this.Max = max;
        }
    }
}