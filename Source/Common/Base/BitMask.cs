namespace Coorth {
    public struct BitMask32 {

        private uint data;

        public uint Data => this.data;

        public bool IsNull => data != 0;

        public static BitMask32 Null => new BitMask32(0);
        
        public static BitMask32 Full => new BitMask32(~0u);
        
        public BitMask32(uint v) {
            this.data = v;
        }

        public bool this[int index] {
            get => (data & (1u << index)) > 0;
            set {
                if (value) {
                    this.data |= (1u << index);
                } else {
                    this.data &= ~(1u << index);
                }
            }
        }
        
        public static BitMask32 operator &(BitMask32 l, BitMask32 r) {
            return new BitMask32(l.data & r.data);
        }
        
        public static BitMask32 operator |(BitMask32 l, BitMask32 r) {
            return new BitMask32(l.data | r.data);
        }
        
        public static BitMask32 operator ~(BitMask32 l) {
            return new BitMask32(~l.data);
        }
        
        public static explicit operator int(BitMask32 l) {
            return (int)l.Data;
        }
        
        public static explicit operator uint(BitMask32 l) {
            return l.Data;
        }
        
        public static explicit operator BitMask32(int l) {
            return new BitMask32((uint)l);
        }
        
        public static explicit operator BitMask32(uint l) {
            return new BitMask32(l);
        }
    }
    
    public struct BitMask64 {

        private ulong data;

        public ulong Data => this.data;

        public bool IsNull => data != 0;

        public static BitMask64 Null => new BitMask64(0);
        
        public static BitMask64 Full => new BitMask64(~0ul);
        
        public const int CAPACITY = 64;
        
        public BitMask64(ulong v) {
            this.data = v;
        }

        public bool this[int index] {
            get => (data & (1ul << index)) > 0;
            set {
                if (value) {
                    this.data |= (1ul << index);
                } else {
                    this.data &= ~(1ul << index);
                }
            }
        }
        
        public static BitMask64 operator &(BitMask64 l, BitMask64 r) {
            return new BitMask64(l.data & r.data);
        }
        
        public static BitMask64 operator |(BitMask64 l, BitMask64 r) {
            return new BitMask64(l.data | r.data);
        }
        
        public static BitMask64 operator ~(BitMask64 l) {
            return new BitMask64(~l.data);
        }
        
        public static explicit operator long(BitMask64 l) {
            return (int)l.Data;
        }
        
        public static explicit operator ulong(BitMask64 l) {
            return l.Data;
        }
        
        public static explicit operator BitMask64(long l) {
            return new BitMask64((ulong)l);
        }
        
        public static explicit operator BitMask64(ulong l) {
            return new BitMask64(l);
        }
    }
}