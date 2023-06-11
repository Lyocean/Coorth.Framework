namespace Coorth.Framework;

public readonly record struct WorldCreateEvent(World World) {
    public readonly World World = World;
}

public readonly record struct WorldRemoveEvent(World World) {
    public readonly World World = World;
}
