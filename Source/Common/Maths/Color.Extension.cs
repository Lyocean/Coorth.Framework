using System;

namespace Coorth.Maths; 

public partial struct Color {

    private const double GAMMA = 2.2;

    public Color ToLinear() {
        return new Color {
            R = (float)Math.Pow(R, GAMMA),
            G = (float)Math.Pow(G, GAMMA),
            B = (float)Math.Pow(B, GAMMA),
            A = A
        };
    }


    public Color ToGamma() {
        return new Color {
            R = (float)Math.Pow(R, 1/GAMMA),
            G = (float)Math.Pow(G, 1/GAMMA),
            B = (float)Math.Pow(B, 1/GAMMA),
            A = A
        };
    }
    
    public Color ToGray() {
        // R * 0.2126f + G * 0.7152f + B * 0.0722f;
        // (R + G + B)/3f;
        // (Math.Max(R, G, B) + Math.Min(R, G, B)) / 2
        var gray = R * 0.299f + G *  0.587f + B * 0.114f;
        return new Color(gray, gray, gray, A);
    }
}