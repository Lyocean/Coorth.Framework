using System;
using System.Numerics;
using Coorth.Maths;

namespace Coorth.Platforms;

public interface IGraphicsSurface {
    
    string Name { get; set; }
    
    Rectangle Rect { get; set; }

    Vector2 Size { get; set; }

    IntPtr Handle { get; }
}
