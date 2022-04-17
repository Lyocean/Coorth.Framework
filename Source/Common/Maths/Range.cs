using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth.Maths {
    [DataContract, Guid("15AA0458-A548-473F-8934-9F2EDF30AE87")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Range : IEquatable<Range> {

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
            Min = Math.Min(this.Min, other.Min);
            Max = Math.Max(this.Max, other.Max);
        }

        public readonly ContainmentType Contains(in Range other) {
            if (this.Max < other.Min || this.Min > other.Max) {
                return ContainmentType.Disjoint;
            }
            if (this.Min > other.Min && this.Max < other.Max) {
                return ContainmentType.Contains;
            }
            return ContainmentType.Intersects;
        }

        public readonly bool Equals(Range other) {
            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }

        public override readonly bool Equals(object? obj) {
            return obj is Range other && Equals(other);
        }

        public static bool operator ==(Range left, Range right) {
            return left.Equals(right);
        }

        public static bool operator !=(Range left, Range right) {
            return !(left == right);
        }
        
        public override readonly int GetHashCode() {
            return HashCode.Combine(Min, Max);
        }

        public override readonly string ToString() {
            return $"Range(Min:{Min},Max:{Max})";
        }
    }
}
