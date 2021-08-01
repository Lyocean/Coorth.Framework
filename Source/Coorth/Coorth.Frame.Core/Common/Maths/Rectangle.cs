using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Rectangle: IEquatable<Rectangle> {
        public float X;
        public float Y;
        public float W;
        public float H;

        public Vector2 Min => new Vector2(X, Y);

        public Vector2 Max => new Vector2(X + W, Y + H);

        public Vector2 Size => new Vector2(W, H);
        
        public static readonly Rectangle Empty;

        public Rectangle(float x, float y, float w, float h) {
            this.X = x;
            this.Y = y;
            this.W = w;
            this.H = h;
        }
        
        public bool Contains(Vector2 position) {
            return (position.X >= X && position.Y >= Y) && (position.X <= X + W && position.Y <= Y + H);
        }
        
        public bool Equals(Rectangle other) {
            return X.Equals(other.X) && Y.Equals(other.Y) && W.Equals(other.W) && H.Equals(other.H);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Rectangle) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                hashCode = (hashCode * 397) ^ H.GetHashCode();
                return hashCode;
            }
        }
    }
}