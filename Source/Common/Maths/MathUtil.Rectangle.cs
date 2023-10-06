using System;

namespace Coorth.Maths; 

public partial class MathUtil {
    public static Rectangle Clamp(Rectangle rect, in Rectangle parent) {
        Rectangle result = default;
        Clamp(ref result, parent);
        return result;
    }
    
    public static void Clamp(ref Rectangle rect, in Rectangle parent) {
        rect.X = Math.Clamp(rect.X, parent.XMin, parent.XMax);
        rect.Y = Math.Clamp(rect.Y, parent.YMin, parent.YMax);
        rect.W = Math.Clamp(rect.W, 0, parent.XMax - rect.X);
        rect.H = Math.Clamp(rect.H, 0, parent.YMax - rect.Y);
    }
}