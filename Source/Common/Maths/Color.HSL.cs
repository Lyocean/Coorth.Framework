using System;

namespace Coorth.Maths; 

public partial struct Color {
    
    
    public Hsl ToHsl() {
        var r = R;
        var g = G;
        var b = B;
   
        var v = Math.Max(r, g);
        v = Math.Max(v, b);

        var m = Math.Min(r, g);
        m = Math.Min(m, b);
        
        var h = 0f; // default to black
        var s = 0f;
        var l = (m + v) / 2.0f;

        if (l <= 0.0) {
            return new Hsl(h, s, l);
        }

        var vm = v - m;
        s = vm;

        if (s > 0.0f) {
            s /= l <= 0.5f ? v + m : 2.0f - v - m;
        }
        else {
            return new Hsl(h, s, l);
        }

        var r2 = (v - r) / vm;
        var g2 = (v - g) / vm;
        var b2 = (v - b) / vm;

        if (Math.Abs(r - v) < float.Epsilon) {
            h = Math.Abs(g - m) < float.Epsilon ? 5.0f + b2 : 1.0f - g2;
        }
        else if (Math.Abs(g - v) < float.Epsilon) {
            h = Math.Abs(b - m) < float.Epsilon ? 1.0f + r2 : 3.0f - b2;
        }
        else {
            h = Math.Abs(r - m) < float.Epsilon ? 3.0f + g2 : 5.0f - r2;
        }

        h *= 60;
        h = NormalizeHue(h);

        return new Hsl(h, s, l);
    }
    
    private static float NormalizeHue(float h) {
        if (h < 0) {
            return h + 360 * ((int) (h/360) + 1);
        }
        return h % 360;
    }
    
    public readonly struct Hsl : IEquatable<Hsl> {
        public readonly float H;
        public readonly float S;
        public readonly float L;

        public Hsl(float h, float s, float l) {
            H = h;
            S = s;
            L = l;
        }

        public bool Equals(Hsl other) {
            return H.Equals(other.H) && S.Equals(other.S) && L.Equals(other.L);
        }

        public override bool Equals(object? obj) {
            return obj is Hsl other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine(H, S, L);
        }
    }

}