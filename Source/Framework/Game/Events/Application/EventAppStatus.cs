namespace Coorth.Framework;

[Event]
public record EventAppStatus(bool IsPause) {
    public readonly bool IsPause = IsPause;
}