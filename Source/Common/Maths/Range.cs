using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths;

[DataDefine(DataFlags.PublicField), Guid("15AA0458-A548-473F-8934-9F2EDF30AE87")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct Range {

    public double Min;
        
    public double Max;

    public Range(double min, double max) {
        if (min > max) {
            throw new ArgumentException("Range: min music less or equal than max.");
        }
        Min = min;
        Max = max;
    }

    public Range(double val) {
        Min = val;
        Max = val;
    }

    public void Expand(Range other) {
        Min = Math.Min(Min, other.Min);
        Max = Math.Max(Max, other.Max);
    }

    public readonly ContainmentType Contains(in Range other) {
        if (Max < other.Min || Min > other.Max) {
            return ContainmentType.Disjoint;
        }
        if (Min > other.Min && Max < other.Max) {
            return ContainmentType.Contains;
        }
        return ContainmentType.Intersects;
    }

}