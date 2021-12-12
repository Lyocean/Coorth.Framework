using System;
using System.Collections.Generic;

namespace Coorth.Maths {
    public static class RandomUtil {

        private static Random random = new Random(Guid.NewGuid().GetHashCode());

        public static void SetSeed(int seed) {
            random = new Random(seed);
        }

        public static float Random(float min, float max) {
            return (float) (min + (max - min) * random.NextDouble());
        }

        public static double Random(double min, double max) {
            return min + (max - min) * random.NextDouble();
        }

        public static int Random(int min, int max) {
            return random.Next(min, max);
        }
        
        public static TimeSpan Random(TimeSpan min, TimeSpan max) {
            return TimeSpan.FromSeconds(min.TotalSeconds + (max.TotalSeconds - min.TotalSeconds) * random.NextDouble());
        }
        
        public static T Random<T>(IList<T> list) {
            var index = random.Next(0, list.Count);
            return list[index];
        }

        public static int RandomRange(int min, int max) {
            return (int) (min + (max - min) * random.NextDouble());
        }

        public static double RandomNormal(double mu = 0d, double sigma = 1d, double size = 1d) {
            var u = Random(0d, size);
            var v = Random(0d, size);
            var z = Math.Sqrt(-2 * Math.Log(u)) * Math.Cos(2 * Math.PI * v);
            return mu + z * sigma;
        }
    }
}