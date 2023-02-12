using System;
using Coorth.Serialize;


namespace Coorth.Collections;

[Serializable]
public struct BitMask32 : IEquatable<BitMask32> {
    public uint Value { get; private set; }

    public bool IsNull => Value != 0;

    public static BitMask32 Null => new BitMask32(0);

    public static BitMask32 Full => new BitMask32(~0u);

    public const int CAPACITY = 32;

    public BitMask32(uint v) => Value = v;

    public bool this[int index] {
        get => (Value & (1u << index)) > 0;
        set => Value = value ? Value | (1u << index) : Value & ~(1u << index);
    }

    public static BitMask32 operator &(BitMask32 l, BitMask32 r) => new BitMask32(l.Value & r.Value);

    public static BitMask32 operator |(BitMask32 l, BitMask32 r) => new BitMask32(l.Value | r.Value);

    public static BitMask32 operator ~(BitMask32 l) => new BitMask32(~l.Value);

    public static explicit operator int(BitMask32 l) => (int) l.Value;

    public static explicit operator uint(BitMask32 l) => l.Value;

    public static explicit operator BitMask32(int l) => new BitMask32((uint) l);

    public static explicit operator BitMask32(uint l) => new BitMask32(l);

    public bool Equals(BitMask32 other) => Value == other.Value;

    public override bool Equals(object? obj) => obj is BitMask32 other && Equals(other);

    public static bool operator ==(BitMask32 left, BitMask32 right) => left.Equals(right);

    public static bool operator !=(BitMask32 left, BitMask32 right) => !(left == right);

    public override int GetHashCode() => (int) Value;

    public override string ToString() {
        const int segment = 8;
        var span = (stackalloc char[CAPACITY + CAPACITY / segment]);
        var index = span.Length - 1;
        for (var i = 0; i < CAPACITY / segment; i++) {
            for (var j = 0; j < segment; j++) {
                span[index--] = this[i * segment + j] ? '1' : '0';
            }
            span[index--] = '_';
        }
        return new string(span[1..]);
    }
    
    
    [SerializeFormatter(typeof(BitMask32))]
    public class BitMask32_Formatter : SerializeFormatter<BitMask32> {

        public override void SerializeWriting(in SerializeWriter writer, scoped in BitMask32 value) {
            writer.WriteUInt32(value.Value);
        }

        public override void SerializeReading(in SerializeReader reader, scoped ref BitMask32 value) {
            value = new BitMask32(reader.ReadUInt32());
        }

    }
    
}