namespace Coorth.Framework;

[Event]
public readonly record struct EventAppStatus(bool IsPause) {
    public readonly bool IsPause = IsPause;
}