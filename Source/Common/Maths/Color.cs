using System;
using System.Numerics;
using System.Runtime.InteropServices;


namespace Coorth.Maths;

[StoreContract, Guid("DFEF084C-77FD-4EE8-9323-779C20DE7FCD")]
[Serializable, StructLayout(LayoutKind.Sequential, Size = 4)]
public partial struct Color : IEquatable<Color> {
    
    public float R;

    public float G;

    public float B;

    public float A;

    public Color(float rgb, float alpha = 1f) {
        R = G = B = rgb;
        A = alpha;
    }

    public Color(float r, float g, float b, float a = 1f) {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Color(int r, int g, int b, int a = 255) {
        R = r / 255.0f;
        G = g / 255.0f;
        B = b / 255.0f;
        A = a / 255.0f;
    }

    public Color(byte r, byte g, byte b, byte alpha = 255) {
        R = r / 255.0f;
        G = g / 255.0f;
        B = b / 255.0f;
        A = alpha / 255.0f;
    }

    public Color(Vector4 value) {
        R = value.X;
        G = value.Y;
        B = value.Z;
        A = value.W;
    }

    public Color(Vector3 value, float alpha = 1f) {
        R = value.X;
        G = value.Y;
        B = value.Z;
        A = alpha;
    }

    public Color(ReadOnlySpan<float> values) {
        R = values[0];
        G = values[1];
        B = values[2];
        A = values[3];
    }

    public Color(float[] values, int index = 0) {
        if (values == null) {
            throw new ArgumentNullException(nameof(values));
        }
        if (values.Length != index + 4) {
            throw new ArgumentOutOfRangeException(nameof(values));
        }
        R = values[index];
        G = values[index + 1];
        B = values[index + 2];
        A = values[index + 3];
    }

    public readonly float[] ToArray() => new[] {R, G, B, A};

    public Color(int value) {
        R = ((value >> 24) & 255) / 255.0f;
        G = ((value >> 16) & 255) / 255.0f;
        B = ((value >> 8) & 255) / 255.0f;
        A = (value & 255) / 255.0f;
    }

    public static explicit operator int(Color value) {
        return ((byte) (value.R * 255) << 24) | ((byte) (value.G * 255) << 16) | ((byte) (value.B * 255) << 8) | (byte) (value.B * 255);
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
        var r = (long) (value.R * 255) << 48;
        var g = (long) (value.G * 255) << 32;
        var b = (long) (value.B * 255) << 16;
        var a = (long) (value.A * 255);
        return r | g | b | a;
    }

    public static explicit operator Color(long value) {
        return new Color(value);
    }

    public float this[int index] {
        readonly get {
            switch (index) {
                case 0: return R;
                case 1: return G;
                case 2: return B;
                case 3: return A;
            }
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        set {
            switch (index) {
                case 0:
                    R = value;
                    break;
                case 1:
                    G = value;
                    break;
                case 2:
                    B = value;
                    break;
                case 3:
                    A = value;
                    break;
            }
            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }

    public Vector3 RGB() => new(R, G, B);

    public Vector4 ToVector4() => new(R, G, B, A);

    public static Color operator *(Color color, float value) {
        return new Color(color.R * value, color.G * value, color.B * value, color.A * value);
    }

    public readonly bool Equals(Color other) {
        return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A);
    }

    public readonly override bool Equals(object? obj) {
        return obj is Color other && Equals(other);
    }

    public static bool operator ==(Color left, Color right) {
        return left.Equals(right);
    }

    public static bool operator !=(Color left, Color right) {
        return !(left == right);
    }

    public readonly override int GetHashCode() {
        return HashCode.Combine(R, G, B, A);
    }

    public readonly override string ToString() {
        return $"Color(R:{R},G:{G},B:{B},A:{A})";
    }

    public readonly string ToHexString() {
        void GetHexChar(float v, out char lChar, out char rChar) {
            var lValue = ((int) (v * 255)) / 16;
            var rValue = ((int) (v * 255)) % 16;
            if (lValue < 10) {
                lChar = (char) ('0' + lValue);
            }
            else {
                lChar = (char) ('A' + (lValue - 10));
            }
            if (rValue < 10) {
                rChar = (char) ('0' + rValue);
            }
            else {
                rChar = (char) ('A' + (rValue - 10));
            }
        }

        GetHexChar(R, out var rl, out var rr);
        GetHexChar(G, out var gl, out var gr);
        GetHexChar(B, out var bl, out var br);
        GetHexChar(A, out var al, out var ar);

        return $"{rl}{rr}{gl}{gr}{bl}{br}{al}{ar}";
    }
}