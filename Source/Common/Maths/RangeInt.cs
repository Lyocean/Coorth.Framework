using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[DataDefine(DataFlags.PubField), Guid("4B392DDA-3029-4A33-B8DD-F708EA0DE8C7")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct RangeInt(int From, int Size)  {
    public int From = From;
    public int Size = Size;
}