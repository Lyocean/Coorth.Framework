﻿namespace Coorth.Framework; 

public readonly record struct EntityId(int Index, int Version) {
        
    public readonly int Index = Index;
        
    public readonly int Version = Version;

    public static EntityId Null => new(0, 0);

    private EntityId(long uid) : this((int) (uid & ~0xFFFFFFFF), (int) ((uid >> sizeof(int)) & ~0xFFFFFFFF)) { }

    public bool IsNull => Index == 0 && Version == 0;

    public bool IsNotNull => Index != 0 || Version != 0;

    public static explicit operator long(EntityId id) => ((long) id.Version << sizeof(int)) | (uint) id.Index;

    public static explicit operator EntityId(long uid) => new(uid);

    public override string ToString() => $"(Index:{Index}, Version:{Version})";

    public override int GetHashCode() => Index;
}