namespace Coorth.Maths {
    public partial struct Color {
        /// <summary>透明</summary>
        public static readonly Color Transparent = new Color(0f, 0f, 0f, 0f);
        
        /// <summary>黑色</summary>
        public static readonly Color Black       = new Color(0f, 0f, 0f, 1f);

        /// <summary>白色</summary>
        public static readonly Color White       = new Color(1f, 1f, 1f, 1f);
        
        /// <summary>红色</summary>
        public static readonly Color Red         = new Color(1f, 0f, 0f, 1f);
        
        /// <summary>绿色</summary>
        public static readonly Color Green       = new Color(0f, 1f, 0f, 1f);
        
        /// <summary>蓝色</summary>
        public static readonly Color Blue        = new Color(0f, 0f, 1f, 1f);
        
        /// <summary>黄色</summary>
        public static readonly Color Yellow      = new Color(1f, 1f, 0f, 1f);
        
        /// <summary>紫色</summary>
        public static readonly Color Purple      = new Color(1f, 0f, 1f, 1f);
        
        /// <summary>青色</summary>
        public static readonly Color Cyan        = new Color(0f, 1f, 1f, 1f);
    }
}