using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Range<T> where T : IComparable<T> {

        public T Min;
        
        public T Max;

        public Range(T min, T max) {
            if (min.CompareTo(max) > 0) {
                throw new ArgumentException("Range: min music less or equal than max.");
            }
            Min = min;
            Max = max;
        }

        public Range(T val) {
            Min = val;
            Max = val;
        }
    }
    
    
    public enum DistributionTypes {
        Constant,
        Uniform,
        Normal,
    }
    
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct RandomRange {
        
        public DistributionTypes Distribution;
        
        public float Min;
        
        public float Max;

        public RandomRange(float min, float max) {
            this.Min = min;
            this.Max = max;
            this.Distribution = DistributionTypes.Uniform;
        }
        
        public RandomRange(float min, float max, DistributionTypes distribution) {
            this.Min = min;
            this.Max = max;
            this.Distribution = distribution;
        }

        private static double NormalDistribute(double mu, double sigma = 1f, double size = 1f) {
            double u = MathUtil.Random(0.0, 1.0);
            double v = MathUtil.Random(0.0, 1.0);
            double z = Math.Sqrt(-2 * Math.Log(u)) * Math.Cos(2 * Math.PI * v);
            // double b = Math.Sqrt(-2 * Math.Log(u)) * Math.Sin(2 * Math.PI * v);
            return mu + z * sigma;
        }
        
        // public float Evaluate() {
        //     float result = Distribution switch {
        //         DistributionTypes.Constant => (Min + Max)/2f,
        //         DistributionTypes.Uniform => MathUtil.Random(Min, Max),
        //         DistributionTypes.Normal => (float)NormalDistribute((Min + Max)/2f, (Max - Min) / 2f / 3f),
        //         _ => (Min + Max)/2f
        //     };
        //     return MathUtil.Clamp(result, Min, Max);
        // }
    }
}
