namespace Coorth.Framework;

public readonly record struct NodeData(NodeDefinition Definition, int InputMin, int InputMax, int OutputMin, int OutputMax) {
    public readonly NodeDefinition Definition = Definition;
    public readonly int InputMin = InputMin;
    public readonly int InputMax = InputMax;
    public readonly int OutputMin = OutputMin;
    public readonly int OutputMax = OutputMax;
}