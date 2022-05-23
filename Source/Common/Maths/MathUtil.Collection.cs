using System.Collections.Generic;

namespace Coorth.Maths; 

public static partial class MathUtil {

    public static void Swap<T>(this IList<T> list, int i, int j) {
        (list[i], list[j]) = (list[j], list[i]);
    }
        
    public static void Shuffle<T>(this IList<T> list) {
        for (var i = 0; i < list.Count; i++) {
            var index = RandomUtil.Random(i, list.Count);
            Swap(list, i, index);
        }
    }
}