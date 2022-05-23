namespace Coorth.Maths; 

public partial struct Color {
    /// <summary>透明</summary>
    public static readonly Color Transparent = new (0f, 0f, 0f, a:0f);
        
    /// <summary>黑色</summary>
    public static readonly Color Black       = new (0f, 0f, 0f, a:1f);

    /// <summary>白色</summary>
    public static readonly Color White       = new (1f, 1f, 1f, a:1f);
        
    /// <summary>红色</summary>
    public static readonly Color Red         = new (1f, 0f, 0f, a:1f);
        
    /// <summary>绿色</summary>
    public static readonly Color Green       = new (0f, 1f, 0f, a:1f);
        
    /// <summary>蓝色</summary>
    public static readonly Color Blue        = new (0f, 0f, 1f, a:1f);
        
    /// <summary>黄色</summary>
    public static readonly Color Yellow      = new (1f, 1f, 0f, a:1f);
        
    /// <summary>紫色</summary>
    public static readonly Color Purple      = new (1f, 0f, 1f, a:1f);
        
    /// <summary>青色</summary>
    public static readonly Color Cyan        = new (0f, 1f, 1f, a:1f);
}