using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [StoreContract("DFEF084C-77FD-4EE8-9323-779C20DE7FCD")]
    [Serializable, StructLayout(LayoutKind.Sequential, Size = 4)]
    public partial struct Color : IEquatable<Color> {
        
        public float R;
        
        public float G;
        
        public float B;
        
        public float A;



        public Color(float rgb, float alpha = 1f) {
            this.R = this.G = this.B = rgb;
            this.A = alpha;
        }
        
        public Color(float r, float g, float b, float a = 1f) {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }
        
        public Color(int r, int g, int b, int a = 255) {
            this.R = r / 255.0f;
            this.G = g / 255.0f;
            this.B = b / 255.0f;
            this.A = a / 255.0f;
        }
        
        public Color(byte r, byte g, byte b, byte alpha = 255) {
            this.R = r / 255.0f;
            this.G = g / 255.0f;
            this.B = b / 255.0f;
            this.A = alpha / 255.0f;
        }
        
        public Color(Vector4 value) {
            this.R = value.X;
            this.G = value.Y;
            this.B = value.Z;
            this.A = value.W;
        }
        
        public Color(Vector3 value, float alpha = 1f) {
            this.R = value.X;
            this.G = value.Y;
            this.B = value.Z;
            this.A = alpha;
        }

        #if NET5_0_OR_GREATER

        public Color(Span<float> values) {
            this.R = values[0];
            this.G = values[1];
            this.B = values[2];
            this.A = values[3];
        }
        
        #endif
        
        public Color(float[] values, int index = 0) {
            if (values == null) {
                throw new ArgumentNullException(nameof(values));
            }
            if (values.Length != index + 4) {
                throw new ArgumentOutOfRangeException(nameof(values));
            }
            this.R = values[index];
            this.G = values[index+1];
            this.B = values[index+2];
            this.A = values[index+3];
        }
        
        public float[] ToArray() {
            return new[] { this.R, this.G, this.B, this.A };
        }
        
        public Color(int value) {
            this.R = ((value >> 24) & 255) / 255.0f;
            this.G = ((value >> 16) & 255) / 255.0f;
            this.B = ((value >> 8) & 255) / 255.0f;
            this.A = (value & 255) / 255.0f;
        }
        
        public static explicit operator int(Color value) {
            return ((int)(byte)(value.R * 255) << 24) | ((int)(byte)(value.G * 255) << 16) | ((int)(byte)(value.B * 255) << 8) | (int)(byte)(value.B * 255);
        }
        
        public static explicit operator Color(int value) {
            return new Color(value);
        }
        
        public Color(long value) {
            R = ((value >> 48) & 255) / 255.0f;
            G = ((value >> 32) & 255) / 255.0f;
            B = ((value >> 16) & 255) / 255.0f;
            A = (value & 255) / 255.0f;
        }
        
        public static explicit operator long(Color value) {
            var r = ((long) (value.R * 255) << 48);
            var g = ((long) (value.G * 255) << 32);
            var b = ((long) (value.B * 255) << 16);
            var a = ((long) (value.A * 255));
            return r | g | b | a;
        }
        
        public static explicit operator Color(long value) {
            return new Color(value);
        }

        public float this[int index] {
            get {
                switch (index) {
                    case 0: return this.R;
                    case 1: return this.G;
                    case 2: return this.B;
                    case 3: return this.A;
                }
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            set {
                switch (index) {
                    case 0: this.R = value; break;
                    case 1: this.G = value; break;
                    case 2: this.B = value; break;
                    case 3: this.A = value; break;
                } 
                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public bool Equals(Color other) {
            return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A);
        }

        public override bool Equals(object obj) {
            return obj is Color other && Equals(other);
        }

        public static bool operator ==(Color left, Color right) {
            return left.Equals(right);
        }

        public static bool operator !=(Color left, Color right) {
            return !(left == right);
        }
        
        public override int GetHashCode() {
            unchecked {
                int hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                hashCode = (hashCode * 397) ^ A.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString() {
            return $"Color(R:{R},G:{G},B:{B},A:{A})";
        }
    }
}