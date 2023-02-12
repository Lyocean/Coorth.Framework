using System;
using Coorth.Serialize;

namespace Coorth.Collections;

[Serializable]
public struct BitMask64 : IEquatable<BitMask64> {
    public ulong Value { get; private set; }

    public bool IsNull => Value != 0;

    public static BitMask64 Null => new BitMask64(0);

    public static BitMask64 Full => new BitMask64(~0ul);

    public const int CAPACITY = 64;

    public BitMask64(ulong v) => Value = v;

    public bool this[int index] {
        get => (Value & (1ul << index)) > 0;
        set => Value = value ? Value | (1ul << index) : Value & ~(1ul << index);
    }

    public static BitMask64 operator &(BitMask64 l, BitMask64 r) => new BitMask64(l.Value & r.Value);

    public static BitMask64 operator |(BitMask64 l, BitMask64 r) => new BitMask64(l.Value | r.Value);

    public static BitMask64 operator ~(BitMask64 l) => new BitMask64(~l.Value);

    public static explicit operator long(BitMask64 l) => (int) l.Value;

    public static explicit operator ulong(BitMask64 l) => l.Value;

    public static explicit operator BitMask64(long l) => new BitMask64((ulong) l);

    public static explicit operator BitMask64(ulong l) => new BitMask64(l);

    public bool Equals(BitMask64 other) => Value == other.Value;

    public override bool Equals(object? obj) => obj is BitMask64 other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(BitMask64 left, BitMask64 right) => left.Equals(right);

    public static bool operator !=(BitMask64 left, BitMask64 right) => !(left == right);

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
    
    [SerializeFormatter(typeof(BitMask64))]
    public class BitMask64_Formatter : SerializeFormatter<BitMask64> {
        
        public override void SerializeWriting(in SerializeWriter writer, scoped in BitMask64 value) {
            writer.WriteUInt64(value.Value);
        }

        public override void SerializeReading(in SerializeReader reader, scoped ref BitMask64 value) {
            value = new BitMask64(reader.ReadUInt64());
        }
    }
}