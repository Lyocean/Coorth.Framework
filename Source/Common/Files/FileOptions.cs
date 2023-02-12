using System.IO;

namespace Coorth.Files; 

public readonly record struct FileOptions(FileMode Mode, FileAccess Access, FileShare Share) {
    public readonly FileMode Mode = Mode;
    public readonly FileAccess Access = Access;
    public readonly FileShare Share = Share;

    public static readonly FileOptions Read = new(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    public static readonly FileOptions Write = new(FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

}