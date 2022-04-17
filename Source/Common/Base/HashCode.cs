// using System.Runtime.CompilerServices;
//
// namespace System {
// #if NETSTANDARD2_1_OR_GREATER
//     public struct HashCode {
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static int Combine<T>(T v) => v is null ? 0 : v.GetHashCode();
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static int Combine<T1, T2>(T1 v1, T2 v2) {
//             unchecked {
//                 return (v1 is null ? 0 : v1.GetHashCode() * 397) ^ (v2 is null ? 0 : v2.GetHashCode());
//             }
//         }
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static int Combine<T1, T2, T3>(T1 v1, T2 v2, T3 v3) {
//             unchecked {
//                 var hashCode = v1 is null ? 0 : v1.GetHashCode();
//                 hashCode = (hashCode * 397) ^ (v2 is null ? 0 : v2.GetHashCode());
//                 hashCode = (hashCode * 397) ^ (v3 is null ? 0 : v3.GetHashCode());
//                 return hashCode;
//             }
//         }
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static int Combine<T1, T2, T3, T4>(T1 v1, T2 v2, T3 v3, T4 v4) {
//             unchecked {
//                 var hashCode = v1 is null ? 0 : v1.GetHashCode();
//                 hashCode = (hashCode * 397) ^ (v2 is null ? 0 : v2.GetHashCode());
//                 hashCode = (hashCode * 397) ^ (v3 is null ? 0 : v3.GetHashCode());
//                 hashCode = (hashCode * 397) ^ (v4 is null ? 0 : v4.GetHashCode());
//                 return hashCode;
//             }
//         }
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static int Combine<T1, T2, T3, T4, T5>(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5) {
//             unchecked {
//                 var hashCode = v1 is null ? 0 : v1.GetHashCode();
//                 hashCode = (hashCode * 397) ^ (v2 is null ? 0 : v2.GetHashCode());
//                 hashCode = (hashCode * 397) ^ (v3 is null ? 0 : v3.GetHashCode());
//                 hashCode = (hashCode * 397) ^ (v4 is null ? 0 : v4.GetHashCode());
//                 hashCode = (hashCode * 397) ^ (v5 is null ? 0 : v5.GetHashCode());
//                 return hashCode;
//             }
//         }
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static int Combine<T1, T2, T3, T4, T5, T6>(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6) {
//             unchecked {
//                 var hashCode = v1 is null ? 0 : v1.GetHashCode();
//                 hashCode = (hashCode * 397) ^ (v2 is null ? 0 : v2.GetHashCode());
//                 hashCode = (hashCode * 397) ^ (v3 is null ? 0 : v3.GetHashCode());
//                 hashCode = (hashCode * 397) ^ (v4 is null ? 0 : v4.GetHashCode());
//                 hashCode = (hashCode * 397) ^ (v5 is null ? 0 : v5.GetHashCode());
//                 hashCode = (hashCode * 397) ^ (v6 is null ? 0 : v6.GetHashCode());
//                 return hashCode;
//             }
//         }
//     }
// #endif
//
// }