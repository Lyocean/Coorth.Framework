// using System;
// using NUnit.Framework;
//
// namespace Coorth.Tests {
//     [TestFixture]
//     public class JsonSerializeTest {
//
//         public void TestPrimitive() {
//             var a = new TestSerializePrimitive() {
//                 Bool = true,
//                 Byte = 2,
//                 Sbyte = 3,
//                 Short = 12,
//                 UShort = 54,
//                 Int = 87,
//                 UInt = 65847,
//                 Float = -12.05f,
//             };
//         }
//     }
//
//     [Serializable]
//     public class TestSerializePrimitive {
//         public bool Bool;
//         public byte Byte;
//         public sbyte Sbyte;
//         public short Short;
//         public ushort UShort;
//         public int Int;
//         public uint UInt;
//         public float Float;
//         public double Double;
//         public long Long;
//         public ulong ULong;
//
//         public override bool Equals(object obj) {
//             return base.Equals(obj);
//         }
//
//         protected bool Equals(TestSerializePrimitive other) {
//             return Bool == other.Bool && Byte == other.Byte && Sbyte == other.Sbyte && Short == other.Short && UShort == other.UShort && Int == other.Int && UInt == other.UInt && Float.Equals(other.Float) && Double.Equals(other.Double) && Long == other.Long && ULong == other.ULong;
//         }
//
//         public override int GetHashCode() {
//             unchecked {
//                 var hashCode = Bool.GetHashCode();
//                 hashCode = (hashCode * 397) ^ Byte.GetHashCode();
//                 hashCode = (hashCode * 397) ^ Sbyte.GetHashCode();
//                 hashCode = (hashCode * 397) ^ Short.GetHashCode();
//                 hashCode = (hashCode * 397) ^ UShort.GetHashCode();
//                 hashCode = (hashCode * 397) ^ Int;
//                 hashCode = (hashCode * 397) ^ (int) UInt;
//                 hashCode = (hashCode * 397) ^ Float.GetHashCode();
//                 hashCode = (hashCode * 397) ^ Double.GetHashCode();
//                 hashCode = (hashCode * 397) ^ Long.GetHashCode();
//                 hashCode = (hashCode * 397) ^ ULong.GetHashCode();
//                 return hashCode;
//             }
//         }
//     }
// }