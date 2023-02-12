namespace Coorth.Framework;

public readonly record struct PortData(PortDefinition Definition, int EdgeMin, int EdgeMax) {
    public readonly PortDefinition Definition = Definition;
    public readonly int EdgeMin = EdgeMin;
    public readonly int EdgeMax = EdgeMax;
}