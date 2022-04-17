using System.Numerics;
using Coorth.Maths;

namespace Coorth.Serializes {
    [DataSerializer(typeof(Vector2))]
    public class Vector2Serializer : TupleSerializer<Vector2> {
        protected override void OnWrite(SerializeWriter writer, in Vector2 value) {
            writer.WriteField(nameof(value.X), 1, value.X);
            writer.WriteField(nameof(value.Y), 2, value.Y);
        }

        protected override void OnRead(SerializeReader reader, ref Vector2 value) {
            value.X = reader.ReadField<float>(nameof(value.X), 1);
            value.Y = reader.ReadField<float>(nameof(value.Y), 2);
        }
    }
    
    [DataSerializer(typeof(Vector3))]
    public class Vector3Serializer : TupleSerializer<Vector3> {
        protected override void OnWrite(SerializeWriter writer, in Vector3 value) {
            writer.WriteField(nameof(value.X), 1, value.X);
            writer.WriteField(nameof(value.Y), 2, value.Y);
            writer.WriteField(nameof(value.Z), 3, value.Z);
        }

        protected override void OnRead(SerializeReader reader, ref Vector3 value) {
            value.X = reader.ReadField<float>(nameof(value.X), 1);
            value.Y = reader.ReadField<float>(nameof(value.Y), 2);
            value.Z = reader.ReadField<float>(nameof(value.Z), 3);
        }
    }
    
    [DataSerializer(typeof(Vector4))]
    public class Vector4Serializer : TupleSerializer<Vector4> {
        protected override void OnWrite(SerializeWriter writer, in Vector4 value) {
            writer.WriteField(nameof(value.X), 1, value.X);
            writer.WriteField(nameof(value.Y), 2, value.Y);
            writer.WriteField(nameof(value.Z), 3, value.Z);
            writer.WriteField(nameof(value.W), 4, value.W);
        }

        protected override void OnRead(SerializeReader reader, ref Vector4 value) {
            value.X = reader.ReadField<float>(nameof(value.X), 1);
            value.Y = reader.ReadField<float>(nameof(value.Y), 2);
            value.Z = reader.ReadField<float>(nameof(value.Z), 3);
            value.W = reader.ReadField<float>(nameof(value.W), 4);
        }
    }
    
    [DataSerializer(typeof(Quaternion))]
    public class QuaternionSerializer : TupleSerializer<Quaternion> {
        protected override void OnWrite(SerializeWriter writer, in Quaternion value) {
            writer.WriteField(nameof(value.X), 1, value.X);
            writer.WriteField(nameof(value.Y), 2, value.Y);
            writer.WriteField(nameof(value.Z), 3, value.Z);
            writer.WriteField(nameof(value.W), 4, value.W);
        }

        protected override void OnRead(SerializeReader reader, ref Quaternion value) {
            value.X = reader.ReadField<float>(nameof(value.X), 1);
            value.Y = reader.ReadField<float>(nameof(value.Y), 2);
            value.Z = reader.ReadField<float>(nameof(value.Z), 3);
            value.W = reader.ReadField<float>(nameof(value.W), 4);
        }
    }
    
    [DataSerializer(typeof(Int2))]
    public class Int2Serializer : TupleSerializer<Int2> {
        protected override void OnWrite(SerializeWriter writer, in Int2 value) {
            writer.WriteField(nameof(value.X), 1, value.X);
            writer.WriteField(nameof(value.Y), 2, value.Y);
        }

        protected override void OnRead(SerializeReader reader, ref Int2 value) {
            value.X = reader.ReadField<int>(nameof(value.X), 1);
            value.Y = reader.ReadField<int>(nameof(value.Y), 2);
        }
    }
    
    [DataSerializer(typeof(Int3))]
    public class Int3Serializer : TupleSerializer<Int3> {
        protected override void OnWrite(SerializeWriter writer, in Int3 value) {
            writer.WriteField(nameof(value.X), 1, value.X);
            writer.WriteField(nameof(value.Y), 2, value.Y);
            writer.WriteField(nameof(value.Z), 3, value.Z);
        }

        protected override void OnRead(SerializeReader reader, ref Int3 value) {
            value.X = reader.ReadField<int>(nameof(value.X), 1);
            value.Y = reader.ReadField<int>(nameof(value.Y), 2);
            value.Z = reader.ReadField<int>(nameof(value.Z), 3);
        }
    }
    
    [DataSerializer(typeof(Color))]
    public class ColorSerializer : TupleSerializer<Color> {
        protected override void OnWrite(SerializeWriter writer, in Color value) {
            writer.WriteField(nameof(value.R), 1, value.R);
            writer.WriteField(nameof(value.G), 2, value.G);
            writer.WriteField(nameof(value.B), 3, value.B);
            writer.WriteField(nameof(value.A), 4, value.A);
        }

        protected override void OnRead(SerializeReader reader, ref Color value) {
            value.R = reader.ReadField<float>(nameof(value.R), 1);
            value.G = reader.ReadField<float>(nameof(value.G), 2);
            value.B = reader.ReadField<float>(nameof(value.B), 3);
            value.A = reader.ReadField<float>(nameof(value.A), 4);
        }
    }


    [DataSerializer(typeof(BitMask32))]
    public class BitMask32Serializer : Serializer<BitMask32> {
        public override void Write(SerializeWriter writer, in BitMask32 value) {
            writer.WriteUInt(value.Value);
        }

        public override BitMask32 Read(SerializeReader reader, BitMask32 value) {
            return new BitMask32(reader.ReadUInt());
        }
    }
    
    [DataSerializer(typeof(BitMask64))]
    public class BitMask64Serializer : Serializer<BitMask64> {
        public override void Write(SerializeWriter writer, in BitMask64 value) {
            writer.WriteULong(value.Value);
        }

        public override BitMask64 Read(SerializeReader reader, BitMask64 value) {
            return new BitMask64(reader.ReadULong());
        }
    }
}