using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth.Maths {
    [DataContract, Guid("62442A87-EBF5-44D7-89EF-A7BE8C4E4095")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Rectangle: IEquatable<Rectangle> {
        
        public float X;
        
        public float Y;
        
        public float W;
        
        public float H;

        public readonly Vector2 Min => new Vector2(X, Y);

        public readonly Vector2 Max => new Vector2(X + W, Y + H);

        public readonly Vector2 Size => new Vector2(W, H);
        
        public static readonly Rectangle Empty;

        public Rectangle(float x, float y, float w, float h) {
            this.X = x;
            this.Y = y;
            this.W = w;
            this.H = h;
        }
        
        public readonly bool Contains(Vector2 position) {
            return (position.X >= X && position.Y >= Y) && (position.X <= X + W && position.Y <= Y + H);
        }
        
        public readonly bool Equals(Rectangle other) {
            return X.Equals(other.X) && Y.Equals(other.Y) && W.Equals(other.W) && H.Equals(other.H);
        }

        public override readonly bool Equals(object? obj) {
            return obj is Rectangle rectangle && Equals(rectangle);
        }

        public static bool operator ==(Rectangle left, Rectangle right) {
            return left.Equals(right);
        }

        public static bool operator !=(Rectangle left, Rectangle right) {
            return !(left == right);
        }
        
        public override readonly int GetHashCode() {
            return HashCode.Combine(X, Y, W, H);
        }

        public override readonly string ToString() {
            return $"Rectangle(X:{X},Y:{Y},W:{W},H:{H})";
        }
    }
}