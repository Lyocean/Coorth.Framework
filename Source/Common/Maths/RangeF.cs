using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[DataDefine(DataFlags.PubField), Guid("3C5C5E01-2BB8-4B77-A44F-9797AC954EE7")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct RangeF {
    public float Min;
        
    public float Max;
    
    
    public RangeF(float min, float max) {
        if (min > max) {
            throw new ArgumentException("Range: min music less or equal than max.");
        }
        Min = min;
        Max = max;
    }

    public RangeF(float val) {
        Min = val;
        Max = val;
    }

    public void Expand(RangeF other) {
        Min = Math.Min(Min, other.Min);
        Max = Math.Max(Max, other.Max);
    }

    public readonly ContainmentType Contains(in RangeF other) {
        if (Max < other.Min || Min > other.Max) {
            return ContainmentType.Disjoint;
        }
        if (Min > other.Min && Max < other.Max) {
            return ContainmentType.Contains;
        }
        return ContainmentType.Intersects;
    }
}